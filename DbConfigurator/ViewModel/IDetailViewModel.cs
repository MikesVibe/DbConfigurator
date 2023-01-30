using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public interface IDetailViewModel
    {
        bool HasChanges { get; }
        int Id { get; }
    }
}