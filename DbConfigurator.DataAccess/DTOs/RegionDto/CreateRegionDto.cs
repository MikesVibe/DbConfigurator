using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.DTOs.RegionDtos
{
    public class CreateRegionDto
    {
        public int AreaId { get; set; }
        public int BusinessUnitId { get; set; }
        public int CountryId { get; set; }
    }
}
