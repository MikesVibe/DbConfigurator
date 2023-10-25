using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Features.Countries.Services;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System.ComponentModel;

namespace DbConfigurator.UI.Features.Countries
{
    public class CountryDetailViewModel : DetailViewModelBase<ICountryService, Country>, IDetailViewModel, INotifyPropertyChanged
    {
        public CountryDetailViewModel(ICountryService countryService, IEventAggregator eventAggregator) : base(countryService, eventAggregator)
        {
            Title = "Country";
            ViewWidth = 660;
            ViewHeight = 420;
        }

        protected override void OnCreate()
        {
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<CreateCountryEvent>()
                  .Publish(
                new CreateCountryEventArgs
                {
                    Entity = new Country
                    {
                        Id = EntityDto.Id,
                        CountryName = EntityDto.CountryName,
                        CountryCode = EntityDto.CountryCode
                    }
                });
        }

        protected override void OnUpdate()
        {
            if (EntityDto is null)
                return;

            EventAggregator.GetEvent<EditCountryEvent>()
                  .Publish(
                new EditCountryEventArgs
                {
                    Entity = new Country
                    {
                        Id = EntityDto.Id,
                        CountryName = EntityDto.CountryName,
                        CountryCode = EntityDto.CountryCode
                    }
                });
        }
    }
}
