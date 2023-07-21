using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
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

        public DistributionInformationTableViewModel(IWindowService dialogService,
            IEventAggregator eventAggregator,
            IDistributionInformationService dataService,
            Func<DistributionInformationDetailViewModel> DistributionInformationDetailVmCreator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator, dialogService, dataService, DistributionInformationDetailVmCreator)
        {
            _dataService = dataService;

            EventAggregator.GetEvent<CreateDistributionInformationEvent>()
                .Subscribe(OnCreateExecute);
            EventAggregator.GetEvent<EditDistributionInformationEvent>()
                .Subscribe(OnEditExecute);
        }



        private void OnCreateExecute(CreateDistributionInformationEventArgs obj)
        {
            var wrapped = new DistributionInformationDtoWrapper(obj.DistributionInformation);
            Items.Add(wrapped);
        }
        private void OnEditExecute(EditDistributionInformationEventArgs obj)
        {
            var distributionInformation = Items.Where(a => a.Id == obj.DistributionInformation.Id).FirstOrDefault();
            if (distributionInformation is null)
            {
                RefreshItemsList();
                return;
            }

            var disInfoDto = obj.DistributionInformation;
            distributionInformation.Region = disInfoDto.Region;
            distributionInformation.Priority = disInfoDto.Priority;
            distributionInformation.RecipientsTo = disInfoDto.RecipientsTo;
            distributionInformation.RecipientsCc = disInfoDto.RecipientsCc;
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
