using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Interfaces
{
    public interface IMainPanelViewModel
    {
        int Id { get; }

        Task LoadAsync();
        void SetId(int id);
    }
}
