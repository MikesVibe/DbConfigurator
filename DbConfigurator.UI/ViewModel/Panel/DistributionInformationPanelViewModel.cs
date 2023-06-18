using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Table;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.Model.Entities.Wrapper.Table;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Panel
{
    public class DistributionInformationPanelViewModel : TableViewModelBase, IMainPanelViewModel
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
                var mapped = _autoMapper.Mapper.Map<DistributionInformationTableItem>(distributionInformation);
                var wrapped = new DistributionInformationTableItemWrapper(mapped);
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

            //var distributionInformation = _autoMapper.Mapper.Map<DistributionInformation>(distributionInformationViewModel.DistributionInformation);
            var dis = distributionInformationViewModel.DistributionInformation;

            List<Recipient> recipientsTo = new();
            
            
            foreach (var recipient in dis.RecipientsTo)
            {
                var recipientEntity = await _dataModel.GetRecipientByIdAsync(recipient.Id);
                recipientsTo.Add(recipientEntity);
            }

            var distributionInformation = new DistributionInformation
            { 
                RegionId = dis.Region.Id,
                PriorityId = dis.Priority.Id,
                RecipientsTo = recipientsTo
            };


            _dataModel.Add(distributionInformation);
            _dataModel.SaveChanges();
            
            var recipients = distributionInformation.RecipientsTo;
            var mapped = _autoMapper.Mapper.Map<DistributionInformationTableItem>(distributionInformation);
            var wrapped = new DistributionInformationTableItemWrapper(mapped);
            Items.Add(wrapped);
        }

        protected override async void OnRemoveExecute()
        {
            var distributionInformationToRemove = await _dataModel.GetDistributionInformationByIdAsync(SelectedItem!.Id);
            _dataModel.Remove(distributionInformationToRemove);
            await _dataModel.SaveChangesAsync();

            base.OnRemoveExecute();
        }


        protected override void OnEditExecute()
        {
            var distributionInformationViewModel = _addDistributionInformationCreator();
            distributionInformationViewModel.DistributionInformation = _autoMapper.Mapper.Map<DistributionInformationDto>(SelectedItem!.Model);

            bool? result = DialogService.ShowDialog(distributionInformationViewModel);

            if (result == false)
                return;
        }
    }
}
