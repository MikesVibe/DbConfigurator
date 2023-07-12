using DbConfigurator.UI.ViewModel;

namespace DbConfigurator.UI.Services.Interfaces
{
    public interface IDialogService
    {
        bool? ShowDialog(IDetailViewModel viewModel);
    }
}
