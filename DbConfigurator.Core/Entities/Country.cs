using  DbConfigurator.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace  DbConfigurator.Core.Entities
{
    public class Country : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string CountryName { get; set; }

        [Required]
        [MaxLength(3)]
        public string CountryCode { get; set; }

        public IEntity CreateCopy()
        {
            return new Country { Id = Id, CountryName = CountryName, CountryCode = CountryCode };
        }
    }
}
