using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.DTOs.Creation
{
    public class CountryForCreationDto
    {
        public CountryForCreationDto(int id, string countryName, string countryCode)
        {
            Id = id;
            CountryName = countryName;
            CountryCode = countryCode;
        }

        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }
}
