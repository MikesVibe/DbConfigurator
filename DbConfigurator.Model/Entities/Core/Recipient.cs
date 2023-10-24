using DbConfigurator.Model.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.Entities.Core
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
    }
}
