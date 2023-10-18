namespace DbConfigurator.Model.DTOs.Creation
{
    public class BusinessUnitForCreationDto
    {
        public BusinessUnitForCreationDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
