namespace DbConfigurator.Model.DTOs.Parser
{
    public class AreaBusinessUnitForParserDto
    {
        public AreaBusinessUnitForParserDto(string area, string BusinessUnit)
        {
            Area = area;
            BusinessUnit = BusinessUnit;
        }

        public string Area { get; set; }
        public string BusinessUnit { get; set; }

    }
}
