using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SimpleBrowser.ViewModels;
using SimpleBrowser.Views;
using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
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
                desktop.Exit += OnAppExit;
                desktop.Startup += OnAppStartup;
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainVM(),
                };
            }
            base.OnFrameworkInitializationCompleted();
        }

        private void OnAppExit(Object? obj, ControlledApplicationLifetimeExitEventArgs args)
        {
            Settings.SerializeInstance();
        }
        private void OnAppStartup(Object? obj, ControlledApplicationLifetimeStartupEventArgs args)
        {
            Settings.DeserializeInstance();
        }
    }
}
