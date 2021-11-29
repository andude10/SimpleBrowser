using Microsoft.Toolkit.Mvvm.Input;
using ReactiveUI;
using SimpleBrowser.Translations;
using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using SimpleBrowser.API;
using Microsoft.Toolkit.Mvvm.Messaging;
using SimpleBrowser.Services;
using System.Collections.Generic;

namespace SimpleBrowser.ViewModels
{
    public class WebsiteTabVM : TabVM
    {
        public WebsiteTabVM()
        {
            Name = Localizer.Instance["NewPage"];
            History = new List<string>();
            SearchSuggestions = new ObservableCollection<string>();
            SearchSystem = new GoogleSearcher();
            CurrentAddress = "https://www.google.com/";
        }

        private ObservableCollection<string> _searchSuggestions;
        public ObservableCollection<string> SearchSuggestions
        {
            get { return _searchSuggestions; }
            set { this.RaiseAndSetIfChanged(ref _searchSuggestions, value); }
        }

        private string _selectedResult;
        public string SelectedResult
        {
            get { return _selectedResult; }
            set { this.RaiseAndSetIfChanged(ref _selectedResult, value); }
        }

        private ISearchSystem _searchSystem;
        private ISearchSystem SearchSystem
        {
            get { return _searchSystem; }
            set { this.RaiseAndSetIfChanged(ref _searchSystem, value); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { this.RaiseAndSetIfChanged(ref _searchText, value); }
        }

        private List<string> _history;
        public List<string> History
        {
            get { return _history; }
            set { this.RaiseAndSetIfChanged(ref _history, value); }
        }

        private string _currentAddress;
        public string CurrentAddress
        {
            get { return _currentAddress; }
            set
            {
                this.RaiseAndSetIfChanged(ref _currentAddress, value);
                History.Add(CurrentAddress);
                SearchText = CurrentAddress;

                IconUrl = "http://www.google.com/s2/favicons?domain=" + new Uri(CurrentAddress).Host;

                ChangeSiteIconMessage changeSiteIcon = new ChangeSiteIconMessage(IconUrl);
                WeakReferenceMessenger.Default.Send(changeSiteIcon);
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

        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get
            {
                return _navigateCommand ??= new RelayCommand(() =>
                {
                    CurrentAddress = SearchSystem.Search(SearchText);
                    SearchText = CurrentAddress;
                });
            }
        }

        private ICommand _querySet;
        public ICommand QuerySet
        {
            get
            {
                return _querySet ??= new RelayCommand(async () =>
                {
                    bool isAddress = Uri.IsWellFormedUriString(SearchText, UriKind.Absolute);
                    if (isAddress)
                    {
                        return;
                    }
                    ObservableCollection<string> result = new ObservableCollection<string>(await SearchSystem.GetSearchSuggestions(SearchText));
                    if (result != null)
                    {
                        SearchSuggestions = result;
                    }
                });
            }
        }

        private ICommand _goBack;
        public ICommand GoBack
        {
            get
            {
                return _goBack ??= new RelayCommand(() =>
                {
                    WeakReferenceMessenger.Default.Send<GoBackPageMessage>();
                });
            }
        }

        private ICommand _goForward;
        public ICommand GoForward
        {
            get
            {
                return _goForward ??= new RelayCommand(() =>
                {
                    WeakReferenceMessenger.Default.Send<GoForwardPageMessage>();
                });
            }
        }
        private ICommand _refresh;
        public ICommand Refresh
        {
            get
            {
                return _refresh ??= new RelayCommand(() =>
                {
                    WeakReferenceMessenger.Default.Send<RefreshPageMessage>();
                });
            }
        }
        public ICommand _changeTitle;
        public ICommand ChangeTitle
        {
            get
            {
                return _changeTitle ??= new RelayCommand(() =>
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
    }
} 
