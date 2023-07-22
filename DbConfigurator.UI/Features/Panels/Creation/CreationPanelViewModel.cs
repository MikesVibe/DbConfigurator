using Autofac.Features.Indexed;
using DbConfigurator.UI.Features.Areas;
using DbConfigurator.UI.Features.BuisnessUnits;
using DbConfigurator.UI.Features.Countries;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Panels.Creation
{
    public class CreationPanelViewModel : PanelViewModelBase, IMainPanelViewModel
    {
        public CreationPanelViewModel(
            IIndex<string, ITableViewModel> tableViewModelCreator
            )
        {
            AreaTable = tableViewModelCreator[nameof(AreaTableViewModel)];
            BuisnessUnitTable = tableViewModelCreator[nameof(BuisnessUnitTableViewModel)];
            CountryTable = tableViewModelCreator[nameof(CountryTableViewModel)];
        }

        public ITableViewModel AreaTable { get; set; }
        public ITableViewModel BuisnessUnitTable { get; set; }
        public ITableViewModel CountryTable { get; set; }

        public override async Task LoadAsync()
        {
            await AreaTable.LoadAsync();
            await BuisnessUnitTable.LoadAsync();
            await CountryTable.LoadAsync();
        }
    }
}
