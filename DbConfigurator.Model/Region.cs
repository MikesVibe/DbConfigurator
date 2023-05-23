using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class Region
    {
        [Required]
        public int Id { get; set; }
        
        public Area Area { get; set; }
        public int AreaId { get; set; }
        public BuisnessUnit BuisnessUnit { get; set; }
        public int BuisnessUnitId { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }

        public ICollection<DistributionInformation> DistributionInformations { get; set; }

    }
}
