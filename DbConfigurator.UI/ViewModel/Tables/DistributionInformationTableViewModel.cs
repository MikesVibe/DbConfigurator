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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class DistributionInformationTableViewModel : TableViewModelBase<DistributionInformationDtoWrapper>
    {
        private readonly ICombinedDataService _dataModel;
        private readonly Func<AddDistibutionInformationViewModel> _addDistributionInformationCreator;
        private readonly AutoMapperConfig _autoMapper;

        public DistributionInformationTableViewModel(IDialogService dialogService,
            IEventAggregator eventAggregator,
            ICombinedDataService dataModel,
            Func<AddDistibutionInformationViewModel> addDistributionInformationCreator,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator, dialogService)
        {
            _dataModel = dataModel;
            _addDistributionInformationCreator = addDistributionInformationCreator;
            _autoMapper = autoMapper;
        }

        public async override Task LoadAsync()
        {
            var distributionInformations = await _dataModel.GetAllDistributionInformationDtoAsync();

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
            var dis = distributionInformationViewModel.DistributionInformation;
            var distributionInformation = new DistributionInformation
            {
                RegionId = dis.Region.Id,
                PriorityId = dis.Priority.Id,
                RecipientsTo = new List<Recipient>(),
                RecipientsCc = new List<Recipient>()
            };
            foreach (var recipient in dis.RecipientsTo)
            {
                var recipientEntity = await _dataModel.GetRecipientByIdAsync(recipient.Id);
                distributionInformation.RecipientsTo.Add(recipientEntity);
            }
            foreach (var recipient in dis.RecipientsCc)
            {
                var recipientEntity = await _dataModel.GetRecipientByIdAsync(recipient.Id);
                distributionInformation.RecipientsCc.Add(recipientEntity);
            }

            _dataModel.Add(distributionInformation);
            _dataModel.SaveChanges();

            var mapped = _autoMapper.Mapper.Map<DistributionInformationDto>(distributionInformation);
            var wrapped = new DistributionInformationDtoWrapper(mapped);
            Items.Add(wrapped);
        }
        protected override async void OnRemoveExecute()
        {
            var distributionInformationToRemove = await _dataModel.GetDistributionInformationByIdAsync(SelectedItem!.Id);
            _dataModel.Remove(distributionInformationToRemove);
            await _dataModel.SaveChangesAsync();

            base.OnRemoveExecute();
        }
        protected override async void OnEditExecute()
        {
            var distributionInformationViewModel = _addDistributionInformationCreator();
            var disInfoDto = new DistributionInformationDto
            {
                Id = SelectedItem!.Model.Id,
                Region = SelectedItem!.Model.Region,
                Priority = SelectedItem!.Model.Priority,
                RecipientsTo = new ObservableCollection<RecipientDto>(SelectedItem!.Model.RecipientsTo),
                RecipientsCc = new ObservableCollection<RecipientDto>(SelectedItem!.Model.RecipientsCc)
            };
            distributionInformationViewModel.DistributionInformation = disInfoDto;
            await distributionInformationViewModel.LoadAsync();

            bool? result = DialogService.ShowDialog(distributionInformationViewModel);
            if (result == false)
                return;

            //Apply changes to distributionInformationEntity
            var dis = distributionInformationViewModel.DistributionInformation;
            var distributionInformationEntity = await _dataModel.GetDistributionInformationByIdAsync(dis.Id);
            distributionInformationEntity.RegionId = dis.Region.Id;
            distributionInformationEntity.PriorityId = dis.Priority.Id;
            distributionInformationEntity.RecipientsTo = new List<Recipient>();
            distributionInformationEntity.RecipientsCc = new List<Recipient>();

            foreach (var recipient in dis.RecipientsTo)
            {
                var recipientEntity = await _dataModel.GetRecipientByIdAsync(recipient.Id);
                distributionInformationEntity.RecipientsTo.Add(recipientEntity);
            }
            foreach (var recipient in dis.RecipientsCc)
            {
                var recipientEntity = await _dataModel.GetRecipientByIdAsync(recipient.Id);
                distributionInformationEntity.RecipientsCc.Add(recipientEntity);
            }
            _dataModel.SaveChanges();

            SelectedItem.Region = dis.Region;
            SelectedItem.Priority = dis.Priority;
            SelectedItem.RecipientsTo = dis.RecipientsTo;
            SelectedItem.RecipientsCc = dis.RecipientsCc;
        }
    }
}
