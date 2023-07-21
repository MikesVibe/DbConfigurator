using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System.ComponentModel;

namespace DbConfigurator.UI.Features.BuisnessUnits
{
    public class BuisnessUnitDetailViewModel : DetailViewModelBase<IBuisnessUnitService, BuisnessUnitDto>, IDetailViewModel, INotifyPropertyChanged
    {
        public BuisnessUnitDetailViewModel(IBuisnessUnitService buisnessUnitService, IEventAggregator eventAggregator) : base(buisnessUnitService, eventAggregator)
        {
            Title = "BuisnessUnit";
            ViewWidth = 560;
            ViewHeight = 340;
        }

        protected override void OnCreate()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
