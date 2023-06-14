using DbConfigurator.Model.DTOs;
using System.Collections.ObjectModel;

namespace DbConfigurator.Model.Wrapper
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
                SetValue<int>(value);
            }
        }
        public RegionDto Region
        {
            get { return GetValue<RegionDto>(); }
            set
            {
                SetValue<RegionDto>(value);
            }
        }
        public PriorityDto Priority
        {
            get { return GetValue<PriorityDto>(); }
            set
            {
                SetValue<PriorityDto>(value);
            }
        }

        public ObservableCollection<RecipientDto> RecipientsTo
        {
            get { return GetValue<ObservableCollection<RecipientDto>>(); }
            set
            {
                SetValue<ObservableCollection<RecipientDto>>(value);
            }
        }
        public ObservableCollection<RecipientDto> RecipientsCc
        {
            get { return GetValue<ObservableCollection<RecipientDto>>(); }
            set
            {
                SetValue<ObservableCollection<RecipientDto>>(value);
            }
        }
    }
}
