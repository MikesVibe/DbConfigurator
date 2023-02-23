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
        public RecipientsGroup() { }
        public RecipientsGroup(string name)
        {
            Name = name;
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? DistributionInformationId { get; set; }
        public DistributionInformation? DistributionInformation { get; set; }
        public ICollection<Recipient> Recipients { get; set; }

    }
}
