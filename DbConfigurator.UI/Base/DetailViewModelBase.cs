using DbConfigurator.Model.Contracts;
using DbConfigurator.UI.Base.Contracts;
using Prism.Commands;
using Prism.Events;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class DetailViewModelBase<TDataService, TEntity> : NotifyBase, IDetailViewModel, INotifyPropertyChanged
        where TDataService : IDataService<TEntity>
        where TEntity : class, IEntity, new()
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

            EntityDto = CreateNew();
        }
        protected ModelAction Action { get; set; } = ModelAction.Update;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public Action<bool>? CloseAction { get; set; }
        public bool WasCancelled { get; set; } = false;
        public int ViewWidth { get; set; }
        public int ViewHeight { get; set; }
        public string Title { get; set; }
        public TEntity EntityDto { get; set; }

        public virtual async Task LoadAsync(IEntity entity)
        {
            try
            {
                var result = await DataService.ExistsAsync(entity.Id);
                if (result == false)
                    return;

                Action = ModelAction.Update;
                EntityDto = (TEntity)entity;
            }
            catch (Exception ex)
            {

            }
            await LoadAsync();
        }
        public virtual async Task LoadAsync()
        {
            await Task.CompletedTask;
        }

        private TEntity CreateNew()
        {
            Action = ModelAction.Create;
            return new TEntity();
        }

        private async void OnSaveExecute()
        {
            switch (Action)
            {
                case ModelAction.Update:
                    {
                        var result = await DataService.UpdateAsync(EntityDto!);
                        if (result)
                        {
                            OnUpdate();
                        }
                        else
                        {
                            Error();
                        }
                        break;
                    }

                case ModelAction.Create:
                    {
                        var result = await DataService.CreateAsync(EntityDto!);
                        if (result)
                        {
                            OnCreate();
                        }
                        else
                        {
                            Error();
                        }
                        break;
                    }
                default:
                    Cancel();
                    return;
            }

            WasCancelled = false;
            CloseAction?.Invoke(true);
        }

        private void Error()
        {
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
