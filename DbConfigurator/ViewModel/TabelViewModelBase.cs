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
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        public abstract Task LoadAsync();

        protected abstract void OnDeleteExecute();
        protected abstract bool OnSaveCanExecute();
        protected abstract void OnSaveExecute();


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
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }


        protected readonly IEventAggregator EventAggregator;

        private int _id;
        private bool _hasChanges;


    }
}
