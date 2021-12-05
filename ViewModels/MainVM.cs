using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using ReactiveUI;
using SimpleBrowser.Services;

namespace SimpleBrowser.ViewModels
{
    public class MainVM : ViewModelBase
    {
        private TabVM _selectedTab;
        private double _tabItemCount;
        private ObservableCollection<TabVM> _tabVMs;

        public MainVM()
        {
            TabVMs = new ObservableCollection<TabVM>();
            TabVMs.Add(new WebsiteTabVM());
            TabVMs.Add(new WebsiteTabVM());
            _selectedTab = TabVMs[0];

            SortTabs();
        }

        public ObservableCollection<TabVM> TabVMs
        {
            get => _tabVMs;
            set
            {
                this.RaiseAndSetIfChanged(ref _tabVMs, value);
                TabItemCount = TabVMs.Count;
            }
        }

        public TabVM SelectedTab
        {
            get => _selectedTab;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTab, value);
                SortTabs();
            }
        }

        public double TabItemCount
        {
            get => _tabItemCount;
            set => this.RaiseAndSetIfChanged(ref _tabItemCount, value);
        }

        public void SortTabs()
        {
            for (var i = 0; i < TabVMs.Count; i++)
            {
                TabVMs[i].Index = i;
                TabVMs[i].IsTabLast = false;
                TabVMs[i].IsSelected = false;
            }

            TabVMs.Last().IsTabLast = true;
            if (SelectedTab != null)
            {
                TabVMs.First(t => t == SelectedTab).IsSelected = true;
            }
            TabItemCount = TabVMs.Count;
        }

        #region Command

        private ICommand _addNewTab;
        public ICommand AddNewTab
        {
            get
            {
                return _addNewTab = new RelayCommand(() =>
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
                return _removeTab = new RelayCommand<int>(obj =>
                {
                    TabVMs.RemoveAt(obj);
                    SortTabs();
                });
            }
        }

        private ICommand _openNewWindow;
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
        #endregion
    }
}