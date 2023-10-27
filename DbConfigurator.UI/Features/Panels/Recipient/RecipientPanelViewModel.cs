using Autofac.Features.Indexed;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Recipients;
using DbConfigurator.UI.ViewModel.Base;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Panels.Recipient
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
        public override async Task Refresh()
        {
            await RecipientTable.Refresh();
        }
    }
}
