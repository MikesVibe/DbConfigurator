using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Interfaces
{
    public interface IDetailViewModel
    {
        bool HasChanges { get; }
        int Id { get; }
    }
}