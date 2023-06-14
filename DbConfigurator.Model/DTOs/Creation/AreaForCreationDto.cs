namespace DbConfigurator.Model.DTOs.Creation
{
    public class AreaForCreationDto
    {
        public AreaForCreationDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
