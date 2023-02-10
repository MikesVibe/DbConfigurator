using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class LocationOption
    {
        public LocationOption()
        {
            DistributionInformations = new Collection<DistributionInformation>();
        }
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Descripiton { get; set; }

        public ICollection<DistributionInformation> DistributionInformations { get; set; }

    }
}
