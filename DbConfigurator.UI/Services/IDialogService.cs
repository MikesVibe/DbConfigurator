using DbConfigurator.UI.ViewModel;

namespace DbConfigurator.UI.Services
{
    public interface IDialogService
    {
        bool? ShowDialog(IEditingViewModel viewModel);
    }
}
