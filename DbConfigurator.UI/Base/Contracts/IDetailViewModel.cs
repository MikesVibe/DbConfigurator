using DbConfigurator.Core.Contracts;
using System;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Base.Contracts
{
    public interface IDetailViewModel
    {
        Action<bool>? CloseAction { get; set; }
        Task LoadAsync(IEntity entity);
        Task LoadAsync();
    }
}