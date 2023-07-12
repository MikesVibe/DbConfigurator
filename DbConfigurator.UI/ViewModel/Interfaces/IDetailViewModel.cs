using System;

namespace DbConfigurator.UI.ViewModel
{
    public interface IDetailViewModel
    {
        Action<bool>? CloseAction { get; set; }
    }
}