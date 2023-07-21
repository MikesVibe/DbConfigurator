using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Detail;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class CountryTableViewModel : TableViewModelBase<CountryDtoWrapper, CountryDto, ICountryService>, ITableViewModel
    {
        public CountryTableViewModel(IEventAggregator eventAggregator, IWindowService dialogService, ICountryService dataService, AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService, dataService)
        {
        }

        public override async Task LoadAsync()
        {
            var countries = await DataService.GetAllAsync();
            foreach (var country in countries)
            {
                var wrapped = new CountryDtoWrapper(country);
                Items.Add(wrapped);
            }
        }
        protected override void OnAddExecute()
        {
            var detailsViewModel = new CountryDetailViewModel();

            bool? result = DialogService.ShowDialog(detailsViewModel);
            if (result == false)
                return;

            var dto = DataService.Add(detailsViewModel.Country.Model);
            var wrapped = new CountryDtoWrapper(dto);
            Items.Add(wrapped);
        }
        protected override void OnEditExecute()
        {
            var detailViewModel = new CountryDetailViewModel();
            detailViewModel.Country = new CountryDtoWrapper(SelectedItem!.Model);

            bool? result = DialogService.ShowDialog(detailViewModel);
            if (result == false)
                return;

            var status = DataService.Update(SelectedItem!.Model);

            SelectedItem.CountryName = SelectedItem!.Model.CountryName;
            SelectedItem.CountryCode = SelectedItem!.Model.CountryCode;
        }
    }
}
