using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using SimpleBrowser.ViewModels;
using System;
using Microsoft.Toolkit.Mvvm.Messaging;
using WebViewControl;
using SimpleBrowser.Services;

namespace SimpleBrowser.Views
{
    public partial class WebsiteTab : UserControl
    {
        public WebsiteTab()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<WebsiteTab, RefreshPageMessage>(this, RefreshPage);
            WeakReferenceMessenger.Default.Register<WebsiteTab, GoBackPageMessage>(this, GoBackPage);
            WeakReferenceMessenger.Default.Register<WebsiteTab, GoForwardPageMessage>(this, GoForwardPage);
            WeakReferenceMessenger.Default.Register<WebsiteTab, GetUrlTitleMessage>(this, GetTitle);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        #region  Messages Handlers
        private void RefreshPage(WebsiteTab recipient, RefreshPageMessage message)
        {
            var i = this.FindControl<WebView>("webView").Title;
            this.FindControl<WebView>("webView").Reload();
        }
        private void GoBackPage(WebsiteTab recipient, GoBackPageMessage message)
        {
            this.FindControl<WebView>("webView").GoBack();
        }
        private void GoForwardPage(WebsiteTab recipient, GoForwardPageMessage message)
        {
            this.FindControl<WebView>("webView").GoForward();
        }
        private void GetTitle(WebsiteTab recipient, GetUrlTitleMessage message)
        {
            var i = this.FindControl<WebView>("webView").Title;
            message.Reply(i);
        }
        #endregion
    }
}
