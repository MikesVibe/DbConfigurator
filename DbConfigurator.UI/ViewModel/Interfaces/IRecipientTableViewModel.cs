using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Interfaces
{
    public interface IRecipientTableViewModel : ITabelViewModel
    {
        Task LoadAsync();
    }
}