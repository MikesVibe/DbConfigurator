using DbConfigurator.Model.Contracts;
using DbConfigurator.UI.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Prism.Commands;
using Prism.Events;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class DetailViewModelBase<TDataService, TEntityDto> : NotifyBase, IDetailViewModel, INotifyPropertyChanged
        where TDataService : IGenericDataService<TEntityDto>
        where TEntityDto : IEntityDto, new()
    {
        protected enum ModelAction { Create = 0, Update = 1 }

        protected readonly TDataService DataService;
        protected readonly IEventAggregator EventAggregator;

        public DetailViewModelBase(TDataService dataService, IEventAggregator eventAggregator)
        {
            DataService = dataService;
            EventAggregator = eventAggregator;
            Title = "EditingWindow";
            ViewWidth = 1000;
            ViewHeight = 500;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            CancelCommand = new DelegateCommand(Cancel);
        }
        protected ModelAction Action { get; set; } = ModelAction.Update;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public Action<bool>? CloseAction { get; set; }
        public bool WasCancelled { get; set; } = false;
        public int ViewWidth { get; set; }
        public int ViewHeight { get; set; }
        public string Title { get; set; }
        public TEntityDto? EntityDto { get; set; }

        public virtual async Task LoadAsync(int entityId)
        {
            EntityDto = (entityId > 0) ?
                    await DataService.GetByIdAsync(entityId) :
                    CreateNew();
        }

        protected virtual TEntityDto CreateNew()
        {
            Action = ModelAction.Create;
            return new TEntityDto();
        }

        private async void OnSaveExecute()
        {
            if (Action == ModelAction.Create)
            {
                EntityDto = await DataService.AddAsync(EntityDto!);
                OnCreate();
            }
            else if (Action == ModelAction.Update)
            {
                await DataService.UpdateAsync(EntityDto!);
                OnUpdate();
            }
            else
            {
                return;
            }
            WasCancelled = false;
            CloseAction?.Invoke(true);
        }


        protected virtual bool OnSaveCanExecute()
        {
            return EntityDto is not null;
        }

        protected abstract void OnCreate();
        protected abstract void OnUpdate();

        protected virtual void Cancel()
        {
            WasCancelled = true;
            CloseAction?.Invoke(false);
        }
    }
}
