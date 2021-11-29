using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBrowser.API
{
    public interface ISearchSystem
    {
        public abstract string Name { get; }
        public abstract Task<IEnumerable<string>> GetSearchSuggestions(string query);
        public abstract string Search(string searchtext);
    }
}
