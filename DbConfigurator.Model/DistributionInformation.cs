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
            ToRecipientsGroup = new RecipientsGroup("TO");
            CcRecipientsGroup = new RecipientsGroup("CC");
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        [Required]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
        public int? ToRecipientsGroupId { get; set; }
        public RecipientsGroup? ToRecipientsGroup { get; set; }
        public int? CcRecipientsGroupId { get; set; }
        public RecipientsGroup? CcRecipientsGroup { get; set; }

    }
}
