using ReactiveUI;
using System.Collections.Generic;

namespace SimpleBrowser.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public MainVM()
        {
            TabVMs = new List<TabVM>();
            TabVMs.Add(new WebsiteTabVM());
            TabVMs.Add(new WebsiteTabVM() {Name="Test tab withbigtext"});
            TabVMs.Add(new WebsiteTabVM());
            TabVMs.Add(new WebsiteTabVM());
        }
        private List<TabVM> _tabVMs;
        public List<TabVM> TabVMs
        {
            get { return _tabVMs; }
            set 
            { 
                this.RaiseAndSetIfChanged(ref _tabVMs, value);
                TabItemCount = TabVMs.Count;
            }
        }
        private int _tabItemCount;
        public int TabItemCount
        {
            get { return _tabItemCount; }
            set { this.RaiseAndSetIfChanged(ref _tabItemCount, value); }
        }
    }
}
