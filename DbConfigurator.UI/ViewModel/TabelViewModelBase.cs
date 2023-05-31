using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public abstract class TableViewModelBase : ViewModelBase, ITabelViewModel
    {

        public TableViewModelBase(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;


            AddCommand = new DelegateCommand(OnAddAreaExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
        }

        public abstract Task LoadAsync();


        protected abstract void OnAddAreaExecute();
        protected abstract void OnRemoveExecute();
        protected abstract bool OnRemoveCanExecute();

        public int Id
        {
            get { return _id; }
            protected set { _id = value; }
        }
        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    //((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand RemoveCommand { get; private set; }


        protected readonly IEventAggregator EventAggregator;

        private int _id;
        private bool _hasChanges;


    }
}
