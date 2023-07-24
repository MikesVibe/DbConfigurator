using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;

namespace DbConfigurator.UI.Features.Regions
{
    public class RegionTableViewModel : TableViewModelBase<RegionDtoWrapper, RegionDto, IRegionService>
    {

        public RegionTableViewModel(
            IEventAggregator eventAggregator,
            IEditingWindowService dialogService,
            IRegionService dataService,
            AutoMapperConfig autoMapper,
            Func<RegionDetailViewModel> addRegionCreator
            ) : base(eventAggregator, dialogService, dataService, addRegionCreator, autoMapper)
        {
            EventAggregator.GetEvent<CreateRegionEvent>()
                .Subscribe(OnAddEntityExecute);
            EventAggregator.GetEvent<EditRegionEvent>()
                .Subscribe(OnEditEntityExecute);

            SubscribeToCreateEvent<CreateRegionEvent, CreateRegionEventArgs>();
            SubscribeToEditEvent<EditRegionEvent, EditRegionEventArgs>();
        }
    }
}
