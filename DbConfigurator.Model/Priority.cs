using DbConfigurator.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class Priority
    {
        public Priority()
        {
            DistributionInformations = new Collection<DistributionInformation>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(6)]
        public string Name { get; set; }

        public ICollection<DistributionInformation> DistributionInformations { get; set; }
    }
}
