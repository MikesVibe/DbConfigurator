using Autofac.Features.Indexed;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Areas;
using DbConfigurator.UI.Features.BusinessUnits;
using DbConfigurator.UI.Features.Countries;
using DbConfigurator.UI.ViewModel.Base;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Panels.Creation
{
    public class CreationPanelViewModel : PanelViewModelBase, IMainPanelViewModel
    {
        public CreationPanelViewModel(
            IIndex<string, ITableViewModel> tableViewModelCreator,
            IStatusService statusService
            ) : base(statusService)
        {
            AreaTable = tableViewModelCreator[nameof(AreaTableViewModel)];
            BusinessUnitTable = tableViewModelCreator[nameof(BusinessUnitTableViewModel)];
            CountryTable = tableViewModelCreator[nameof(CountryTableViewModel)];
        }

        public ITableViewModel AreaTable { get; set; }
        public ITableViewModel BusinessUnitTable { get; set; }
        public ITableViewModel CountryTable { get; set; }

        protected override async Task LoadDataAsync()
        {
            await AreaTable.LoadAsync();
            await BusinessUnitTable.LoadAsync();
            await CountryTable.LoadAsync();
        }

        public override async Task RefreshAsync()
        {
            await AreaTable.Refresh();
            await BusinessUnitTable.Refresh();
            await CountryTable.Refresh();
        }
    }
}
