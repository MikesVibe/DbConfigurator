using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Startup;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class TableViewModelBase<TEntityWrapper, TEntity, TDataService,
        TCreateEvent, TCreateEventArgs,
        TEditEvent, TEditEventArgs> : NotifyBase, ITableViewModel
        where TEntityWrapper : IWrapperWithId
        where TEntity : class, IEntity, new()
        where TDataService : IDataService<TEntity>
        where TCreateEvent : PubSubEvent<TCreateEventArgs>, new()
        where TCreateEventArgs : IEventArgs<TEntity>, new()
        where TEditEvent : PubSubEvent<TEditEventArgs>, new()
        where TEditEventArgs : IEventArgs<TEntity>, new()
    {
        protected readonly IEditingWindowService _windowService;
        protected readonly IEventAggregator _eventAggregator;
        protected readonly TDataService _dataService;
        protected readonly Func<IDetailViewModel> _detailViewModelCreator;
        protected readonly AutoMapperConfig _autoMapper;

        private int _id;
        private bool _hasChanges;

        public TableViewModelBase(IEventAggregator eventAggregator,
            IEditingWindowService dialogService,
            TDataService dataService,
            Func<IDetailViewModel> detailViewModel,
            AutoMapperConfig autoMapper)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<TCreateEvent>()
                .Subscribe(args => OnAddEntityExecute(args));
            _eventAggregator.GetEvent<TEditEvent>()
                .Subscribe(args => OnEditEntityExecute(args));

            _windowService = dialogService;
            _dataService = dataService;
            _detailViewModelCreator = detailViewModel;
            _autoMapper = autoMapper;

            AddCommand = new DelegateCommand(OnAddExecute);
            EditCommand = new DelegateCommand(OnEditExecute, OnEditCanExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);

            SelectionChangedCommand = new DelegateCommand(OnSelectionChangedExecute);
        }

        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand SelectionChangedCommand { get; set; }

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
        public ObservableCollection<TEntityWrapper> Items { get; set; } = new();
        public TEntityWrapper? SelectedItem { get; set; }


        public async virtual Task LoadAsync()
        {
            if(_dataService.IsConnected == false)
            {
                return;
            }

            var result = await _dataService.GetAllAsyncResult();
            if (result.IsFailed)
            {
                //MessageBox.Show("Could not load data. Service may be unavailable");
                return;
            }

            var allItems = result.Value;

            Items.Clear();

            foreach (var item in allItems)
            {
                if (item is null)
                    continue;

                var wrapped = (TEntityWrapper?)Activator.CreateInstance(typeof(TEntityWrapper), item);
                if (wrapped is null)
                    continue;

                Items.Add(wrapped);
            }
        }
        public async virtual Task Refresh()
        {
            await LoadAsync();
        }
        protected async virtual void OnAddExecute()
        {
            var detailViewModel = _detailViewModelCreator();
            await detailViewModel.LoadAsync();
            _windowService.ShowWindow(detailViewModel);
        }
        protected async virtual void OnEditExecute()
        {
            var detailViewModel = _detailViewModelCreator();
            await detailViewModel.LoadAsync(SelectedItem!.Entity);
            _windowService.ShowWindow(detailViewModel);
        }
        protected virtual bool OnEditCanExecute()
        {
            return SelectedItem is not null;
        }
        protected virtual async void OnRemoveExecute()
        {
            var entity = await _dataService.DeleteAsync(SelectedItem!.Id);
            if (entity == false)
            {
                return;
            }

            Items.Remove(SelectedItem!);
            SelectedItem = default(TEntityWrapper);
        }
        protected virtual bool OnRemoveCanExecute()
        {
            return SelectedItem is not null;
        }
        protected virtual void OnSelectionChangedExecute()
        {
            ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditCommand).RaiseCanExecuteChanged();
        }

        protected void RefreshItemsList()
        {
            throw new NotImplementedException();
        }



        protected void OnAddEntityExecute(TCreateEventArgs obj)
        {
            var wrapped = (TEntityWrapper?)Activator.CreateInstance(typeof(TEntityWrapper), obj.Entity);
            if (wrapped is null)
                return;

            Items.Add(wrapped);
        }
        protected void OnEditEntityExecute(IEventArgs<TEntity> obj)
        {
            var item = Items.Where(a => a.Id == obj.Entity.Id).FirstOrDefault();
            if (item is null)
            {
                RefreshItemsList();
                return;
            }

            _autoMapper.Mapper.Map(obj.Entity, item);
        }


    }
}
