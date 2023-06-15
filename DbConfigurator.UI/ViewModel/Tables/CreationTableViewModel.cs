using DbConfigurator.Model;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Tables;
using Prism.Events;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel
{
    public class CreationTableViewModel : TableViewModelBase
    {
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;
        private IDialogService _dialogService;

        public ITabelViewModel AreaTableViewModel { get; set; }
        public ITabelViewModel BuisnessUnitTableViewModel { get; set; }
        public ITabelViewModel CountryTableViewModel { get; set; }



        public CreationTableViewModel(IDataModel dataModel,
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            AutoMapperConfig autoMapper
            ) : base(eventAggregator)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            _dialogService = dialogService;

            AreaTableViewModel = new AreaTableViewModel(eventAggregator, dialogService, dataModel, autoMapper);
            BuisnessUnitTableViewModel = new BuisnessUnitTableViewModel(eventAggregator, dialogService, dataModel, autoMapper);
            CountryTableViewModel = new CountryTableViewModel(eventAggregator, dialogService, dataModel, autoMapper);
        }

        public override async Task LoadAsync()
        {
            await AreaTableViewModel.LoadAsync();
            await BuisnessUnitTableViewModel.LoadAsync();
            await CountryTableViewModel.LoadAsync();
        }

        protected override bool OnRemoveCanExecute()
        {
            return false;
        }

        protected override void OnRemoveExecute()
        {

        }

        protected override void OnAddExecute()
        {
        }
    }
}
