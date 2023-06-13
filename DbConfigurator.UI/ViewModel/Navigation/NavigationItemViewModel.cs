using DbConfigurator.UI.Event;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Navigation
{
    public class NavigationItemViewModel : ViewModelBase
    {
        public NavigationItemViewModel(int id, string displayMember,
            string detailViewModelName,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMember;
            _detailViewModelName = detailViewModelName;
            OpenTabelViewCommand = new DelegateCommand(OnOpenTabelViewExecute);
        }

        private void OnOpenTabelViewExecute()
        {
            _eventAggregator.GetEvent<OpenTabelViewEvent>()
                  .Publish(
                new OpenTabelViewEventArgs
                {
                    Id = Id,
                    ViewModelName = _detailViewModelName
                });
        }


        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; }
        public ICommand OpenTabelViewCommand { get; }


        private string _displayMember;
        private string _detailViewModelName;
        private IEventAggregator _eventAggregator;

    }
}
