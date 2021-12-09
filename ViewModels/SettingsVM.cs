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
        private List<string> _themes;
        private List<string> _languages;
        private CultureInfo _selectedLanguage;
        private string _selectedTheme;
        
        public SettingsVM()
        {
            SelectedLanguage = new CultureInfo(Settings.SelectedLanguageCode);
            Languages = new List<string>();
            Themes = Settings.Themes;
            SelectedTheme = Settings.SelectedTheme;
            
            foreach (var t in Settings.LanguageCodes)
                Languages.Add(new CultureInfo(t).EnglishName);
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

        public List<string> Languages
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