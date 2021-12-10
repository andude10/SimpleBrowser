using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Input;
using Avalonia.Media.Imaging;
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
        private ICommand _navigateCommand;
        private ICommand _openNewWindow;
        private ICommand _querySet;
        private ICommand _refresh;
        public ICommand _changeTitle;
        private ICommand _goBack;
        private ICommand _goForward;
        private ISearchSystem _searchSystem;

        private string _currentAddress;
        private List<string> _history;
        private ObservableCollection<string> _searchSuggestions;
        private string _searchText;

        public WebsiteTabVM()
        {
            Name = Localizer.Instance["NewPage"];
            History = new List<string>();
            SearchSuggestions = new ObservableCollection<string>();
            SearchSystem = new GoogleSearcher();
            CurrentAddress = "https://www.google.com/";
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

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public List<string> History
        {
            get => _history;
            set => this.RaiseAndSetIfChanged(ref _history, value);
        }

        public string CurrentAddress
        {
            get => _currentAddress;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentAddress, value);
                History.Add(CurrentAddress);
                SearchText = CurrentAddress;

                var iconUrl = "http://www.google.com/s2/favicons?domain=" + new Uri(CurrentAddress).Host;
                DownloadIcon(iconUrl);

                try
                {
                    Name = WeakReferenceMessenger.Default.Send<GetUrlTitleMessage>();
                }
                catch
                {
                    Name = Localizer.Instance["NewPage"];
                }
            }
        }

        public void DownloadIcon(string url)
        {
            using (var client = new WebClient())
            {
                client.DownloadDataAsync(new Uri(url));
                client.DownloadDataCompleted += DownloadComplete;
            }
        }

        private void DownloadComplete(object sender, DownloadDataCompletedEventArgs e)
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
                        CurrentAddress = SearchText;
                    }
                    else
                    {
                        CurrentAddress = SearchSystem.Search(SearchText);
                        SearchText = CurrentAddress;
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
                return _goBack = new RelayCommand(() =>
                {
                    WeakReferenceMessenger.Default.Send<GoBackPageMessage>();
                    if (ChangeTitle.CanExecute(null)) ChangeTitle.Execute(null);
                });
            }
        }

        public ICommand GoForward
        {
            get
            {
                return _goForward = new RelayCommand(() =>
                {
                    WeakReferenceMessenger.Default.Send<GoForwardPageMessage>();
                    if (ChangeTitle.CanExecute(null)) ChangeTitle.Execute(null);
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

        public ICommand ChangeTitle
        {
            get
            {
                return _changeTitle = new RelayCommand(() =>
                {
                    try
                    {
                        Name = WeakReferenceMessenger.Default.Send<GetUrlTitleMessage>();
                    }
                    catch
                    {
                        Name = Localizer.Instance["NewPage"];
                    }
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

        private ICommand _openSettingsWindow;

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