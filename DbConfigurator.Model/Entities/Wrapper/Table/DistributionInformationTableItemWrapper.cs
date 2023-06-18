using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Table;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.Entities.Wrapper.Table
{
    public class DistributionInformationTableItemWrapper : TableItemWrapper<DistributionInformationTableItem>, ITableItemWrapper
    {
        public DistributionInformationTableItemWrapper(DistributionInformationTableItem model) : base(model)
        {
        }

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public RegionDto Region
        {
            get { return GetValue<RegionDto>(); }
            set { SetValue(value); }
        }
        public PriorityDto Priority
        {
            get { return GetValue<PriorityDto>(); }
            set { SetValue(value); }
        }
        public ObservableCollection<RecipientDto> RecipientsTo
        {
            get { return GetValue<ObservableCollection<RecipientDto>>(); }
        }
        public ObservableCollection<RecipientDto> RecipientsCc
        {
            get { return GetValue<ObservableCollection<RecipientDto>>(); }
        }
    }
}
