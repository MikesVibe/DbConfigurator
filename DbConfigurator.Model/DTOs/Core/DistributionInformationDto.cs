using System.Collections.ObjectModel;

namespace DbConfigurator.Model.DTOs.Core
{
    public class DistributionInformationDto
    {
        public int Id { get; init; }

        public RegionDto Region { get; set; }
        public PriorityDto Priority { get; set; }
        public ObservableCollection<RecipientDto> RecipientsTo { get; set; } = new ObservableCollection<RecipientDto>();
        public ObservableCollection<RecipientDto> RecipientsCc { get; set; } = new ObservableCollection<RecipientDto>();
    }
}
