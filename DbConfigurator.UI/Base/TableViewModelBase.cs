using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        protected readonly IEditingWindowService WindowService;
        protected readonly IEventAggregator EventAggregator;
        protected readonly TDataService DataService;
        protected readonly Func<IDetailViewModel> DetailViewModelCreator;
        protected readonly AutoMapperConfig AutoMapper;

        private int _id;
        private bool _hasChanges;

        public TableViewModelBase(IEventAggregator eventAggregator,
            IEditingWindowService dialogService,
            TDataService dataService,
            Func<IDetailViewModel> detailViewModel,
            AutoMapperConfig autoMapper)
        {
            EventAggregator = eventAggregator;
            EventAggregator.GetEvent<TCreateEvent>()
                .Subscribe(args => OnAddEntityExecute(args));
            EventAggregator.GetEvent<TEditEvent>()
                .Subscribe(args => OnEditEntityExecute(args));

            WindowService = dialogService;
            DataService = dataService;
            DetailViewModelCreator = detailViewModel;
            AutoMapper = autoMapper;

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
            var distributionInformations = await DataService.GetAllAsync();

            foreach (var distributionInformation in distributionInformations)
            {
                if (distributionInformation is null)
                    continue;

                var wrapped = (TEntityWrapper?)Activator.CreateInstance(typeof(TEntityWrapper), distributionInformation);
                if (wrapped is null)
                    continue;

                Items.Add(wrapped);
            }
        }

        protected async virtual void OnAddExecute()
        {
            await Task.CompletedTask;
            var detailViewModel = DetailViewModelCreator();
            WindowService.ShowWindow(detailViewModel);
        }
        protected async virtual void OnEditExecute()
        {
            var detailViewModel = DetailViewModelCreator();
            await detailViewModel.LoadAsync(SelectedItem!.Entity);
            WindowService.ShowWindow(detailViewModel);
        }
        protected virtual bool OnEditCanExecute()
        {
            return SelectedItem is not null;
        }
        protected virtual async void OnRemoveExecute()
        {
            var entity = await DataService.DeleteAsync(SelectedItem!.Id);
            if (entity == false)
            {
                
                if (Debugger.IsAttached)
                {
                    throw new Exception();
                }
                //Entity could not be deleted
                //Log some error mesage here
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
            var area = Items.Where(a => a.Id == obj.Entity.Id).FirstOrDefault();
            if (area is null)
            {
                RefreshItemsList();
                return;
            }

            AutoMapper.Mapper.Map(obj.Entity, area);
        }
    }
}
