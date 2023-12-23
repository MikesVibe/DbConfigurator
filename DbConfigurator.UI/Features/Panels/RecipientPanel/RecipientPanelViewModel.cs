using Autofac.Features.Indexed;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Recipients;
using DbConfigurator.UI.ViewModel.Base;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Panels.RecipientPanel
{
    public class RecipientPanelViewModel : PanelViewModelBase
    {
        public RecipientPanelViewModel(
            IIndex<string, ITableViewModel> tableViewModelCreator,
            IStatusService statusService) 
            : base(statusService)
        {
            RecipientTable = tableViewModelCreator[nameof(RecipientTableViewModel)];
        }

        public ITableViewModel RecipientTable { get; set; }

        protected override async Task LoadDataAsync()
        {
            await RecipientTable.LoadAsync();
        }
        public override async Task RefreshAsync()
        {
            await RecipientTable.Refresh();
        }
    }
}
