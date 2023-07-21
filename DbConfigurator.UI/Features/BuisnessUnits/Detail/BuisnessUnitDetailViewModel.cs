using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
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
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<CreateBuisnessUnitEvent>()
                  .Publish(
                new CreateBuisnessUnitEventArgs
                {
                    Entity = new BuisnessUnitDto
                    {
                        Id = EntityDto.Id,
                        Name = EntityDto.Name
                    }
                });
        }

        protected override void OnUpdate()
        {
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<EditBuisnessUnitEvent>()
                  .Publish(
                new EditBuisnessUnitEventArgs
                {
                    Entity = new BuisnessUnitDto
                    {
                        Id = EntityDto.Id,
                        Name = EntityDto.Name
                    }
                });
        }
    }
}
