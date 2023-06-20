using Autofac.Features.Indexed;
using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Tables;
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
    public class RegionPanelViewModel : PanelViewModelBase
    {
        public RegionPanelViewModel(IIndex<string, ITableViewModel> tableViewModelCreator)
        {
            RegionTable = tableViewModelCreator[nameof(RegionTableViewModel)];
        }

        public ITableViewModel RegionTable { get; set; }

        public override async Task LoadAsync()
        {
            await RegionTable.LoadAsync();
        }
    }
}
