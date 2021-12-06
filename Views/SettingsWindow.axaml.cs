using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                UseNativeTitleBar();
            }
            this.Padding = new Thickness(
                this.OffScreenMargin.Left,
                this.OffScreenMargin.Top,
                this.OffScreenMargin.Right,
                this.OffScreenMargin.Bottom);
#if DEBUG
            this.AttachDevTools();
#endif
        }
        private void UseNativeTitleBar()
        {
            ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.SystemChrome;
            ExtendClientAreaTitleBarHeightHint = -1;
            ExtendClientAreaToDecorationsHint = false;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
