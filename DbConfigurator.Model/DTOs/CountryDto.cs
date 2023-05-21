using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.DTOs
{
    public class CountryDto
    {
        public int Id { get; init; }
        public string CountryName { get; set; } = string.Empty;
        public string CountryShortCode { get; set; } = string.Empty;
        public string BuisnessUnitName { get; set; } = string.Empty;
        public int BuisnessUnitId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public int AreaId { get; set; }

    }
}
