using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
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
        private readonly IDistributionInformationService _dataService;
        private readonly AutoMapperConfig _autoMapper;

        public DistributionInformationTableViewModel(IWindowService dialogService,
            IEventAggregator eventAggregator,
            IDistributionInformationService dataService,
            Func<DistributionInformationDetailViewModel> DistributionInformationDetailVmCreator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator, dialogService, dataService, DistributionInformationDetailVmCreator, autoMapper)
        {
            _dataService = dataService;
            _autoMapper = autoMapper;
            EventAggregator.GetEvent<CreateDistributionInformationEvent>()
                .Subscribe(OnCreateExecute);
            EventAggregator.GetEvent<EditDistributionInformationEvent>()
                .Subscribe(OnEditExecute);
        }

        private void OnCreateExecute(CreateDistributionInformationEventArgs obj)
        {
            var wrapped = new DistributionInformationDtoWrapper(obj.Entity);
            Items.Add(wrapped);
        }

        public async override Task LoadAsync()
        {
            var distributionInformations = await _dataService.GetAllAsync();

            foreach (var distributionInformation in distributionInformations)
            {
                var wrapped = new DistributionInformationDtoWrapper(distributionInformation);
                Items.Add(wrapped);
            }
        }
    }
}
