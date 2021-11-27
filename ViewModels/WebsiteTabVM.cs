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
            History.Add(CurrentAddress);
        }
        private string _name;
        public new string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        private Uri _uri;
        public Uri Uri
        {
            get { return _uri; }
            set { this.RaiseAndSetIfChanged(ref _uri, value); }
        }

        private List<string> _history = new List<string>();
        public List<string> History
        {
            get { return _history; }
            set { this.RaiseAndSetIfChanged(ref _history, value); }
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
                if(History.All(a => a != CurrentAddress))
                {
                    History.Add(CurrentAddress);
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
                    ObservableCollection<string>result = new ObservableCollection<string>(await SearchSystem.GetSearchSuggestions(SearchText));
                    if(result != null)
                    {
                        SearchSuggestions = result;
                    }
                });
            }
        }

        private ICommand _undo;
        public ICommand Undo
        {
            get
            {
                return _undo ??= new RelayCommand(() =>
                {
                    int index = History.LastIndexOf(CurrentAddress);
                    CurrentAddress = History[index - 1];
                    SearchText = CurrentAddress;
                },
                () => History.Count > 0 || History.FirstOrDefault() != CurrentAddress);
            }
        }

        private ICommand _redo;
        public ICommand Redo
        {
            get
            {
                return _redo ??= new RelayCommand(() =>
                {
                    int index = History.LastIndexOf(CurrentAddress);
                    CurrentAddress = History[index + 1];
                    SearchText = CurrentAddress;
                },
                () => History.Count > 0 && History.LastOrDefault() != CurrentAddress);
            }
        }

        private ICommand _refresh;
        public ICommand Refresh
        {
            get
            {
                return _refresh ??= new RelayCommand(() =>
                {
                    CurrentAddress = SearchSystem.Search(SearchText);
                });
            }
        }
    }
}
