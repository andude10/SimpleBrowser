using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleBrowser.API
{
    internal class GoogleSearcher : ISearchSystem
    {
        /// <summary>
        ///     The Google Suggest search URL.
        /// </summary>
        /// <remarks>
        ///     Add gl=dk for Google Denmark. Add lr=lang_da for danish results. Add hl=da to indicate the language of the UI
        ///     making the request.
        /// </remarks>
        private const string ENsuggestSearchUrl = "http://www.google.com/complete/search?output=toolbar&q={0}&hl=en";

        private const string RUsuggestSearchUrl = "http://www.google.com/complete/search?output=toolbar&q={0}&hl=ru";
        private const string _uriSearchString = "http://google.com/search?q=";

        public string Name { get; } = "Google";

        /// <summary>
        ///     Gets the search suggestions from Google.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>A list of <see cref="GoogleSuggestion" />s.</returns>
        public async Task<IEnumerable<string>?> GetSearchSuggestions(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<string>();

            var result = string.Empty;

            using (var client = new HttpClient())
            {
                string suggestSearchUrl;
                switch (Settings.SelectedLanguageCode)
                {
                    case "en":
                        suggestSearchUrl = ENsuggestSearchUrl;
                        break;
                    case "ru":
                        suggestSearchUrl = RUsuggestSearchUrl;
                        break;
                    default:
                        suggestSearchUrl = ENsuggestSearchUrl;
                        break;
                }

                result = await client.GetStringAsync(string.Format(suggestSearchUrl, query));
            }

            var doc = XDocument.Parse(result);

            var googleSuggestions = from suggestion in doc.Descendants("CompleteSuggestion")
                select new GoogleSuggestion
                {
                    Phrase = suggestion.Element("suggestion").Attribute("data").Value
                };

            List<GoogleSuggestion> suggestions = googleSuggestions.ToList();

            return suggestions.Select(s => s.Phrase.ToString());
        }

        public string Search(string searchText)
        {
            return _uriSearchString + searchText.Trim();
        }
    }

    /// <summary>
    ///     Encapsulates a suggestion from Google.
    /// </summary>
    public class GoogleSuggestion
    {
        /// <summary>
        ///     Gets or sets the phrase.
        /// </summary>
        /// <value>The phrase.</value>
        public string Phrase { get; set; }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Phrase;
        }
    }
}