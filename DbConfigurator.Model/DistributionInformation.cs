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
        public DistributionInformation(Area area, BuisnessUnit buisnessUnit, Country country, Priority priority) 
        {
            Area = area;
            BuisnessUnit = buisnessUnit;  
            Country = country;
            Priority = priority;
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public int AreaId { get; set; }
        public Area Area { get; set; }
        [Required]
        public int BuisnessUnitId { get; set; }
        public BuisnessUnit BuisnessUnit { get; set; }
        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        [Required]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
        public int? RecipientsGroupId { get; set; }
        public RecipientGroup? RecipientsGroup { get; set; }

    }
}
