using System;

namespace DbConfigurator.UI.ViewModel
{
    public interface IEditingViewModel
    {
        Action<bool>? CloseAction { get; set; }
    }
}