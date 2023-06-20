using Autofac.Features.Indexed;
using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Tables;
using Newtonsoft.Json.Linq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Panel
{
    public class DistributionInformationPanelViewModel : MainPanelViewModelBase
    {
        public DistributionInformationPanelViewModel(IIndex<string, ITableViewModel> tableViewModelCreator)
        {
            DistributionInformationTable = tableViewModelCreator[nameof(DistributionInformationTableViewModel)];
        }

        public ITableViewModel DistributionInformationTable { get; set; }

        public override async Task LoadAsync()
        {
            await DistributionInformationTable.LoadAsync();
        }
    }
}
