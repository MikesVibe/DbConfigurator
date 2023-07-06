using Autofac.Features.Indexed;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Tables;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Panel
{
    public class RecipientPanelViewModel : PanelViewModelBase
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
