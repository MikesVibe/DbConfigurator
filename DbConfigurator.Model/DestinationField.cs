using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class DestinationField
    {
        public DestinationField() 
        {
            RecipientsGroups = new Collection<RecipientsGroup>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(3)]
        public string Name { get; set; }
        public ICollection<RecipientsGroup> RecipientsGroups { get; set; }
    }
}
