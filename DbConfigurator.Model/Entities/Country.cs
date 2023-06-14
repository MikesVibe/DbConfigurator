using DbConfigurator.Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model
{
    public class Country
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(3)]
        public string ShortCode { get; set; }


        public ICollection<Region> Regions { get; set; }
        //public ICollection<BuisnessUnit> BuisnessUnits { get; set; }
        //public ICollection<DistributionInformation> DistributionInformations { get; set; }

    }
}
