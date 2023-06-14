namespace DbConfigurator.Model.DTOs.Creation
{
    public class BuisnessUnitForCreationDto
    {
        public BuisnessUnitForCreationDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
