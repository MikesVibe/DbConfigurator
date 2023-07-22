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

namespace DbConfigurator.UI.Features.Areas
{
    public class AreaTableViewModel : TableViewModelBase<AreaDtoWrapper, AreaDto, IAreaService>, ITableViewModel
    {

        public AreaTableViewModel(
            IEventAggregator eventAggregator,
            IEditingWindowService dialogService,
            IAreaService dataService,
            Func<AreaDetailViewModel> areaDetailViewModelCreator,
            AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService, dataService, areaDetailViewModelCreator, autoMapper)
        {
            EventAggregator.GetEvent<CreateAreaEvent>()
                .Subscribe(OnCreateExecute);
            EventAggregator.GetEvent<EditAreaEvent>()
                .Subscribe(OnEditExecute);
        }
    }
}
