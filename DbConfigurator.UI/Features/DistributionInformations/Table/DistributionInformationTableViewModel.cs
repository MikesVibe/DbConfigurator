using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.DistributionInformations
{
    public class DistributionInformationTableViewModel : TableViewModelBase<DistributionInformationDtoWrapper, DistributionInformationDto, IDistributionInformationService>
    {
        public DistributionInformationTableViewModel(IWindowService dialogService,
            IEventAggregator eventAggregator,
            IDistributionInformationService dataService,
            Func<DistributionInformationDetailViewModel> DistributionInformationDetailVmCreator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator, dialogService, dataService, DistributionInformationDetailVmCreator, autoMapper)
        {
            EventAggregator.GetEvent<CreateDistributionInformationEvent>()
                .Subscribe(OnCreateExecute);
            EventAggregator.GetEvent<EditDistributionInformationEvent>()
                .Subscribe(OnEditExecute);
        }
    }
}
