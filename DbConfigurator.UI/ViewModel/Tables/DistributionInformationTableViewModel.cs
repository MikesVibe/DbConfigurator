using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class DistributionInformationTableViewModel : TableViewModelBase<DistributionInformationDtoWrapper>
    {
        private readonly IDistributionInformationService _dataService;
        private readonly Func<AddDistibutionInformationViewModel> _addDistributionInformationCreator;
        private readonly AutoMapperConfig _autoMapper;

        public DistributionInformationTableViewModel(IDialogService dialogService,
            IEventAggregator eventAggregator,
            IDistributionInformationService dataService,
            Func<AddDistibutionInformationViewModel> addDistributionInformationCreator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator, dialogService)
        {
            _dataService = dataService;
            _addDistributionInformationCreator = addDistributionInformationCreator;
            _autoMapper = autoMapper;
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
            await distributionInformationViewModel.LoadAsync();
            bool? result = DialogService.ShowDialog(distributionInformationViewModel);
            if (result == false)
                return;

            //Mapping DistributionInformationDto to new DistributionInformation entity
            var distributionInformationDto = distributionInformationViewModel.DistributionInformation;

            distributionInformationDto = await _dataService.AddAsync(distributionInformationDto);

            var wrapped = new DistributionInformationDtoWrapper(distributionInformationDto);
            Items.Add(wrapped);
        }
        protected override async void OnRemoveExecute()
        {
            await _dataService.RemoveByIdAsync(SelectedItem!.Id);

            base.OnRemoveExecute();
        }
        protected override async void OnEditExecute()
        {
            //var distributionInformationViewModel = _addDistributionInformationCreator();
            //var disInfoDto = new DistributionInformationDto
            //{
            //    Id = SelectedItem!.Model.Id,
            //    Region = SelectedItem!.Model.Region,
            //    Priority = SelectedItem!.Model.Priority,
            //    RecipientsTo = new ObservableCollection<RecipientDto>(SelectedItem!.Model.RecipientsTo),
            //    RecipientsCc = new ObservableCollection<RecipientDto>(SelectedItem!.Model.RecipientsCc)
            //};
            //distributionInformationViewModel.DistributionInformation = disInfoDto;
            //await distributionInformationViewModel.LoadAsync();

            //bool? result = DialogService.ShowDialog(distributionInformationViewModel);
            //if (result == false)
            //    return;

            ////Apply changes to distributionInformationEntity
            //var dis = distributionInformationViewModel.DistributionInformation;
            //var distributionInformationEntity = await _dataService.GetDistributionInformationByIdAsync(dis.Id);
            //distributionInformationEntity.RegionId = dis.Region.Id;
            //distributionInformationEntity.PriorityId = dis.Priority.Id;
            //distributionInformationEntity.RecipientsTo = new List<Recipient>();
            //distributionInformationEntity.RecipientsCc = new List<Recipient>();

            //foreach (var recipient in dis.RecipientsTo)
            //{
            //    var recipientEntity = await _dataService.GetRecipientByIdAsync(recipient.Id);
            //    distributionInformationEntity.RecipientsTo.Add(recipientEntity);
            //}
            //foreach (var recipient in dis.RecipientsCc)
            //{
            //    var recipientEntity = await _dataService.GetRecipientByIdAsync(recipient.Id);
            //    distributionInformationEntity.RecipientsCc.Add(recipientEntity);
            //}
            //_dataService.SaveChanges();

            //SelectedItem.Region = dis.Region;
            //SelectedItem.Priority = dis.Priority;
            //SelectedItem.RecipientsTo = dis.RecipientsTo;
            //SelectedItem.RecipientsCc = dis.RecipientsCc;
        }
    }
}
