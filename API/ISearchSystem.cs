using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData.Tests;

namespace SimpleBrowser.API
{
    public interface ISearchSystem
    {
        public string Name { get; }
        public Task<IEnumerable<string>?> GetSearchSuggestions(string query);
        public string Search(string searchText);
    }
}
