using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBrowser.Services
{
    public class TabEvent
    {
        public int TabIndex { get; set; }
        public EventName? Event { get; set; }

        public enum EventName
        {
            PointerPressed,
            PointerReleased,
            PointerMoved,
        }
    }
}
