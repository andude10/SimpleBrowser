using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Microsoft.Toolkit.Mvvm.Messaging;
using SimpleBrowser.Services;
using SimpleBrowser.Translations;
using SimpleBrowser.ViewModels;
using SimpleBrowser.Views;
using WebViewControl;

namespace SimpleBrowser
{
    public class App : Application
    {
        private List<Window> Windows { get; set; }

        public override void Initialize()
        {
            Windows = new List<Window>();
            WebView.Settings.OsrEnabled = false;
            WebView.Settings.PersistCache = false;

            WeakReferenceMessenger.Default.Register<Application, OpenNewWindowMessage>(this, OpenNewWindowHandler);
            WeakReferenceMessenger.Default.Register<Application, OpenSettingsWindowMessage>(this, OpenSettingsWindowHandler);

            AvaloniaXamlLoader.Load(this);
        }

        #region Messages Handlers
        private void OpenNewWindowHandler(Application recipient, OpenNewWindowMessage message)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private void OpenSettingsWindowHandler(Application recipient, OpenSettingsWindowMessage message)
        {
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                SettingsWindow settingsWindow = new SettingsWindow();
                settingsWindow.ShowDialog(desktop.MainWindow);
            }
        }
        #endregion

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Exit += OnAppExit;
                InitialSetup();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainVM()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void OnAppExit(object? obj, ControlledApplicationLifetimeExitEventArgs args)
        {
            Settings.SerializeInstance();
        }

        private void InitialSetup()
        {
            Settings.DeserializeInstance();
            var selectedThemeStyle =
                new StyleInclude(
                    new Uri($"avares://SimpleBrowser/DefaultThemes/{Settings.SelectedTheme}/Generic.axaml"))
                {
                    Source = new Uri($"avares://SimpleBrowser/DefaultThemes/{Settings.SelectedTheme}/Generic.axaml")
                };
            Current.Styles.Add(selectedThemeStyle);
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