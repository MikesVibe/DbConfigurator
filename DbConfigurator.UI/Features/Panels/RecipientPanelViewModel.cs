using Autofac.Features.Indexed;
using DbConfigurator.UI.Features.Recipients;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Panel
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
