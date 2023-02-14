using DbConfigurator.UI.Data.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class DataModel : IDataModel
    {
        public DataModel(
            IDistributionInformationRepository distributionInformationRepository,
            ICountryRepository countryRepository
            )
        {
            _distributionInformationRepository = distributionInformationRepository;
            _countryRepository = countryRepository;


            LoadDataFromDatabase();
        }

        private async void LoadDataFromDatabase()
        {
            DistributionInformations = await _distributionInformationRepository.GetAllAsync();
            Areas = await _countryRepository.GetAllAreasAsync();
            BuisnessUnits = await _countryRepository.GetAllBuisnessUnitsAsync();
            Countries = await _countryRepository.GetAllCountriesAsync();
            Priorities = await _distributionInformationRepository.GetAllPrioritiesAsync();

        }

        public void SaveChangesAsync()
        {
            _distributionInformationRepository.SaveAsync();
        }

        public IEnumerable<DistributionInformation> DistributionInformations { get; set; }
        public IEnumerable<Area> Areas { get; set; }
        public IEnumerable<BuisnessUnit> BuisnessUnits { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<Priority> Priorities { get; set; }


        public bool HasChanges 
        { 
            get { return _distributionInformationRepository.HasChanges(); }
        }

        private IDistributionInformationRepository _distributionInformationRepository;
        private ICountryRepository _countryRepository;
    }
}
