using Autofac.Features.Indexed;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.DistributionInformations;
using DbConfigurator.UI.ViewModel.Base;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Panels.DistributionInformation
{
    public class DistributionInformationPanelViewModel : PanelViewModelBase, IDistributionInformationPanelViewModel
    {
        public DistributionInformationPanelViewModel(
            IIndex<string, ITableViewModel> tableViewModelCreator,
            IStatusService statusService
            ) : base(statusService)
        {
            DistributionInformationTable = tableViewModelCreator[nameof(DistributionInformationTableViewModel)];
        }

        public ITableViewModel DistributionInformationTable { get; set; }

        protected override async Task LoadDataAsync()
        {
            if (DistributionInformationTable is null)
                return;

            await DistributionInformationTable.LoadAsync();
        }
        public override async Task RefreshAsync()
        {
            await DistributionInformationTable.Refresh();
        }
    }
}
