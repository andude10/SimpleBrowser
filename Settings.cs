using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SimpleBrowser
{
    public class Settings
    {
        [JsonProperty] private static string _selectedLanguageCode;

        private static string _selectedTheme;

        private Settings()
        {
        }

        public static string SelectedLanguageCode
        {
            get => _selectedLanguageCode ?? "en";
            set => _selectedLanguageCode = value;
        }

        public static List<string> LanguageCodes { get; } = new() {"en", "ru"};

        public static List<string> Themes { get; set; } = new() {"LightTheme", "DarkTheme"};

        [JsonProperty]
        public static string SelectedTheme
        {
            get => _selectedTheme ?? Themes.First();
            set => _selectedTheme = value;
        }

        public static void SerializeInstance()
        {
            var json = JsonConvert.SerializeObject(SettingsNested.Instance);
            File.WriteAllTextAsync("settings.json", json);
        }

        public static void DeserializeInstance()
        {
            if (!File.Exists("settings.json"))
            {
                SerializeInstance();
                return;
            }

            var json = File.ReadAllText("settings.json");
            SettingsNested.Instance = JsonConvert.DeserializeObject<Settings>(json);
        }

        private class SettingsNested
        {
            internal static Settings Instance = new();
        }
    }
}