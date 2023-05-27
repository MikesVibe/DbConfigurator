using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConfigurator.Model.DTOs.Creation;

namespace DbConfigurator.Model.DTOs.Parser
{
    public class AreaBuisnessUnitForParserDto 
    {
        public AreaBuisnessUnitForParserDto(string area, string buisnessUnit)
        {
            Area = area;
            BuisnessUnit = buisnessUnit;
        }

        public string Area { get; set; }
        public string BuisnessUnit { get; set; }

    }
}
