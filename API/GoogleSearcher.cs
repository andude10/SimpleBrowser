using SimpleBrowser.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleBrowser.API
{
    class GoogleSearcher : ISearchSystem
    {
		/// <summary>
		/// The Google Suggest search URL.
		/// </summary>
		/// <remarks>
		/// Add gl=dk for Google Denmark. Add lr=lang_da for danish results. Add hl=da to indicate the language of the UI making the request.
		/// </remarks>
		private const string _ENsuggestSearchUrl = "http://www.google.com/complete/search?output=toolbar&q={0}&hl=en";
		private const string _RUsuggestSearchUrl = "http://www.google.com/complete/search?output=toolbar&q={0}&hl=ru";
		private const string _uriSearchString = "http://google.com/search?q=";

		/// <summary>
		/// Gets the search suggestions from Google.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns>A list of <see cref="GoogleSuggestion"/>s.</returns>
		public async Task<IEnumerable<string>> GetSearchSuggestions(string query)
		{
			if (String.IsNullOrWhiteSpace(query))
			{
				return new List<string>();
			}

			string result = String.Empty;

			using (HttpClient client = new HttpClient())
			{
				string suggestSearchUrl;
				switch(Localizer.SelectedLanguageCode)
                {
					case "en":
						suggestSearchUrl = _ENsuggestSearchUrl;
						break;
					case "ru":
						suggestSearchUrl = _RUsuggestSearchUrl;
						break;
					default:
						suggestSearchUrl = _ENsuggestSearchUrl;
						break;
				}
				result = await client.GetStringAsync(String.Format(suggestSearchUrl, query));
			}

			XDocument doc = XDocument.Parse(result);

			var googleSuggestions = (from suggestion in doc.Descendants("CompleteSuggestion")
							  select new GoogleSuggestion
							  {
								  Phrase = suggestion.Element("suggestion").Attribute("data").Value
							  });

			List<GoogleSuggestion> suggestions = googleSuggestions.ToList();
			
			return suggestions.Select(s => s.Phrase.ToString());
		}

        public string Search(string searchtext)
        {
			bool isAddress = Uri.IsWellFormedUriString(searchtext, UriKind.Absolute);
			if(isAddress)
            {
				return searchtext;
			}
			else
            {
				string resultUri = _uriSearchString + searchtext.Trim();
				return resultUri;
			}
		}
    }

	/// <summary>
	/// Encapsulates a suggestion from Google.
	/// </summary>
	public class GoogleSuggestion
	{
		/// <summary>
		/// Gets or sets the phrase.
		/// </summary>
		/// <value>The phrase.</value>
		public string Phrase { get; set; }

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return this.Phrase;
		}
	}
}
