using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class CountryLookup
    {
        public CountryLookup(Country countryModel)
        {
            if (countryModel == null)
                return;
            _countryModel = countryModel;

            //
            //throw new Exception("Change this class to DTO");
            try
            {
                Id = countryModel.Id;
                Area = countryModel.BuisnessUnits.First().Areas.First().Name;
                AreaId = countryModel.BuisnessUnits.First().Areas.First().Id;
                BuisnessUnit = countryModel.BuisnessUnits.First().Name;
                BuisnessUnitId = countryModel.BuisnessUnits.First().Id;
                ShortCode = countryModel.ShortCode;
                Country = countryModel.Name;
                CountryId = countryModel.Id;
            }
            catch (Exception ex)
            {

            }


        }

        private Country _countryModel;

        public int Id { get; }
        public string Area { get; set; }
        public int AreaId { get; set; }
        public string BuisnessUnit { get; set; }
        public int BuisnessUnitId { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        public string ShortCode { get; set; }
    }
}
