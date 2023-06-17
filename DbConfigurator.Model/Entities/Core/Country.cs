using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.Entities.Core
{
    public class Country
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string CountryName { get; set; }

        [Required]
        [MaxLength(3)]
        public string CountryCode { get; set; }


        public ICollection<Region> Regions { get; set; }
        //public ICollection<BuisnessUnit> BuisnessUnits { get; set; }
        //public ICollection<DistributionInformation> DistributionInformations { get; set; }

    }
}
