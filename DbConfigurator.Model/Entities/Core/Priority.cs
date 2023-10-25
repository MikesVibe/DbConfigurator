using DbConfigurator.Model.Contracts;

namespace DbConfigurator.Model.Entities.Core
{
    public class Priority : IEntity
    {
        //[Required]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(6)]
        public string Name { get; set; }
    }
}
