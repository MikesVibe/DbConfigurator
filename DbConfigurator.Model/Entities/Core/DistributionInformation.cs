using DbConfigurator.Model.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.Entities.Core
{
    public class DistributionInformation : IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Region Region { get; set; }
        [Required]
        public Priority Priority { get; set; }
        public ObservableCollection<Recipient> RecipientsTo { get; set; } = new ObservableCollection<Recipient>();
        public ObservableCollection<Recipient> RecipientsCc { get; set; } = new ObservableCollection<Recipient>();
    }
}
