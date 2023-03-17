using AutoMapper;
using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DbConfigurator.UI.Startup
{
    public class AutoMapperConfig 
    {
        public IMapper Mapper { get; private set; }

        public AutoMapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Priority, PriorityDto>();
                cfg.CreateMap<Country, CountryDto>();
                cfg.CreateMap<BuisnessUnit, BuisnessUnitDto>();
                cfg.CreateMap<Area, AreaDto>();
                cfg.CreateMap<DistributionInformation, DistributionInformationDto>()
                            .ForMember(d => d.RecipientsTo, opt => opt.MapFrom(
                                rg => rg.RecipientsGroup != null && rg.RecipientsGroup.RecipientsTo != null ? rg.RecipientsGroup.RecipientsTo : Enumerable.Empty<Recipient>()))
                            .ForMember(d => d.RecipientsCc, opt => opt.MapFrom(
                                rg => rg.RecipientsGroup != null && rg.RecipientsGroup.RecipientsCc != null ? rg.RecipientsGroup.RecipientsCc : Enumerable.Empty<Recipient>()))
                            .ForMember(d => d.Area, opt => opt.MapFrom(a => a.Area.Name))
                            .ForMember(d => d.BuisnessUnit, opt => opt.MapFrom(a => a.BuisnessUnit.Name))
                            .ForMember(d => d.Country, opt => opt.MapFrom(a => a.Country.Name))
                            .ForMember(d => d.Priority, opt => opt.MapFrom(a => a.Priority.Name));
            });

            Mapper = config.CreateMapper();
        }

    }
}
