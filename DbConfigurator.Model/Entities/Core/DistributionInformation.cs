using DbConfigurator.Model.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.Entities.Core
{
    public class DistributionInformation : IEntity
    {
        [Required]
        public int Id { get; set; }
        public Region Region { get; set; }
        public int RegionId { get; set; }

        [Required]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }


        public ICollection<Recipient> RecipientsTo { get; set; } = new Collection<Recipient>();
        public ICollection<Recipient> RecipientsCc { get; set; } = new Collection<Recipient>();
    }
}
