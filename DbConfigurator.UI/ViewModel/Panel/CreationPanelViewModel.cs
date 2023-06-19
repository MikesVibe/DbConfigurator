using Autofac.Features.Indexed;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Tables;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Panel
{
    public class CreationPanelViewModel : MainPanelViewModelBase, IMainPanelViewModel
    {
        public ITableViewModel AreaTable { get; set; }
        public ITableViewModel BuisnessUnitTable { get; set; }
        public ITableViewModel CountryTable { get; set; }

        public CreationPanelViewModel(
            IIndex<string, ITableViewModel> tableViewModelCreator
            )
        {
            AreaTable = tableViewModelCreator[nameof(AreaTableViewModel)];
            BuisnessUnitTable = tableViewModelCreator[nameof(BuisnessUnitTableViewModel)];
            CountryTable = tableViewModelCreator[nameof(CountryTableViewModel)];
        }

        public override async Task LoadAsync()
        {
            await AreaTable.LoadAsync();
            await BuisnessUnitTable.LoadAsync();
            await CountryTable.LoadAsync();
        }
    }
}
