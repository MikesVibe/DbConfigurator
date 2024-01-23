using DbConfigurator.Core.Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Core.Models
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

        public IEntity CreateCopy()
        {
            return new DistributionInformation
            {
                Id = Id,
                Region = Region,
                Priority = Priority,
                RecipientsCc = new ObservableCollection<Recipient>(RecipientsCc),
                RecipientsTo = new ObservableCollection<Recipient>(RecipientsTo)
            };
        }
    }
}
