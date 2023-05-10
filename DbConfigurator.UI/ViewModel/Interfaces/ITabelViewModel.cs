using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Interfaces
{
    public interface ITabelViewModel
    {
        Task LoadAsync();

        bool HasChanges { get; }
        int Id { get; }
    }
}