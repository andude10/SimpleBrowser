using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using ReactiveUI;

namespace SimpleBrowser.ViewModels
{
    public class SettingsVM : ViewModelBase
    {
        private ICommand _applySettings;
        private string _background;

        private string _foreground1;

        private string _foreground2;

        private bool _isTransparentScreenBackground;
        private string _languageCode;

        private string _mainColor;

        public SettingsVM()
        {
            LanguageCode = Settings.LanguageCode;
            Foreground1 = Settings.Foreground1;
            Foreground2 = Settings.Foreground2;
            Background = Settings.Background;
            MainColor = Settings.MainColor;
            IsTransparentScreenBackground = Settings.IsTransparentScreenBackground;
        }

        public string LanguageCode
        {
            get => _languageCode;
            set => this.RaiseAndSetIfChanged(ref _languageCode, value);
        }

        public string MainColor
        {
            get => _mainColor;
            set => this.RaiseAndSetIfChanged(ref _mainColor, value);
        }

        public string Background
        {
            get => _background;
            set => this.RaiseAndSetIfChanged(ref _background, value);
        }

        public string Foreground1
        {
            get => _foreground1;
            set => this.RaiseAndSetIfChanged(ref _foreground1, value);
        }

        public string Foreground2
        {
            get => _foreground2;
            set => this.RaiseAndSetIfChanged(ref _foreground2, value);
        }

        public bool IsTransparentScreenBackground
        {
            get => _isTransparentScreenBackground;
            set => this.RaiseAndSetIfChanged(ref _isTransparentScreenBackground, value);
        }

        public ICommand ApplySettings
        {
            get
            {
                return _applySettings = new RelayCommand(() =>
                {
                    Settings.LanguageCode = LanguageCode;
                    Settings.MainColor = MainColor;
                    Settings.Foreground1 = Foreground1;
                    Settings.Foreground2 = Foreground2;
                    Settings.IsTransparentScreenBackground = IsTransparentScreenBackground;
                    Settings.Background = Background;
                });
            }
        }
    }
}