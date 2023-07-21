using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.DistributionInformations
{
    public class DistributionInformationTableViewModel : TableViewModelBase<DistributionInformationDtoWrapper, DistributionInformationDto, IDistributionInformationService>
    {
        private readonly IDistributionInformationService _dataService;

        public DistributionInformationTableViewModel(IWindowService dialogService,
            IEventAggregator eventAggregator,
            IDistributionInformationService dataService,
            Func<DistibutionInformationDetailViewModel> DistributionInformationDetailVmCreator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator, dialogService, dataService, DistributionInformationDetailVmCreator)
        {
            _dataService = dataService;
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

        //protected override async void OnAddExecute()
        //{
        //    var detailViewModel = _detailViewModelCreator();
        //    await detailViewModel.LoadAsync(-1);
        //    WindowService.ShowWindow(detailViewModel);




        //    var wrapped = new DistributionInformationDtoWrapper(detailViewModel.EntityDto!);
        //    Items.Add(wrapped);
        //}

        //protected override async void OnEditExecute()
        //{
        //    var distributionInformationViewModel = _detailViewModelCreator();
        //    await distributionInformationViewModel.LoadAsync(SelectedItem!.Id);
        //    WindowService.ShowWindow(distributionInformationViewModel);

        //    if (distributionInformationViewModel.WasCancelled == true)
        //        return;


        //    var disInfoDto = distributionInformationViewModel.EntityDto;
        //    SelectedItem.Region = disInfoDto.Region;
        //    SelectedItem.Priority = disInfoDto.Priority;
        //    SelectedItem.RecipientsTo = disInfoDto.RecipientsTo;
        //    SelectedItem.RecipientsCc = disInfoDto.RecipientsCc;
        //}
    }
}
