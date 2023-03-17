using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Wrapper
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
        public int AreaId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue<int>(value);
            }
        }
        public int BuisnessUnitId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue<int>(value);
            }
        }
        public int CountryId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue<int>(value);
            }
        }
        public int PriorityId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue<int>(value);
            }
        }
        public int RecipientsGroupId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue<int>(value);
            }
        }
        public string Area
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue<string>(value);
            }
        }
        public string BuisnessUnit
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue<string>(value);
            }
        }
        public string Country
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue<string>(value);
            }
        }
        public string Priority
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue<string>(value);
            }
        }
        public ObservableCollection<Recipient> RecipientsTo
        {
            get { return GetValue<ObservableCollection<Recipient>>(); }
            set
            {
                SetValue<ObservableCollection<Recipient>>(value);
            }
        }
        public ObservableCollection<Recipient> RecipientsCc
        {
            get { return GetValue<ObservableCollection<Recipient>>(); }
            set
            {
                SetValue<ObservableCollection<Recipient>>(value);
            }
        }
    }
}
