﻿using Autofac.Features.Indexed;
using DbConfigurator.UI.Features.DistributionInformations;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Panels.DistributionInformation
{
    public class DistributionInformationPanelViewModel : PanelViewModelBase, IDistributionInformationPanelViewModel
    {
        public DistributionInformationPanelViewModel(IIndex<string, ITableViewModel> tableViewModelCreator)
        {
            DistributionInformationTable = tableViewModelCreator[nameof(DistributionInformationTableViewModel)];
        }

        public ITableViewModel DistributionInformationTable { get; set; }

        public override async Task LoadAsync()
        {
            if (DistributionInformationTable is null)
                return;

            await DistributionInformationTable.LoadAsync();
        }
    }
}
