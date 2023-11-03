using System.Threading.Tasks;

namespace DbConfigurator.UI.Base.Contracts
{
    public interface IMainPanelViewModel
    {
        int Id { get; }

        Task LoadAsync();
        Task RefreshAsync();
        void SetId(int id);
    }
}
