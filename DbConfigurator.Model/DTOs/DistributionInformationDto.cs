using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.DTOs
{
    public class DistributionInformationDto
    {
        public int Id { get; init; }
        public int AreaId { get; set; }
        public int BuisnessUnitId { get; set; }
        public int CountryId { get; set; }
        public int PriorityId { get; set; }
        public int RecipientsGroupId { get; set; }
        public string Area { get; set; }
        public string BuisnessUnit { get; set; }
        public string Country { get; set; }
        public string Priority { get; set; }
        public ObservableCollection<RecipientDto> RecipientsTo { get; set; } = new ObservableCollection<RecipientDto>();
        public ObservableCollection<RecipientDto> RecipientsCc { get; set; } = new ObservableCollection<RecipientDto>();
    }
}
