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
