using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Printing;

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
