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
        //public int AreaId
        //{
        //    get { return GetValue<int>(); }
        //    set
        //    {
        //        SetValue<int>(value);
        //    }
        //}
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

        //public int AreaId
        //{
        //    get { return GetValue<int>(); }
        //    set
        //    {
        //        SetValue<int>(value);
        //    }
        //}
        //public int BuisnessUnitId
        //{
        //    get { return GetValue<int>(); }
        //    set
        //    {
        //        SetValue<int>(value);
        //    }
        //}
        //public int CountryId
        //{
        //    get { return GetValue<int>(); }
        //    set
        //    {
        //        SetValue<int>(value);
        //    }
        //}
        //public int PriorityId
        //{
        //    get { return GetValue<int>(); }
        //    set
        //    {
        //        SetValue<int>(value);
        //    }
        //}
        //public int RecipientsGroupId
        //{
        //    get { return GetValue<int>(); }
        //    set
        //    {
        //        SetValue<int>(value);
        //    }
        //}
        //public string Area
        //{
        //    get { return GetValue<string>(); }
        //    set
        //    {
        //        SetValue<string>(value);
        //    }
        //}
        //public string BuisnessUnit
        //{
        //    get { return GetValue<string>(); }
        //    set
        //    {
        //        SetValue<string>(value);
        //    }
        //}
        //public string Country
        //{
        //    get { return GetValue<string>(); }
        //    set
        //    {
        //        SetValue<string>(value);
        //    }
        //}
        //public string Priority
        //{
        //    get { return GetValue<string>(); }
        //    set
        //    {
        //        SetValue<string>(value);
        //    }
        //}
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
