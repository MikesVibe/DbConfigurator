using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System.ComponentModel;

namespace DbConfigurator.UI.Features.BusinessUnits
{
    public class BusinessUnitDetailViewModel : DetailViewModelBase<IBusinessUnitService, BusinessUnitDto>, IDetailViewModel, INotifyPropertyChanged
    {
        public BusinessUnitDetailViewModel(IBusinessUnitService BusinessUnitService, IEventAggregator eventAggregator) : base(BusinessUnitService, eventAggregator)
        {
            Title = "BusinessUnit";
            ViewWidth = 560;
            ViewHeight = 340;
        }

        protected override void OnCreate()
        {
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<CreateBusinessUnitEvent>()
                  .Publish(
                new CreateBusinessUnitEventArgs
                {
                    Entity = new BusinessUnitDto
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

            EventAggregator.GetEvent<EditBusinessUnitEvent>()
                  .Publish(
                new EditBusinessUnitEventArgs
                {
                    Entity = new BusinessUnitDto
                    {
                        Id = EntityDto.Id,
                        Name = EntityDto.Name
                    }
                });
        }
    }
}
