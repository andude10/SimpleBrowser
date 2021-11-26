using Microsoft.Toolkit.Mvvm.Input;
using ReactiveUI;
using SimpleBrowser.Translations;
using System;
using System.ComponentModel;
using WebViewControl;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBrowser.ViewModels
{
    public class WebsiteTabVM : TabVM
    {
        public WebsiteTabVM(WebView webview)
        {
            Name = Localizer.Instance["NewPage"];
            CurrentAddress = "https://www.google.com/";
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
        private string _newAddress;
        public string NewAddress
        {
            get { return _newAddress; }
            set { this.RaiseAndSetIfChanged(ref _newAddress, value);}
        }

        private string _currentAddress;
        public string CurrentAddress
        {
            get { return _currentAddress; }
            set 
            {
                if (CurrentAddress != null)
                {
                    History.Add(CurrentAddress);
                }
                this.RaiseAndSetIfChanged(ref _currentAddress, value);
                History.Add(CurrentAddress);
            }
        }

        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get
            {
                return _navigateCommand ??= new RelayCommand(() =>
                {
                    CurrentAddress = NewAddress;
                });
            }
        }

        private ICommand _showDevToolsCommand;
        public ICommand ShowDevToolsCommand
        {
            get
            {
                return _showDevToolsCommand ??= new RelayCommand(() =>
                {

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
                },
                () => History.Count > 0 || History.LastOrDefault() != CurrentAddress);
            }
        }
    }
}
