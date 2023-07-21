using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class TableViewModelBase<TWrapper, TDto, TDataService> : ViewModelBase, ITableViewModel
        where TWrapper : IWrapperWithId
        where TDto : IEntityDto
        where TDataService : IGenericDataService<TDto>
    {
        protected readonly IWindowService WindowService;
        protected readonly IEventAggregator EventAggregator;
        protected readonly TDataService DataService;
        protected readonly Func<IDetailViewModel> DetailViewModelCreator;

        private int _id;
        private bool _hasChanges;

        public TableViewModelBase(IEventAggregator eventAggregator,
            IWindowService dialogService,
            TDataService dataService,
            Func<IDetailViewModel> detailViewModel
            )
        {
            EventAggregator = eventAggregator;
            WindowService = dialogService;
            DataService = dataService;
            DetailViewModelCreator = detailViewModel;

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
        public ObservableCollection<TWrapper> Items { get; set; } = new();
        public TWrapper? SelectedItem { get; set; }

        public abstract Task LoadAsync();

        protected async virtual void OnAddExecute()
        {
            var detailViewModel = DetailViewModelCreator();
            await detailViewModel.LoadAsync(-1);
            WindowService.ShowWindow(detailViewModel);
        }
        protected async virtual void OnEditExecute()
        {
            var detailViewModel = DetailViewModelCreator();
            await detailViewModel.LoadAsync(SelectedItem!.Id);
            WindowService.ShowWindow(detailViewModel);
        }
        protected virtual bool OnEditCanExecute()
        {
            return SelectedItem is not null;
        }
        protected virtual void OnRemoveExecute()
        {
            var buisnessUnit = DataService.GetById(SelectedItem!.Id);
            if (buisnessUnit is null)
            {
                if (Debugger.IsAttached)
                {
                    throw new Exception();
                }
                //Log some error mesage here
                return;
            }

            DataService.RemoveById(buisnessUnit.Id);

            Items.Remove(SelectedItem!);
            SelectedItem = default(TWrapper);
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
    }
}
