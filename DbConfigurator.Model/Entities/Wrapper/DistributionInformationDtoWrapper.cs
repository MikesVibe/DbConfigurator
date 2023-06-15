using DbConfigurator.Model.DTOs.Core;
using System.Collections.ObjectModel;

namespace DbConfigurator.Model.Entities.Wrapper
{
    public class DistributionInformationDtoWrapper : ModelWrapper<DistributionInformationDto>
    {
        public DistributionInformationDtoWrapper(DistributionInformationDto model) : base(model)
        {
        }

        public int Id
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }
        public RegionDto Region
        {
            get { return GetValue<RegionDto>(); }
            set
            {
                SetValue(value);
            }
        }
        public PriorityDto Priority
        {
            get { return GetValue<PriorityDto>(); }
            set
            {
                SetValue(value);
            }
        }

        public ObservableCollection<RecipientDto> RecipientsTo
        {
            get { return GetValue<ObservableCollection<RecipientDto>>(); }
            set
            {
                SetValue(value);
            }
        }
        public ObservableCollection<RecipientDto> RecipientsCc
        {
            get { return GetValue<ObservableCollection<RecipientDto>>(); }
            set
            {
                SetValue(value);
            }
        }
    }
}
