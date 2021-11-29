using ReactiveUI;
using SimpleBrowser.Translations;

namespace SimpleBrowser.ViewModels
{
    public class TabVM : ViewModelBase
    {
        public int _index;
        public int Index
        {
            get { return _index; }
            set { this.RaiseAndSetIfChanged(ref _index, value); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { this.RaiseAndSetIfChanged(ref _isSelected, value); }
        }
        private bool _isTabLast;
        public bool IsTabLast
        {
            get { return _isTabLast; }
            set { this.RaiseAndSetIfChanged(ref _isTabLast, value); }
        }
        private string _iconUrl;
        public string IconUrl
        {
            get { return _iconUrl; }
            set { this.RaiseAndSetIfChanged(ref _iconUrl, value); }
        }
    }
}
