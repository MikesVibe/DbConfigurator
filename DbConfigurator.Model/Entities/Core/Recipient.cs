using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.Entities.Core
{
    public class Recipient
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public ICollection<DistributionInformation> RecipientGroupTo { get; set; }
        public ICollection<DistributionInformation> RecipientGroupCc { get; set; }
    }
}
