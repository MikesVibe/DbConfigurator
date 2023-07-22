using Prism.Events;

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
