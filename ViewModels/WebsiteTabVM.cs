using ReactiveUI;
using SimpleBrowser.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBrowser.ViewModels
{
    public class WebsiteTabVM : TabVM
    {
        public WebsiteTabVM()
        {
            Name = Localizer.Instance["NewPage"];
        }
        private string _name;
        public new string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        private Uri _uri;
        public Uri Uri
        {
            get { return _uri; }
            set { this.RaiseAndSetIfChanged(ref _uri, value); }
        }

        private string _address;
        public string Address 
        {
            get { return _address; }
            set { this.RaiseAndSetIfChanged(ref _address, value); }
        }
    }
}
