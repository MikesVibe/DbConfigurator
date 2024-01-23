using DbConfigurator.Authentication;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.Areas.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
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
            AutoMapperConfig autoMapper,
            SecuritySettings securitySettings)
            : base(eventAggregator, dialogService, dataService, areaDetailViewModelCreator, autoMapper, securitySettings)
        {
        }
    }
}
