using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Platform;
using HarfBuzzSharp;
using Newtonsoft.Json;

namespace SimpleBrowser
{
    public class Settings
    {
        private class SettingsNested
        {
            internal static Settings Instance = new Settings();
        }

        private Settings()
        {
        }

        public static void SerializeInstance()
        {
            string json = JsonConvert.SerializeObject(SettingsNested.Instance);
            File.WriteAllTextAsync("settings.json", json);
        }

        public static void DeserializeInstance()
        {
            if (!File.Exists("settings.json"))
            {
                SerializeInstance();
                return;
            }

            string json = File.ReadAllText("settings.json");
            SettingsNested.Instance = JsonConvert.DeserializeObject<Settings>(json);
        }

        [JsonProperty] private static string _selectedLanguageCode;
        public static string SelectedLanguageCode
        {
            get => _selectedLanguageCode ?? "en";
            set => _selectedLanguageCode = value;
        }

        public static List<string> LanguageCodes { get; }
            = new List<string>() {"en", "ru"};

        public static List<string> Themes { get; set; }
            = new List<string>() {"LightTheme", "DarkTheme"};

        private static string _selectedTheme;
        [JsonProperty] public static string SelectedTheme {
            get => _selectedTheme ?? Themes.First();
            set => _selectedTheme = value;
        }
    }
}

