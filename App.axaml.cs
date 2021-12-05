using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SimpleBrowser.ViewModels;
using SimpleBrowser.Views;
using System;
using WebViewControl;

namespace SimpleBrowser
{
    public class App : Application
    {
        public override void Initialize()
        {
            WebView.Settings.OsrEnabled = false;
            WebView.Settings.PersistCache = false;
            
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Exit += OnAppExit();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainVM(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
        private EventHandler<ControlledApplicationLifetimeExitEventArgs> OnAppExit()
        {
            return EventHandler
        }
    }
}
