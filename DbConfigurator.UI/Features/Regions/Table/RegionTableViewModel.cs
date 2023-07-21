using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Regions
{
    public class RegionTableViewModel : TableViewModelBase<RegionDtoWrapper, RegionDto, IRegionService>
    {

        public RegionTableViewModel(
            IEventAggregator eventAggregator,
            IWindowService dialogService,
            IRegionService dataService,
            AutoMapperConfig autoMapper,
            Func<RegionDetailViewModel> addRegionCreator
            ) : base(eventAggregator, dialogService, dataService, addRegionCreator, autoMapper)
        {
            EventAggregator.GetEvent<CreateRegionEvent>()
                .Subscribe(OnCreateExecute);
            EventAggregator.GetEvent<EditRegionEvent>()
                .Subscribe(OnEditExecute);
        }
    }
}
