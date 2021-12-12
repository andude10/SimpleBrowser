using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using HtmlAgilityPack;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using ReactiveUI;
using SimpleBrowser.API;
using SimpleBrowser.Services;
using SimpleBrowser.Translations;

namespace SimpleBrowser.ViewModels
{
    public class WebsiteTabVM : TabVM
    {
        private ICommand _goBack;
        private ICommand _goForward;
        private ICommand _navigateCommand;
        private ICommand _openNewWindow;
        private ICommand _querySet;
        private ICommand _refresh;
        private ICommand _openSettingsWindow;

        private ObservableCollection<string> _searchSuggestions;

        private ISearchSystem _searchSystem;

        private string _address;
        private string _searchText;

        public event EventHandler OnAddressChanged;

        public WebsiteTabVM()
        {
            Name = Localizer.Instance["NewPage"];

            SearchSuggestions = new ObservableCollection<string>();
            SearchSystem = new GoogleSearcher();

            Address = "https://www.google.com/";

            OnAddressChanged += LoadSiteMetadata;
        }

        public ObservableCollection<string> SearchSuggestions
        {
            get => _searchSuggestions;
            set => this.RaiseAndSetIfChanged(ref _searchSuggestions, value);
        }

        private ISearchSystem SearchSystem
        {
            get => _searchSystem;
            set => this.RaiseAndSetIfChanged(ref _searchSystem, value);
        }

        /// <value>Property <c>SearchText</c> represents user request, or url displayed </value>
        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        /// <value>Property <c>Address</c> represents current address </value>
        public string Address
        {
            get => _address;
            set
            {
                this.RaiseAndSetIfChanged(ref _address, value);

                // Displaying the current address
                SearchText = Address;

                if (OnAddressChanged != null) OnAddressChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void LoadSiteMetadata(object? sender, EventArgs e)
        {
            var iconUrl = "http://www.google.com/s2/favicons?domain=" + new Uri(Address).Host;
            DownloadIcon(iconUrl);

            Name = DownloadTitle(Address);
        }


        private void DownloadIcon(string url)
        {
            using var client = new WebClient();
            client.DownloadDataAsync(new Uri(url));
            client.DownloadDataCompleted += DownloadIconComplete;
        }

        private void DownloadIconComplete(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                var bytes = e.Result;

                Stream stream = new MemoryStream(bytes);

                var image = new Bitmap(stream);
                Icon = image;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Icon = null;
            }
        }

        private string DownloadTitle(string url)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);
            try
            {
                return document.DocumentNode.SelectSingleNode("html/head/title").InnerText;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        #region Command

        public ICommand NavigateCommand
        {
            get
            {
                return _navigateCommand = new RelayCommand(() =>
                {
                    var isAddress = Uri.IsWellFormedUriString(SearchText, UriKind.Absolute);
                    if (isAddress)
                    {
                        Address = SearchText;
                    }
                    else
                    {
                        Address = SearchSystem.Search(SearchText);
                        SearchText = Address;
                    }
                });
            }
        }

        public ICommand QuerySet
        {
            get
            {
                return _querySet = new RelayCommand(async () =>
                {
                    var isAddress = Uri.IsWellFormedUriString(SearchText, UriKind.Absolute);
                    if (isAddress) return;

                    var result = new ObservableCollection<string>(await SearchSystem.GetSearchSuggestions(SearchText));
                    if (result != null) SearchSuggestions = result;
                });
            }
        }

        public ICommand GoBack
        {
            get
            {
                return _goBack = new RelayCommand(() => { WeakReferenceMessenger.Default.Send<GoBackPageMessage>(); });
            }
        }

        public ICommand GoForward
        {
            get
            {
                return _goForward = new RelayCommand(() =>
                {
                    WeakReferenceMessenger.Default.Send<GoForwardPageMessage>();
                });
            }
        }

        public ICommand Refresh
        {
            get
            {
                return _refresh = new RelayCommand(() =>
                {
                    WeakReferenceMessenger.Default.Send<RefreshPageMessage>();
                });
            }
        }

        public ICommand OpenNewWindow
        {
            get
            {
                return _openNewWindow = new RelayCommand(() =>
                {
                    WeakReferenceMessenger.Default.Send<OpenNewWindowMessage>();
                });
            }
        }

        public ICommand OpenSettingsWindow
        {
            get
            {
                return _openSettingsWindow = new RelayCommand(() =>
                {
                    WeakReferenceMessenger.Default.Send<OpenSettingsWindowMessage>();
                });
            }
        }

        #endregion
    }
}