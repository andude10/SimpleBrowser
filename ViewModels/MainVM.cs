using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBrowser.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public MainVM()
        {
            TabVMs = new List<TabVM>();
            TabVMs.Add(new WebsiteTabVM());
        }
        private List<TabVM> _tabVMs;
        public List<TabVM> TabVMs
        {
            get { return _tabVMs; }
            set { this.RaiseAndSetIfChanged(ref _tabVMs, value); }
        }
    }
}
