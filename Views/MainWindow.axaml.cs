using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Toolkit.Mvvm.Messaging;
using SimpleBrowser.Services;
using SimpleBrowser.Translations;
using SimpleBrowser.ViewModels;
using System.Runtime.InteropServices;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Linq;
using WebViewControl;
using Avalonia.Controls.Shapes;

namespace SimpleBrowser.Views
{
    public partial class MainWindow : Window
    {
        private TabControl mTabControl;
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

            mTabControl = this.FindControl<TabControl>("mainTabControl");
            mTabControl.PointerPressed += MainTabControl_OnPointerPressed;
            mTabControl.PointerReleased += MainTabControl_OnPointerReleased;
            mTabControl.PointerMoved += MainTabControl_OnPointerMoved;
            AddHandler(DragDrop.DragOverEvent, DragOver);

        #if DEBUG
            this.AttachDevTools();
        #endif
        }

        #region   Messages Handlers
        private void OpenNewWindow(MainWindow recipient, OpenNewWindowMessage message)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        #endregion

        #region MainTabControl events
        private void MainTabControl_OnPointerPressed(object sender, RoutedEventArgs e)
        {
            TabControl tc = this.FindControl<TabControl>("mainTabControl");
        }
        private void MainTabControl_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            
        }
        private void MainTabControl_OnPointerMoved(object? sender, PointerEventArgs e)
        {
            
        }

        private void DragOver(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion

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
