using DbConfigurator.DataAccess.DTOs.AreaDtos;
using DbConfigurator.DataAccess.DTOs.BusinessUnitDtos;
using DbConfigurator.DataAccess.DTOs.CountryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.DTOs.RegionDtos
{
    public class UpdateRegionDto
    {
        public int Id { get; init; }
        public int AreaId { get; set; }
        public int BusinessUnitId { get; set; }
        public int CountryId { get; set; }
    }
}
