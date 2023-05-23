using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.DTOs
{
    public class RegionDto
    {
        public int Id { get; init; }

        public AreaDto Area { get; set; }
        public BuisnessUnitDto BuisnessUnit { get; set; }
        public CountryDto Country { get; set; }

        //public int CountryId { get; set; }
        //public string CountryName { get; set; } = string.Empty;
        //public string CountryShortCode { get; set; } = string.Empty;
        //public int BuisnessUnitId { get; set; }
        //public string BuisnessUnitName { get; set; } = string.Empty;
        //public int AreaId { get; set; }
        //public string AreaName { get; set; } = string.Empty;

    }
}
