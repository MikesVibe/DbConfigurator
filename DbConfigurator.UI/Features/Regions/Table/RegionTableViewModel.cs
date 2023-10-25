using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;

namespace DbConfigurator.UI.Features.Regions
{
    public class RegionTableViewModel : TableViewModelBase<RegionWrapper, Region, IRegionService,
        CreateRegionEvent, CreateRegionEventArgs,
        EditRegionEvent, EditRegionEventArgs>
    {

        public RegionTableViewModel(
            IEventAggregator eventAggregator,
            IEditingWindowService dialogService,
            IRegionService dataService,
            AutoMapperConfig autoMapper,
            Func<RegionDetailViewModel> addRegionCreator
            ) : base(eventAggregator, dialogService, dataService, addRegionCreator, autoMapper)
        {
        }
    }
}
