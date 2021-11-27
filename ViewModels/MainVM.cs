using Avalonia.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using WebViewControl;

namespace SimpleBrowser.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public MainVM()
        {
            TabVMs = new ObservableCollection<TabVM>();

            TabVMs.Add(new WebsiteTabVM());
            TabVMs.Add(new WebsiteTabVM(){ Name="Test tab withbigtext"});
            TabVMs.Add(new WebsiteTabVM());
            TabVMs.Add(new WebsiteTabVM());
            SelectedTab = TabVMs.First();

            SortTabs();
        }
        private ObservableCollection<TabVM> _tabVMs;
        public ObservableCollection<TabVM> TabVMs
        {
            get { return _tabVMs; }
            set 
            { 
                this.RaiseAndSetIfChanged(ref _tabVMs, value);
                TabItemCount = TabVMs.Count;
            }
        }
        private TabVM _selectedTab;
        public TabVM SelectedTab
        {
            get { return _selectedTab; }
            set 
            { 
                this.RaiseAndSetIfChanged(ref _selectedTab, value);
                SortTabs();
            }
        }

        private double _tabItemCount;
        public double TabItemCount
        {
            get { return _tabItemCount; }
            set { this.RaiseAndSetIfChanged(ref _tabItemCount, value); }
        }

        #region Command
        private ICommand _addNewTab;
        public ICommand AddNewTab
        {
            get
            {
                return _addNewTab ??= new RelayCommand(() =>
                {
                    TabVMs.Add(new WebsiteTabVM());
                    SortTabs();
                });
            }
        }
        private ICommand _removeTab;
        public ICommand RemoveTab
        {
            get
            {
                return _removeTab ??= new RelayCommand<int>(obj =>
                {
                    TabVMs.RemoveAt(obj);
                    SortTabs();
                });
            }
        }
        #endregion

        public void SortTabs()
        {
            for (int i = 0; i < TabVMs.Count; i++)
            {
                TabVMs[i].Index = i;
                TabVMs[i].IsTabLast = false;
                TabVMs[i].IsSelected = false;
            }
            TabVMs.Last().IsTabLast = true;
            TabVMs.First(t => t == SelectedTab).IsSelected = true;
            TabItemCount = TabVMs.Count;
        }
    }
}
