namespace DbConfigurator.Model.DTOs.Parser
{
    public class RegionForParserDto
    {
        public RegionForParserDto(string area, string BusinessUnit, string country)
        {
            Area = area;
            BusinessUnit = BusinessUnit;
            Country = country;
        }

        public string Area { get; set; }
        public string BusinessUnit { get; set; }
        public string Country { get; set; }
    }
}
