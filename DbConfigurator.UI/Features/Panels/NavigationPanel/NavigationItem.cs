using DbConfigurator.UI.Event;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace DbConfigurator.UI.Features.Panels.NavigationPanel
{
    public class NavigationItem : NotifyBase
    {
        private string _displayMember;
        private string _detailViewModelName;
        private IEventAggregator _eventAggregator;

        public NavigationItem(int id, string displayMember,
            string detailViewModelName,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMember;
            _detailViewModelName = detailViewModelName;
            OpenTabelViewCommand = new DelegateCommand(OnOpenTabelViewExecute);
        }

        public ICommand OpenTabelViewCommand { get; }

        public int Id { get; }
        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        private void OnOpenTabelViewExecute()
        {
            _eventAggregator.GetEvent<OpenPanelViewEvent>()
                  .Publish(
                new OpenPanelViewEventArgs
                {
                    Id = Id,
                    ViewModelName = _detailViewModelName
                });
        }
    }
}
