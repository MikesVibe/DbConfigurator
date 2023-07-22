using Autofac.Features.Indexed;
using DbConfigurator.UI.Features.Regions;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Panels.Region
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
