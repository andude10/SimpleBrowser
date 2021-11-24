using ReactiveUI;
using SimpleBrowser.Translations;

namespace SimpleBrowser.ViewModels
{
    public class TabVM : ViewModelBase
    {
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
    }
}
