using Avalonia.Media.Imaging;
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
        private bool _isPointerOver;
        public bool IsPointerOver
        {
            get { return _isPointerOver; }
            set { this.RaiseAndSetIfChanged(ref _isPointerOver, value); }
        }
        private bool _isTabLast;
        public bool IsTabLast
        {
            get { return _isTabLast; }
            set { this.RaiseAndSetIfChanged(ref _isTabLast, value); }
        }
        private Bitmap _icon;
        public Bitmap Icon
        {
            get { return _icon; }
            set { this.RaiseAndSetIfChanged(ref _icon, value); }
        }
    }
}
