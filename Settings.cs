using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Platform;
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

        public static Settings GetInstance()
        {
            return SettingsNested.Instance;
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
                SettingsNested.Instance = new Settings();
                return;
            }
            string json = File.ReadAllText("settings.json");
            SettingsNested.Instance = JsonConvert.DeserializeObject<Settings>(json);
        }
        
        [JsonProperty]
        public static string LanguageCode { get; set; }

        [JsonProperty]
        public static string MainColor { get; set; }
        [JsonProperty]
        public static string Background { get; set; }
        [JsonProperty]
        public static string Foreground1 { get; set; }
        [JsonProperty]
        public static string Foreground2 { get; set; }
        [JsonProperty]
        public static bool IsTransparentScreenBackground { get; set; }
    }
}
