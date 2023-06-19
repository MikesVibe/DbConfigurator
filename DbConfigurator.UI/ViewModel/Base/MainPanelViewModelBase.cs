using DbConfigurator.UI.ViewModel.Interfaces;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Base
{
    public abstract class MainPanelViewModelBase : IMainPanelViewModel
    {
        public int Id { get; set; }

        public abstract Task LoadAsync();
    }
}
