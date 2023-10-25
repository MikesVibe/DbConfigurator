using System.Threading.Tasks;

namespace DbConfigurator.UI.Base.Contracts
{
    public interface IMainPanelViewModel
    {
        int Id { get; }

        Task LoadAsync();
        void SetId(int id);
    }
}
