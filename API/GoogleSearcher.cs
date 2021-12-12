using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private const string SuggestSearchUrl = "http://www.google.com/complete/search?output=toolbar&q={0}&hl=en";
        private const string UriSearchString = "http://google.com/search?q=";

        public string Name { get; } = "Google";

        /// <summary>
        ///  Gets the search suggestions from Google.
        /// </summary>
        /// <returns>A list of <see cref="GoogleSuggestion" />s.</returns>
        public async Task<IEnumerable<string>?> GetSearchSuggestions(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<string>();

            var result = string.Empty;

            using (var client = new HttpClient())
            {
                result = await client.GetStringAsync(string.Format(SuggestSearchUrl, query));
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

        /// <summary>
        ///  Returning a query string to google
        /// </summary>
        public string Search(string searchText)
        {
            return UriSearchString + searchText.Trim();
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