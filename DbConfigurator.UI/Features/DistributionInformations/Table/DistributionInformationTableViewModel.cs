using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;

namespace DbConfigurator.UI.Features.DistributionInformations
{
    public class DistributionInformationTableViewModel : TableViewModelBase<DistributionInformationDtoWrapper,
        DistributionInformationDto,
        IDistributionInformationService,
        CreateDistributionInformationEvent, CreateDistributionInformationEventArgs,
        EditDistributionInformationEvent, EditDistributionInformationEventArgs>
    {
        public DistributionInformationTableViewModel(IEditingWindowService dialogService,
            IEventAggregator eventAggregator,
            IDistributionInformationService dataService,
            Func<DistributionInformationDetailViewModel> DistributionInformationDetailVmCreator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator, dialogService, dataService, DistributionInformationDetailVmCreator, autoMapper)
        {
        }
    }
}
