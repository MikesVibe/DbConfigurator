using AutoMapper;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using System.Collections.Generic;
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
                cfg.CreateMap<Area, AreaDto>().ReverseMap();
                cfg.CreateMap<BuisnessUnit, BuisnessUnitDto>().ReverseMap();
                cfg.CreateMap<Country, CountryDto>().ReverseMap();
                cfg.CreateMap<Priority, PriorityDto>().ReverseMap();
                cfg.CreateMap<Recipient, RecipientDto>().ReverseMap();
                cfg.CreateMap<DistributionInformation, DistributionInformationDto>()
                            .ForMember(d => d.RecipientsTo, opt => opt.MapFrom(
                                rg => (rg.RecipientsTo != null) ? rg.RecipientsTo : Enumerable.Empty<Recipient>()))
                            .ForMember(d => d.RecipientsCc, opt => opt.MapFrom(
                                rg => (rg.RecipientsCc != null) ? rg.RecipientsCc : Enumerable.Empty<Recipient>()));

                cfg.CreateMap<RegionDto, Region>()
                    .ForMember(dest => dest.Area, opt => opt.Ignore())
                    .ForMember(dest => dest.BuisnessUnit, opt => opt.Ignore())
                    .ForMember(dest => dest.Country, opt => opt.Ignore());

                cfg.CreateMap<DistributionInformationDto, DistributionInformation>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Region, opt => opt.Ignore())
                    .ForMember(dest => dest.Priority, opt => opt.Ignore())
                    .ForMember(dest => dest.RecipientsTo, opt => opt.MapFrom(src => src.RecipientsTo))
                    .ForMember(dest => dest.RecipientsCc, opt => opt.MapFrom(src => src.RecipientsCc));
            });

            Mapper = config.CreateMapper();
        }

    }
}
