using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SimpleBrowser.Views
{
    /// <summary>
    ///  This is not my code, honestly took from https://github.com/FrankenApps/Avalonia-CustomTitleBarTemplate
    /// </summary
    public class MacosTitleBar : UserControl
    {
        public static readonly StyledProperty<bool> IsSeamlessProperty =
            AvaloniaProperty.Register<MacosTitleBar, bool>(nameof(IsSeamless));

        private readonly Button closeButton;
        private readonly Button minimizeButton;
        private readonly StackPanel titleAndWindowIconWrapper;

        private readonly DockPanel titleBarBackground;
        private readonly Button zoomButton;

        public MacosTitleBar()
        {
            InitializeComponent();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) == false)
            {
                IsVisible = false;
            }
            else
            {
                minimizeButton = this.FindControl<Button>("MinimizeButton");
                zoomButton = this.FindControl<Button>("ZoomButton");
                closeButton = this.FindControl<Button>("CloseButton");

                minimizeButton.Click += MinimizeWindow;
                zoomButton.Click += MaximizeWindow;
                closeButton.Click += CloseWindow;

                titleBarBackground = this.FindControl<DockPanel>("TitleBarBackground");
                titleAndWindowIconWrapper = this.FindControl<StackPanel>("TitleAndWindowIconWrapper");

                SubscribeToWindowState();
            }
        }

        public bool IsSeamless
        {
            get => GetValue(IsSeamlessProperty);
            set
            {
                SetValue(IsSeamlessProperty, value);
                if (titleBarBackground != null && titleAndWindowIconWrapper != null)
                {
                    titleBarBackground.IsVisible = IsSeamless ? false : true;
                    titleAndWindowIconWrapper.IsVisible = IsSeamless ? false : true;
                }
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            var hostWindow = (Window) VisualRoot;
            hostWindow.Close();
        }

        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            var hostWindow = (Window) VisualRoot;

            hostWindow.WindowState = hostWindow.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            var hostWindow = (Window) VisualRoot;
            hostWindow.WindowState = WindowState.Minimized;
        }

        private async void SubscribeToWindowState()
        {
            var hostWindow = (Window) VisualRoot;

            while (hostWindow == null)
            {
                hostWindow = (Window) VisualRoot;
                await Task.Delay(50);
            }

            hostWindow.ExtendClientAreaTitleBarHeightHint = 44;
            hostWindow.GetObservable(Window.WindowStateProperty).Subscribe(s =>
            {
                if (s != WindowState.Maximized) hostWindow.Padding = new Thickness(0, 0, 0, 0);
                if (s == WindowState.Maximized)
                    hostWindow.Padding = new Thickness(7, 7, 7, 7);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}