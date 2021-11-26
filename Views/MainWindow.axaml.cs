using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using SimpleBrowser.Translations;
using SimpleBrowser.ViewModels;
using System;
using WebViewControl;

namespace SimpleBrowser.Views
{
    public partial class MainWindow : Window, IStyleable
    {
        public MainWindow()
        {
            WebView.Settings.OsrEnabled = false;
            InitializeComponent();
            DataContext = new MainVM();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnLanguageChanged(object sender, SelectionChangedEventArgs args)
        {
            var cb = sender as ComboBox;
            var language = cb.SelectedIndex == 0 ? "en" : "ru";
            Localizer.Instance.LoadLanguage(language);
        } // OnLanguageChanged
    }
}
