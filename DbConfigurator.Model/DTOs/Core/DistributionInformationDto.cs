using DbConfigurator.Model.Contracts;
using System.Collections.ObjectModel;

namespace DbConfigurator.Model.DTOs.Core
{
    public class DistributionInformationDto : IEntityDto
    {
        public DistributionInformationDto()
        { }
        public DistributionInformationDto(DistributionInformationDto other)
        {
            if (other is null)
                return;

            this.Id = other.Id;

            this.Region = other.Region;
            this.Priority = other.Priority;

            this.RecipientsTo = new ObservableCollection<RecipientDto>();
            foreach (var recipient in other.RecipientsTo)
            {
                this.RecipientsTo.Add(recipient);
            }

            this.RecipientsCc = new ObservableCollection<RecipientDto>();
            foreach (var recipient in other.RecipientsCc)
            {
                this.RecipientsCc.Add(recipient);
            }
        }

        public int Id { get; init; }

        public RegionDto Region { get; set; }
        public PriorityDto Priority { get; set; }
        public ObservableCollection<RecipientDto> RecipientsTo { get; set; } = new ObservableCollection<RecipientDto>();
        public ObservableCollection<RecipientDto> RecipientsCc { get; set; } = new ObservableCollection<RecipientDto>();
    }
}
