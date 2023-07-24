using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System;

namespace DbConfigurator.UI.Features.BuisnessUnits
{
    public class BuisnessUnitTableViewModel : TableViewModelBase<BuisnessUnitDtoWrapper, BuisnessUnitDto, IBuisnessUnitService,
        CreateBuisnessUnitEvent, CreateBuisnessUnitEventArgs,
        EditBuisnessUnitEvent, EditBuisnessUnitEventArgs>, ITableViewModel
    {
        public BuisnessUnitTableViewModel(IEventAggregator eventAggregator,
            IEditingWindowService dialogService,
            IBuisnessUnitService dataService,
            AutoMapperConfig autoMapper,
            Func<BuisnessUnitDetailViewModel> buisnessUnitDetailViewModelCreator)
            : base(eventAggregator, dialogService, dataService, buisnessUnitDetailViewModelCreator, autoMapper)
        {
        }
    }
}
