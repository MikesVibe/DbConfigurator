using DbConfigurator.Authentication;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.Regions.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using DbConfigurator.Core.Contracts;

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
            Func<RegionDetailViewModel> addRegionCreator,
            ISecuritySettings securitySettings
            ) : base(eventAggregator, dialogService, dataService, addRegionCreator, autoMapper, securitySettings)
        {
        }
    }
}
