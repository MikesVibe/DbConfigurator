using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Detail;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class DistributionInformationTableViewModel : TableViewModelBase<DistributionInformationDtoWrapper, DistributionInformationDto, IDistributionInformationService>
    {
        private readonly IDistributionInformationService _dataService;
        private readonly Func<DistibutionInformationDetailViewModel> _detailViewModelCreator;

        public DistributionInformationTableViewModel(IWindowService dialogService,
            IEventAggregator eventAggregator,
            IDistributionInformationService dataService,
            Func<DistibutionInformationDetailViewModel> addDistributionInformationCreator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator, dialogService, dataService)
        {
            _dataService = dataService;
            _detailViewModelCreator = addDistributionInformationCreator;
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

        protected override async void OnAddExecute()
        {
            var detailViewModel = _detailViewModelCreator();
            await detailViewModel.LoadAsync(-1);
            DialogService.ShowDialog(detailViewModel);
   

            //Mapping DistributionInformationDto to new DistributionInformation entity
            var distributionInformationDto = detailViewModel.DistributionInformation;


            var wrapped = new DistributionInformationDtoWrapper(distributionInformationDto);
            Items.Add(wrapped);
        }

        protected override async void OnEditExecute()
        {
            var distributionInformationViewModel = _detailViewModelCreator();

            await distributionInformationViewModel.LoadAsync(SelectedItem!.Id);

            bool? result = DialogService.ShowDialog(distributionInformationViewModel);
            if (result == false)
                return;

            var disInfoDto = distributionInformationViewModel.DistributionInformation;

            SelectedItem.Region = disInfoDto.Region;
            SelectedItem.Priority = disInfoDto.Priority;
            SelectedItem.RecipientsTo = disInfoDto.RecipientsTo;
            SelectedItem.RecipientsCc = disInfoDto.RecipientsCc;
        }
    }
}
