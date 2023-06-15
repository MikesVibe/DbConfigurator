using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Table;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class CountryTableViewModel : TableViewModelBase, ITabelViewModel
    {
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;
        private IDialogService _dialogService;

        public CountryTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IDataModel dataModel, AutoMapperConfig autoMapper)
            : base(eventAggregator)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            _dialogService = dialogService;
        }

        public override async Task LoadAsync()
        {
            var countries = await _dataModel.GetAllCountriesAsync();
            foreach (var country in countries)
            {
                var mapped = _autoMapper.Mapper.Map<CountryTableItem>(country);
                Items.Add(mapped);
            }
        }

        protected override void OnAddExecute()
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
            var mapped = _autoMapper.Mapper.Map<CountryTableItem>(countryEntity);
            Items.Add(mapped);
        }

        protected override void OnRemoveExecute()
        {
            if (SelectedItem is null)
                return;

            var country = _dataModel.GetCountriesById(SelectedItem.Id);
            if (country is null)
            {
                //Log some error mesage here
                return;
            }

            _dataModel.Remove(country);
            _dataModel.SaveChanges();
            base.OnRemoveExecute();
        }
    }
}
