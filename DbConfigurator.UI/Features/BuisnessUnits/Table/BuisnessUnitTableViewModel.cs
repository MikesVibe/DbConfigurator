using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System;

namespace DbConfigurator.UI.Features.BusinessUnits
{
    public class BusinessUnitTableViewModel : TableViewModelBase<BusinessUnitWrapper, BusinessUnit, IBusinessUnitService,
        CreateBusinessUnitEvent, CreateBusinessUnitEventArgs,
        EditBusinessUnitEvent, EditBusinessUnitEventArgs>, ITableViewModel
    {
        public BusinessUnitTableViewModel(IEventAggregator eventAggregator,
            IEditingWindowService dialogService,
            IBusinessUnitService dataService,
            AutoMapperConfig autoMapper,
            Func<BusinessUnitDetailViewModel> BusinessUnitDetailViewModelCreator)
            : base(eventAggregator, dialogService, dataService, BusinessUnitDetailViewModelCreator, autoMapper)
        {
        }
    }
}
