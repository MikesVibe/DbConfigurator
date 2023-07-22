using Prism.Events;

namespace DbConfigurator.UI.Event
{
    public class OpenPanelViewEvent : PubSubEvent<OpenPanelViewEventArgs>
    {
    }
    public class OpenPanelViewEventArgs
    {
        public int Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
