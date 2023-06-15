using DbConfigurator.Model;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Tables;
using Prism.Events;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Panel
{
    public class CreationPanelViewModel : IMainPanelViewModel
    {
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;
        private IDialogService _dialogService;

        public ITableViewModel AreaTableViewModel { get; set; }
        public ITableViewModel BuisnessUnitTableViewModel { get; set; }
        public ITableViewModel CountryTableViewModel { get; set; }

        public bool HasChanges { get; set; }

        public int Id { get; set; }

        public CreationPanelViewModel(IDataModel dataModel,
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            AutoMapperConfig autoMapper
            )
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            _dialogService = dialogService;

            AreaTableViewModel = new AreaTableViewModel(eventAggregator, dialogService, dataModel, autoMapper);
            BuisnessUnitTableViewModel = new BuisnessUnitTableViewModel(eventAggregator, dialogService, dataModel, autoMapper);
            CountryTableViewModel = new CountryTableViewModel(eventAggregator, dialogService, dataModel, autoMapper);
        }

        public async Task LoadAsync()
        {
            await AreaTableViewModel.LoadAsync();
            await BuisnessUnitTableViewModel.LoadAsync();
            await CountryTableViewModel.LoadAsync();
        }
    }
}
