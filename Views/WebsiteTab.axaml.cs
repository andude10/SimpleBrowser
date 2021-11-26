using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using SimpleBrowser.ViewModels;
using System;
using WebViewControl;

namespace SimpleBrowser.Views
{
    public partial class WebsiteTab : UserControl, IStyleable
    {
        Type IStyleable.StyleKey => typeof(WebsiteTab);
        public WebsiteTab()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
