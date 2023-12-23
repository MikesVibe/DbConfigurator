using System.Threading.Tasks;

namespace DbConfigurator.UI.Base.Contracts
{
    public interface INavigationPanelViewModel
    {
        Task LoadAsync();
    }
}