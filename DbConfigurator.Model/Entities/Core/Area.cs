using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.Entities.Core
{
    public class Area
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        //public ICollection<BuisnessUnit> BuisnessUnits { get; set; }
        //public ICollection<DistributionInformation> DistributionInformations { get; set; }
        public ICollection<Region> Regions { get; set; }

    }
}
