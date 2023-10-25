using DbConfigurator.Model.Contracts;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
using System.Collections.ObjectModel;

namespace DbConfigurator.Model.DTOs.Wrapper
{
    public class DistributionInformationWrapper : ModelWrapper<DistributionInformation>, IWrapperWithId
    {
        public DistributionInformationWrapper(DistributionInformation model) : base(model)
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
        public Region Region
        {
            get { return GetValue<Region>(); }
            set
            {
                SetValue(value);
            }
        }
        public Priority Priority
        {
            get { return GetValue<Priority>(); }
            set
            {
                SetValue(value);
            }
        }

        public ObservableCollection<Recipient> RecipientsTo
        {
            get { return GetValue<ObservableCollection<Recipient>>(); }
            set
            {
                SetValue(value);
            }
        }
        public ObservableCollection<Recipient> RecipientsCc
        {
            get { return GetValue<ObservableCollection<Recipient>>(); }
            set
            {
                SetValue(value);
            }
        }
    }
}
