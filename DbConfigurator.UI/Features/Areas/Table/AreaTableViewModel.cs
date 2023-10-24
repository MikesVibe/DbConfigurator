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

namespace DbConfigurator.UI.Features.Areas
{
    public class AreaTableViewModel : TableViewModelBase<AreaWrapper, Area, IAreaService,
        CreateAreaEvent, CreateAreaEventArgs,
        EditAreaEvent, EditAreaEventArgs>, ITableViewModel
    {

        public AreaTableViewModel(
            IEventAggregator eventAggregator,
            IEditingWindowService dialogService,
            IAreaService dataService,
            Func<AreaDetailViewModel> areaDetailViewModelCreator,
            AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService, dataService, areaDetailViewModelCreator, autoMapper)
        {
        }
    }
}
