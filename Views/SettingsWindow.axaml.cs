using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using SimpleBrowser.ViewModels;

namespace SimpleBrowser.Views
{
    public class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = new SettingsVM();

            // Do not use a custom title bar on Linux, because there are too many possible options.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) UseNativeTitleBar();
            Padding = new Thickness(
                OffScreenMargin.Left,
                OffScreenMargin.Top,
                OffScreenMargin.Right,
                OffScreenMargin.Bottom);

#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void UseNativeTitleBar()
        {
            ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.SystemChrome;
            ExtendClientAreaTitleBarHeightHint = -1;
            ExtendClientAreaToDecorationsHint = false;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        /// <summary>
        /// Close window button
        /// </summary>
        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}