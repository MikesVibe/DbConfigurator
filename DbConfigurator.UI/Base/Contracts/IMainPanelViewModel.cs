using System.Threading.Tasks;

namespace DbConfigurator.UI.Base.Contracts
{
    public interface IMainPanelViewModel
    {
        int Id { get; }

        Task LoadAsync();
        Task Refresh();
        void SetId(int id);
    }
}
