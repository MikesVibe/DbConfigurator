using DbConfigurator.Model.Entities.Table;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
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
        protected virtual void OnRemoveExecute()
        {
            Items.Remove(SelectedItem);
            SelectedItem = null;
        }
        protected virtual bool OnRemoveCanExecute()
        {
            return SelectedItem is not null;
        }
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
        public ObservableCollection<ITableItem> Items { get; set; } = new();
        public ITableItem? SelectedItem { get; set; }

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand SelectionChangedCommand { get; set; }

        protected readonly IEventAggregator EventAggregator;

        private int _id;
        private bool _hasChanges;


    }
}
