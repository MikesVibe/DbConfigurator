using  DbConfigurator.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace  DbConfigurator.Core.Entities
{
    public class Recipient : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(250)]
        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(250)]
        public string Email { get; set; }

        public IEntity CreateCopy()
        {
            return new Recipient { Id = Id, FirstName = FirstName, LastName = LastName, Email = Email };
        }
    }
}
