using Avalonia.Media.Imaging;
using ReactiveUI;

namespace SimpleBrowser.ViewModels
{
    public class TabVM : ViewModelBase
    {
        private Bitmap _icon;
        public int _index;
        private bool _isPointerOver;
        private bool _isSelected;
        private bool _isTabLast;
        private string _name;

        public int Index
        {
            get => _index;
            set => this.RaiseAndSetIfChanged(ref _index, value);
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => this.RaiseAndSetIfChanged(ref _isSelected, value);
        }

        public bool IsPointerOver
        {
            get => _isPointerOver;
            set => this.RaiseAndSetIfChanged(ref _isPointerOver, value);
        }

        public bool IsTabLast
        {
            get => _isTabLast;
            set => this.RaiseAndSetIfChanged(ref _isTabLast, value);
        }

        public Bitmap Icon
        {
            get => _icon;
            set => this.RaiseAndSetIfChanged(ref _icon, value);
        }
    }
}