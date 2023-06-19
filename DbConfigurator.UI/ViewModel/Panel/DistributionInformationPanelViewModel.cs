using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Panel
{
    public class DistributionInformationPanelViewModel : TableViewModelBase<DistributionInformationDtoWrapper>, IMainPanelViewModel
    {
        private readonly IDataModel _dataModel;
        private readonly Func<AddDistibutionInformationViewModel> _addDistributionInformationCreator;
        private readonly AutoMapperConfig _autoMapper;

        public DistributionInformationPanelViewModel(
            IDialogService dialogService,
            IEventAggregator eventAggregator,
            IDataModel dataModel,
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
            var distributionInformations = await _dataModel.GetAllDistributionInformationAsync();

            foreach (var distributionInformation in distributionInformations)
            {
                var mapped = _autoMapper.Mapper.Map<DistributionInformationDto>(distributionInformation);
                var wrapped = new DistributionInformationDtoWrapper(mapped);
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
            distributionInformationViewModel.DistributionInformation = _autoMapper.Mapper.Map<DistributionInformationDto>(SelectedItem!.Model);
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


            //distributionInformation.RegionId = dis.Region.Id;
            //distributionInformation.PriorityId = dis.Priority.Id;
            //distributionInformation.RecipientsTo = new List<Recipient>();
            //distributionInformation.RecipientsCc = new List<Recipient>();

            //foreach (var recipient in dis.RecipientsTo)
            //{
            //    distributionInformation.RecipientsTo.Add(recipient);
            //}
            //foreach (var recipient in dis.RecipientsCc)
            //{
            //    distributionInformation.RecipientsCc.Add(recipient);
            //}
        }
    }
}
