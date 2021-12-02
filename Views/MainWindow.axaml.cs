using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Toolkit.Mvvm.Messaging;
using SimpleBrowser.Services;
using SimpleBrowser.Translations;
using SimpleBrowser.ViewModels;
using System.Runtime.InteropServices;
using WebViewControl;

namespace SimpleBrowser.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();

            // Do not use a custom title bar on Linux, because there are too many possible options.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) == true)
            {
                UseNativeTitleBar();
            }
            this.Padding = new Thickness(
                            this.OffScreenMargin.Left,
                            this.OffScreenMargin.Top,
                            this.OffScreenMargin.Right,
                            this.OffScreenMargin.Bottom);

            WeakReferenceMessenger.Default.Register<MainWindow, OpenNewWindowMessage>(this, OpenNewWindow);

#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void OpenNewWindow(MainWindow recipient, OpenNewWindowMessage message)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void UseNativeTitleBar()
        {
            ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.SystemChrome;
            ExtendClientAreaTitleBarHeightHint = -1;
            ExtendClientAreaToDecorationsHint = false;
        }

        private void OnLanguageChanged(object sender, SelectionChangedEventArgs args)
        {
            var cb = sender as ComboBox;
            var language = cb.SelectedIndex == 0 ? "en" : "ru";
            Localizer.Instance.LoadLanguage(language);
        } // OnLanguageChanged

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
