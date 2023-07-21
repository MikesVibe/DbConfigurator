using System;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public interface IDetailViewModel
    {
        Action<bool>? CloseAction { get; set; }
        Task LoadAsync(int entityId);
    }
}