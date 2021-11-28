using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using SimpleBrowser.ViewModels;
using System;
using WebViewControl;

namespace SimpleBrowser.Views
{
    public partial class WebsiteTab : UserControl
    {
        public WebsiteTab()
        {
            InitializeComponent();
            this.DataContext = new WebsiteTabVM(this.FindControl<WebView>("webView"));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
