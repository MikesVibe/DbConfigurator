namespace DbConfigurator.Model.DTOs.Core
{
    public class RegionDto
    {
        public int Id { get; init; }
        public AreaDto Area { get; set; }
        public BusinessUnitDto BusinessUnit { get; set; }
        public CountryDto Country { get; set; }
    }
}
