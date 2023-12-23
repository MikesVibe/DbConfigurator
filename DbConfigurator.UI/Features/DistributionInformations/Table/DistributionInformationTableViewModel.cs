using DbConfigurator.Authentication;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.DistributionInformations.Services;
using DbConfigurator.UI.Features.Notifications.Event;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DbConfigurator.UI.Features.DistributionInformations
{
    public class DistributionInformationTableViewModel : TableViewModelBase<DistributionInformationWrapper,
        DistributionInformation,
        IDistributionInformationService,
        CreateDistributionInformationEvent, CreateDistributionInformationEventArgs,
        EditDistributionInformationEvent, EditDistributionInformationEventArgs>
    {
        public DistributionInformationTableViewModel(IEditingWindowService dialogService,
            IEventAggregator eventAggregator,
            IDistributionInformationService dataService,
            Func<DistributionInformationDetailViewModel> DistributionInformationDetailVmCreator,
            AutoMapperConfig autoMapper,
            SecuritySettings securitySettings
            ) : base(eventAggregator, dialogService, dataService, DistributionInformationDetailVmCreator, autoMapper, securitySettings)
        {
            _eventAggregator.GetEvent<SelectedNotificationDistributionList>()
                .Subscribe(OnDistributionListSelected);
        }

        public List<int> SelectedDistributionInfoIds { get; private set; } = new List<int>();

        public override async Task LoadAsync()
        {
            var isConnected = await _dataService.CanConnect();
            if (isConnected == false)
            {
                return;
            }

            var result = await _dataService.GetAllAsyncResult();
            if (result.IsFailed)
            {
                //MessageBox.Show("Could not load data. Service may be unavailable");
                return;
            }

            var allItems = result.Value;

            Items.Clear();

            foreach (var item in allItems)
            {
                if (item is null)
                    continue;

                var wrapped = (DistributionInformationWrapper?)Activator.CreateInstance(typeof(DistributionInformationWrapper), item);
                if (wrapped is null)
                    continue;

                if(SelectedDistributionInfoIds.Contains(wrapped.Id))
                    wrapped.IsSelected = true;

                Items.Add(wrapped);
            }
        }

        private void OnDistributionListSelected(SelectedNotificationDistributionListArgs args)
        {
            SelectedDistributionInfoIds = args.DistributionInformationIds;

            foreach (var item in Items)
            {
                if (args.DistributionInformationIds.Contains(item.Id))
                {
                    item.IsSelected = true;
                }
            }
        }
    }
}
