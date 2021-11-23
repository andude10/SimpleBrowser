using Avalonia;
using Avalonia.Platform;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Unicode;

namespace SimpleBrowser.Translations
{
    public class Localizer : INotifyPropertyChanged
    {
        private const string IndexerName = "Item";
        private const string IndexerArrayName = "Item[]";
        private Dictionary<string, string>? m_Strings = null;

        public Localizer()
        {
        }

        public bool LoadLanguage(string languageCode)
        {
            LanguageCode = languageCode;
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

            Uri uri = new Uri($"avares://SimpleBrowser/Assets/Languages/{languageCode}.json");
            if (assets.Exists(uri))
            {
                Encoding encoding;
                switch(languageCode)
                {
                    case "ru":
                        encoding = CodePagesEncodingProvider.Instance.GetEncoding(1251);
                        break;
                    case "en":
                        encoding = Encoding.UTF8;
                        break;
                    default:
                        encoding = Encoding.Default;
                        break;
                }
                using (StreamReader sr = new StreamReader(assets.Open(uri), encoding))
                {
                    string json = sr.ReadToEnd();
                    m_Strings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
                Invalidate();

                return true;
            }
            return false;
        } // LoadLanguage

        public string LanguageCode { get; private set; }

        public string this[string key]
        {
            get
            {
                string res;
                if (m_Strings != null && m_Strings.TryGetValue(key, out res))
                    return res.Replace("\\n", "\n");

                return $"{LanguageCode}:{key}";
            }
        }

        public static Localizer Instance { get; set; } = new Localizer();
        public event PropertyChangedEventHandler PropertyChanged;

        public void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerArrayName));
        }
    }
}
