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
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Panel
{
    public class RecipientPanelViewModel : MainPanelViewModelBase
    {
        public RecipientPanelViewModel(IIndex<string, ITableViewModel> tableViewModelCreator)
        {
            RecipientTable = tableViewModelCreator[nameof(RecipientTableViewModel)];
        }

        public ITableViewModel RecipientTable { get; set; }

        public override async Task LoadAsync()
        {
            await RecipientTable.LoadAsync();
        }
    }
}
