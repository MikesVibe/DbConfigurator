using Autofac.Features.Indexed;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Regions;
using DbConfigurator.UI.ViewModel.Base;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Panels.Region
{
    public class RegionPanelViewModel : PanelViewModelBase
    {
        public RegionPanelViewModel(
            IIndex<string, ITableViewModel> tableViewModelCreator,
            IStatusService statusService) 
            : base(statusService)
        {
            RegionTable = tableViewModelCreator[nameof(RegionTableViewModel)];
        }

        public ITableViewModel RegionTable { get; set; }

        protected override async Task LoadDataAsync()
        {
            await RegionTable.LoadAsync();
        }
        public override async Task RefreshAsync()
        {
            await RegionTable.Refresh();
        }
    }
}
