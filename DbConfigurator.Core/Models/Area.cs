using DbConfigurator.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Core.Models
{
    public class Area : IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public IEntity CreateCopy()
        {
            return new Area { Id = Id, Name = Name };
        }
    }
}
