using DbConfigurator.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Core.Models
{
    public class Region : IEntity
    {
        [Required]
        public int Id { get; set; }
        public Area Area { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
        public Country Country { get; set; }

        public IEntity CreateCopy()
        {
            return new Region { Id = Id, Area = Area, BusinessUnit = BusinessUnit, Country = Country };
        }
    }
}
