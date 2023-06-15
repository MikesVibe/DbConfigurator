using AutoMapper;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Table;
using System.Linq;

namespace DbConfigurator.UI.Startup
{
    public class AutoMapperConfig
    {
        public IMapper Mapper { get; private set; }

        public AutoMapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Region, RegionDto>();
                cfg.CreateMap<Area, AreaDto>();
                cfg.CreateMap<BuisnessUnit, BuisnessUnitDto>();
                cfg.CreateMap<Country, CountryDto>()
                .ForMember(c => c.CountryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(c => c.CountryCode, opt => opt.MapFrom(src => src.ShortCode));
                cfg.CreateMap<Priority, PriorityDto>();
                cfg.CreateMap<Recipient, RecipientDto>().ReverseMap();
                cfg.CreateMap<DistributionInformation, DistributionInformationDto>()
                            .ForMember(d => d.RecipientsTo, opt => opt.MapFrom(
                                rg => (rg.RecipientsTo != null) ? rg.RecipientsTo : Enumerable.Empty<Recipient>()))
                            .ForMember(d => d.RecipientsCc, opt => opt.MapFrom(
                                rg => (rg.RecipientsCc != null) ? rg.RecipientsCc : Enumerable.Empty<Recipient>()));

                cfg.CreateMap<Area, AreaTableItem>();
            });

            Mapper = config.CreateMapper();
        }

    }
}
