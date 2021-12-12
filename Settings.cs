using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SimpleBrowser
{
    /// <summary>
    /// Class <c>Settings</c> implements the Singleton pattern
    /// </summary>
    public class Settings
    {
        [JsonProperty] private static string _selectedLanguageCode;

        [JsonProperty] private static string _selectedTheme;

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

        ///<value>Property <c>SelectedTheme</c> represents the currently selected theme by the user </value>
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
            // if the old settings.json file does not exist, creates a new one
            if (!File.Exists("settings.json"))
            {
                SerializeInstance();
                return;
            }

            var json = File.ReadAllText("settings.json");
            SettingsNested.Instance = JsonConvert.DeserializeObject<Settings>(json);
        }

        public class SettingsNested
        {
            ///<value>Property <c>Instance</c> represents the only instance of the Settings class, for serialization - deserialization </value>
            internal static Settings Instance = new();
        }
    }
}