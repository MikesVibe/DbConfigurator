using Autofac.Features.Indexed;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Tables;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Panel
{
    public class DistributionInformationPanelViewModel : PanelViewModelBase
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
