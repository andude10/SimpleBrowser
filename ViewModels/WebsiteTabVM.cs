using Microsoft.Toolkit.Mvvm.Input;
using ReactiveUI;
using SimpleBrowser.Translations;
using System;
using System.ComponentModel;
using WebViewControl;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using SimpleBrowser.API;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Net;
using System.IO;
using SimpleBrowser.Services;

namespace SimpleBrowser.ViewModels
{
    public class WebsiteTabVM : TabVM
    {
        public WebsiteTabVM()
        {
            Name = Localizer.Instance["NewPage"];
            SearchSuggestions = new ObservableCollection<string>();
            SearchSystem = new GoogleSearcher();
            CurrentAddress = "https://www.google.com/";
        }
        private string _name;
        public new string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
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

        private string _currentAddress;
        public string CurrentAddress
        {
            get { return _currentAddress; }
            set 
            {
                this.RaiseAndSetIfChanged(ref _currentAddress, value);
                SearchText = CurrentAddress;
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
                    if(isAddress)
                    {
                        return;
                    }
                    ObservableCollection<string>result = new ObservableCollection<string>(await SearchSystem.GetSearchSuggestions(SearchText));
                    if(result != null)
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
    }
}
