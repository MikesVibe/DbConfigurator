using DbConfigurator.Model;
using DbConfigurator.UI.Services.Interfaces;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class DetailViewModelBase<TDataService, TEntityDto> : ViewModelBase, IDetailViewModel, INotifyPropertyChanged
        where TDataService : IGenericDataService<TEntityDto>
        where TEntityDto : IEntityDto
    {

        private readonly TDataService _dataService;
        public DetailViewModelBase()
        {
            Title = "EditingWindow";
            ViewWidth = 1000;
            ViewHeight = 500;
            SaveCommand = new DelegateCommand(OnAddExecute, OnAddCanExecute);
            CancelCommand = new DelegateCommand(Cancel);
        }
        
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public Action<bool>? CloseAction { get; set; }
        public int ViewWidth { get; set; }
        public int ViewHeight { get; set; }
        public string Title { get; set; }

        protected virtual void OnAddExecute()
        {
            CloseAction?.Invoke(true);
        }
        protected virtual bool OnAddCanExecute()
        {
            return true;
        }
        protected virtual void Cancel()
        {
            CloseAction?.Invoke(false);
        }
    }
}
