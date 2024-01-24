using DbConfigurator.Core.Contracts;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Base.Contracts;
using Prism.Commands;
using Prism.Events;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class DetailViewModelBase<TDataService, TEntity, TEntityWrapper> : NotifyBase, IDetailViewModel, INotifyPropertyChanged
        where TDataService : IDataService<TEntity>
        where TEntity : class, IEntity, new()
        where TEntityWrapper : ModelWrapper<TEntity>
    {
        protected enum ModelAction { Create = 0, Update = 1 }

        protected readonly TDataService DataService;
        protected readonly IEventAggregator EventAggregator;
        private TEntityWrapper wrappedEntity = default!;

        public DetailViewModelBase(TDataService dataService, IEventAggregator eventAggregator)
        {
            DataService = dataService;
            EventAggregator = eventAggregator;
            Title = "EditingWindow";
            ViewWidth = 1000;
            ViewHeight = 500;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            CancelCommand = new DelegateCommand(Cancel);
            Action = ModelAction.Create;

            //EntityDto = CreateNew();
            WrappedEntity = CreateNewWrapper();
        }
        protected ModelAction Action { get; set; } = ModelAction.Update;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public Action<bool>? CloseAction { get; set; }
        public bool WasCancelled { get; set; } = false;
        public int ViewWidth { get; set; }
        public int ViewHeight { get; set; }
        public string Title { get; set; }
        public TEntity EntityDto
        {
            get { return wrappedEntity.Model; }
            set { wrappedEntity.Model = value; }
        }
        public TEntityWrapper WrappedEntity 
        { 
            get => wrappedEntity;
            set => wrappedEntity = value;
        }

        public virtual async Task LoadAsync(IEntity entity)
        {
            var copy = entity.CreateCopy();

            try
            {
                var result = await DataService.ExistsAsync(copy.Id);
                if (result == false)
                    return;

                Action = ModelAction.Update;
                EntityDto = (TEntity)copy;
            }
            catch (System.Exception ex)
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
            return new TEntity();
        }
        private TEntityWrapper CreateNewWrapper()
        {
            var entity = CreateNew();
            return (TEntityWrapper?)Activator.CreateInstance(typeof(TEntityWrapper), entity);
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
