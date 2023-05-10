using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Event
{
    public class OpenTabelViewEvent : PubSubEvent<OpenTabelViewEventArgs>
    {
    }
    public class OpenTabelViewEventArgs
    {
        public int Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
