using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using ReactiveUI;

namespace SimpleBrowser.ViewModels
{
    public class SettingsVM : ViewModelBase
    {
        private ICommand _applySettings;
        private List<CultureInfo> _languages;
        private CultureInfo _selectedLanguage;
        private string _selectedTheme;
        private List<string> _themes;

        public SettingsVM()
        {
            SelectedLanguage = new CultureInfo(Settings.SelectedLanguageCode);
            Languages = new List<CultureInfo>();
            Themes = Settings.Themes;
            SelectedTheme = Settings.SelectedTheme;

            foreach (var t in Settings.LanguageCodes)
                Languages.Add(new CultureInfo(t));
        }

        public List<string> Themes
        {
            get => _themes;
            set => this.RaiseAndSetIfChanged(ref _themes, value);
        }

        public string SelectedTheme
        {
            get => _selectedTheme;
            set => this.RaiseAndSetIfChanged(ref _selectedTheme, value);
        }

        public List<CultureInfo> Languages
        {
            get => _languages;
            set => this.RaiseAndSetIfChanged(ref _languages, value);
        }

        public CultureInfo SelectedLanguage
        {
            get => _selectedLanguage;
            set => this.RaiseAndSetIfChanged(ref _selectedLanguage, value);
        }

        public ICommand ApplySettings
        {
            get
            {
                return _applySettings = new RelayCommand(() =>
                {
                    Settings.SelectedLanguageCode = SelectedLanguage.TwoLetterISOLanguageName;
                    Settings.SelectedTheme = SelectedTheme;
                });
            }
        }
    }
}