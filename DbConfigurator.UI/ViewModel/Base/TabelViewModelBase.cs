using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class TableViewModelBase : ViewModelBase, ITabelViewModel
    {

        public TableViewModelBase(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;

            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);

            SelectionChangedCommand = new DelegateCommand(OnSelectionChangedExecute);
        }


        public abstract Task LoadAsync();


        protected abstract void OnAddExecute();
        protected abstract void OnRemoveExecute();
        protected abstract bool OnRemoveCanExecute();
        protected virtual void OnSelectionChangedExecute()
        {
            ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
        }

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

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand SelectionChangedCommand { get; set; }

        protected readonly IEventAggregator EventAggregator;

        private int _id;
        private bool _hasChanges;


    }
}
