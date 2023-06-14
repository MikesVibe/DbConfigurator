using DbConfigurator.Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model
{
    public class DistributionInformation
    {
        public DistributionInformation()
        {
        }
        public DistributionInformation(Region region, Priority priority)
        {
            Region = region;
            Priority = priority;
        }
        [Required]
        public int Id { get; set; }
        public Region Region { get; set; }
        public int RegionId { get; set; }

        [Required]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }


        public ICollection<Recipient> RecipientsTo { get; set; }
        public ICollection<Recipient> RecipientsCc { get; set; }
    }
}
