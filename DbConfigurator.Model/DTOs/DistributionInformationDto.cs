using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.DTOs
{
    public class DistributionInformationDto
    {
        public DistributionInformationDto()
        {

        }
        public DistributionInformationDto(DistributionInformation distributionInformation)
        {
        }
        public int Id { get; }
        public int AreaId { get; set; }
        public int BuisnessUnitId { get; set; }
        public int CountryId { get; set; }
        public int PriorityId { get; set; }
        public int RecipientsGroupId { get; set; }
        public IEnumerable<Recipient> RecipientsTo { get; set; } = Enumerable.Empty<Recipient>();
        public IEnumerable<Recipient> RecipientsCc { get; set; } = Enumerable.Empty<Recipient>();
    }
}
