using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Wrapper;
using System.Collections.ObjectModel;

namespace DbConfigurator.Model.DTOs.Wrapper
{
    public class DistributionInformationDtoWrapper : ModelWrapper<DistributionInformationDto>, IWrapperWithId
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
