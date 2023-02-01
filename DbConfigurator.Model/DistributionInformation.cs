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
            RecipientsGroup_Collection = new Collection<RecipientsGroup>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int BusinessUnitId { get; set; }
        public BuisnessUnit BuisnessUnit { get; set; }

        [Required]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }

        public ICollection<RecipientsGroup> RecipientsGroup_Collection { get; set; }

    }
}
