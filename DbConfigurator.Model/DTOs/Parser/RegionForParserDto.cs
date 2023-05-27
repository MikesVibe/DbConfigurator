using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.DTOs.Parser
{
    public class RegionForParserDto
    {
        public RegionForParserDto(string area, string buisnessUnit, string country)
        {
            Area = area;
            BuisnessUnit = buisnessUnit;
            Country = country;
        }

        public string Area { get; set; }
        public string BuisnessUnit { get; set; }
        public string Country { get; set; }
    }
}
