using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConfigurator.Model;
using Microsoft.VisualBasic;

namespace DbConfigurator.Model
{
    public class RecipientsGroup
    {
        public RecipientsGroup()
        {
            Recipients = new Collection<Recipient>();
            DistributionInformations = new Collection<DistributionInformation>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int DestinationFieldId { get; set; } 
        public ICollection<Recipient> Recipients { get; set;}
        public ICollection<DistributionInformation> DistributionInformations { get; set; }

    }
}
