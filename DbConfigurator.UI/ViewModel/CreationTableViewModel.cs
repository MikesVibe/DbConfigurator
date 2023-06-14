using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.Entities;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using DbConfigurator.UI.ViewModel.Tables;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel
{
    public class CreationTableViewModel : TableViewModelBase
    {
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;
        private IDialogService _dialogService;

        public ICommand AddCountryCommand { get; set; }
        public ICommand AddBuisnessUnitCommand { get; set; }


        public ITabelViewModel AreaTableViewModel { get; set; }


        public ObservableCollection<CountryDto> Countries { get; set; } = new();
        public ObservableCollection<BuisnessUnitDto> BuisnessUnits { get; set; } = new();

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


            AddBuisnessUnitCommand = new DelegateCommand(OnAddBuisnessUnitExecute);
            AddCountryCommand = new DelegateCommand(OnAddCountryExecute);
        }



        public override async Task LoadAsync()
        {
            var countries = await _dataModel.GetAllCountriesAsync();
            foreach (var country in countries)
            {
                var mapped = _autoMapper.Mapper.Map<CountryDto>(country);
                Countries.Add(mapped);
            }
            var buisnessUnits = await _dataModel.GetAllBuisnessUnitsAsync();

            foreach (var buisnessUnit in buisnessUnits)
            {
                var mapped = _autoMapper.Mapper.Map<BuisnessUnitDto>(buisnessUnit);
                BuisnessUnits.Add(mapped);
            }

            await AreaTableViewModel.LoadAsync();

        }



        private void OnAddBuisnessUnitExecute()
        {
            var addbuisnessUnitViewModel = new AddBuisnessUnitViewModel();

            bool? result = _dialogService.ShowDialog(addbuisnessUnitViewModel);

            if (result == false)
                return;

            string buisnessUnitName = addbuisnessUnitViewModel.BuisnessUnit.Name;
            var buisnessUnit = new BuisnessUnit
            {
                Name = buisnessUnitName
            };
            _dataModel.Add(buisnessUnit);
            _dataModel.SaveChanges();
            var mapped = _autoMapper.Mapper.Map<BuisnessUnitDto>(buisnessUnit);
            BuisnessUnits.Add(mapped);
        }

        private void OnAddCountryExecute()
        {
            var addCountryViewModel = new AddCountryViewModel();

            bool? result = _dialogService.ShowDialog(addCountryViewModel);

            if (result == false)
                return;

            var countryDtoWrapper = addCountryViewModel.Country;
            var countryEntity = new Country
            {
                Name = countryDtoWrapper.CountryName,
                ShortCode = countryDtoWrapper.CountryCode
            };

            _dataModel.Add(countryEntity);
            _dataModel.SaveChanges();
            var mapped = _autoMapper.Mapper.Map<CountryDto>(countryEntity);
            Countries.Add(mapped);
        }

        protected override bool OnRemoveCanExecute()
        {
            return false;
        }

        protected override void OnRemoveExecute()
        {

        }

        protected override void OnAddAreaExecute()
        {
        }
    }
}
