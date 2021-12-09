using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SimpleBrowser.ViewModels;
using SimpleBrowser.Views;
using System;
using System.IO;
using System.Linq;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Styling;
using SimpleBrowser.Translations;
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
            var selectedThemeStyle = new StyleInclude(new Uri($"avares://SimpleBrowser/DefaultThemes/{Settings.SelectedTheme}/Generic.axaml"))
            {
                Source = new Uri($"avares://SimpleBrowser/DefaultThemes/{Settings.SelectedTheme}/Generic.axaml")
            };
            Application.Current.Styles.Add(selectedThemeStyle);
            /*
            else
            {
                var selectedThemeStyle = new StyleInclude(new Uri($"CustomThemes\\{Settings.SelectedTheme}"))
                {
                    Source = new Uri($"Themes\\DefaultTheme")
                };
                Application.Current.Styles.Add(selectedThemeStyle);
            }
            */
            Localizer.Instance.LoadLanguage(Settings.SelectedLanguageCode);
        }
    }
}
