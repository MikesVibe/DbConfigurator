using DbConfigurator.Model;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class DetailViewModelBase<TDataService, TEntityDto> : ViewModelBase, IDetailViewModel, INotifyPropertyChanged
        where TDataService : IGenericDataService<TEntityDto>
        where TEntityDto : IEntityDto, new()
    {
        protected enum ModelAction { Create = 0, Update = 1 }
        
        protected readonly TDataService _dataService;
        
        public DetailViewModelBase(TDataService dataService)
        {
            _dataService = dataService;

            Title = "EditingWindow";
            ViewWidth = 1000;
            ViewHeight = 500;
            SaveCommand = new DelegateCommand(OnAddExecute, OnAddCanExecute);
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
        public TEntityDto? EntityDto;

        public virtual async Task LoadAsync(int entityId)
        {
            EntityDto = (entityId > 0) ?
                    await _dataService.GetByIdAsync(entityId) :
                    CreateNew();
        }

        protected virtual TEntityDto CreateNew()
        {
            Action = ModelAction.Create;
            return new TEntityDto();
        }

        protected virtual void OnAddExecute()
        {
            if (Action == ModelAction.Update)
            {
                _dataService.UpdateAsync(EntityDto!);
            }
            else if (Action == ModelAction.Create)
            {
                _dataService.AddAsync(EntityDto!);
            }
            else
            {
                return;
            }
            CloseAction?.Invoke(true);
        }
        protected virtual bool OnAddCanExecute()
        {
            return EntityDto is not null;
        }
        protected virtual void Cancel()
        {
            WasCancelled = true;
            CloseAction?.Invoke(false);
        }
    }
}
