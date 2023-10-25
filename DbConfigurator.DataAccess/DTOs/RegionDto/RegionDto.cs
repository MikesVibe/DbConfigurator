using DbConfigurator.DataAccess.DTOs.AreaDtos;
using DbConfigurator.DataAccess.DTOs.BusinessUnitDtos;
using DbConfigurator.DataAccess.DTOs.CountryDtos;

namespace DbConfigurator.DataAccess.DTOs.RegionDtos
{
    public class RegionDto
    {
        public int Id { get; init; }
        public AreaDto Area { get; set; }
        public BusinessUnitDto BusinessUnit { get; set; }
        public CountryDto Country { get; set; }
    }
}
