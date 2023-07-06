using Autofac.Features.Indexed;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Tables;
using System.Threading.Tasks;

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
