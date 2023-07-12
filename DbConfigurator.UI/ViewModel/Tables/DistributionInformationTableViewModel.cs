using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Detail;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DbConfigurator.Model.DTOs.Wrapper;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class DistributionInformationTableViewModel : TableViewModelBase<DistributionInformationDtoWrapper, DistributionInformationDto, IDistributionInformationService>
    {
        private readonly IDistributionInformationService _dataService;
        private readonly Func<DistibutionInformationDetailViewModel> _addDistributionInformationCreator;

        public DistributionInformationTableViewModel(IDialogService dialogService,
            IEventAggregator eventAggregator,
            IDistributionInformationService dataService,
            Func<DistibutionInformationDetailViewModel> addDistributionInformationCreator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator, dialogService, dataService)
        {
            _dataService = dataService;
            _addDistributionInformationCreator = addDistributionInformationCreator;
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
            var distributionInformationViewModel = _addDistributionInformationCreator();
            await distributionInformationViewModel.LoadAsync(-1);
            bool? result = DialogService.ShowDialog(distributionInformationViewModel);
            if (result == false)
                return;

            //Mapping DistributionInformationDto to new DistributionInformation entity
            var distributionInformationDto = distributionInformationViewModel.DistributionInformation;

            //distributionInformationDto = await _dataService.AddAsync(distributionInformationDto!);

            var wrapped = new DistributionInformationDtoWrapper(distributionInformationDto);
            Items.Add(wrapped);
        }

        protected override async void OnEditExecute()
        {
            var distributionInformationViewModel = _addDistributionInformationCreator();

            await distributionInformationViewModel.LoadAsync(SelectedItem!.Id);

            bool? result = DialogService.ShowDialog(distributionInformationViewModel);
            if (result == false)
                return;

            ////Adding recipients to distributionInformation
            //var recipientsTo_ToAdd = disInfoDto.RecipientsTo.Except(SelectedItem.RecipientsTo);
            //await _dataService.AddRecipientsToAsync(disInfoDto.Id, recipientsTo_ToAdd);

            //var recipientsCc_ToAdd = disInfoDto.RecipientsCc.Except(SelectedItem.RecipientsCc);
            //await _dataService.AddRecipientsCcAsync(disInfoDto.Id, recipientsCc_ToAdd);

            ////Updates only scalar values
            //disInfoDto = await _dataService.UpdateAsync(distributionInformationViewModel.DistributionInformation!);

            //var disInfoDto = await _dataService.GetByIdAsync(SelectedItem!.Id);
            var disInfoDto = distributionInformationViewModel.DistributionInformation;

            SelectedItem.Region = disInfoDto.Region;
            SelectedItem.Priority = disInfoDto.Priority;
            SelectedItem.RecipientsTo = disInfoDto.RecipientsTo;
            SelectedItem.RecipientsCc = disInfoDto.RecipientsCc;
        }
    }
}
