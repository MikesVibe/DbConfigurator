using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Models
{
    internal class BuisnessUnitCountryWithIdDto
    {
        public BuisnessUnitCountryWithIdDto(int id, int buisnessUnitId, int countryId)
        {
            Id = id;
            BuisnessUnitId = buisnessUnitId;
            CountryId = countryId;
        }

        public int Id { get; set; }
        public int BuisnessUnitId { get; set; }
        public int CountryId { get; set; }
    }
}
