using DbConfigurator.Core.Models;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.BuisnessUnits.Services;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System.ComponentModel;

namespace DbConfigurator.UI.Features.BusinessUnits
{
    public class BusinessUnitDetailViewModel : DetailViewModelBase<IBusinessUnitService, BusinessUnit, BusinessUnitWrapper>, IDetailViewModel, INotifyPropertyChanged
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
                    Entity = new BusinessUnit
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
                    Entity = new BusinessUnit
                    {
                        Id = EntityDto.Id,
                        Name = EntityDto.Name
                    }
                });
        }
    }
}
