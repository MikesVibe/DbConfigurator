using AutoMapper;
using DbConfigurator.DataAccess.DTOs.AreaDtos;
using DbConfigurator.DataAccess.DTOs.BusinessUnitDtos;
using DbConfigurator.DataAccess.DTOs.CountryDtos;
using DbConfigurator.DataAccess.DTOs.DistributionInformationDtos;
using DbConfigurator.DataAccess.DTOs.PriorityDtos;
using DbConfigurator.DataAccess.DTOs.RecipientDtos;
using DbConfigurator.DataAccess.DTOs.RegionDtos;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;

namespace DbConfigurator.UI.Startup
{
    public class AutoMapperConfig
    {
        public IMapper Mapper { get; private set; }

        public AutoMapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Area, CreateAreaDto>();


                cfg.CreateMap<Region, RegionDto>().ReverseMap();
                cfg.CreateMap<Area, AreaDto>().ReverseMap();
                cfg.CreateMap<BusinessUnit, BusinessUnitDto>().ReverseMap();
                cfg.CreateMap<Country, CountryDto>().ReverseMap();
                cfg.CreateMap<Priority, PriorityDto>().ReverseMap();
                cfg.CreateMap<Recipient, RecipientDto>().ReverseMap();
                cfg.CreateMap<DistributionInformation, DistributionInformationDto>().ReverseMap();
                //.ForMember(d => d.RecipientsTo, opt => opt.MapFrom(
                //    rg => (rg.RecipientsTo != null) ? rg.RecipientsTo : Enumerable.Empty<Recipient>()))
                //.ForMember(d => d.RecipientsCc, opt => opt.MapFrom(
                //    rg => (rg.RecipientsCc != null) ? rg.RecipientsCc : Enumerable.Empty<Recipient>()));

                //cfg.CreateMap<Region, Region>()
                //    .ForMember(dest => dest.Area, opt => opt.Ignore())
                //    .ForMember(dest => dest.BusinessUnit, opt => opt.Ignore())
                //    .ForMember(dest => dest.Country, opt => opt.Ignore());

                //cfg.CreateMap<DistributionInformation, DistributionInformation>()
                //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //    .ForMember(dest => dest.Region, opt => opt.Ignore())
                //    .ForMember(dest => dest.Priority, opt => opt.Ignore());
                //    //.ForMember(dest => dest.RecipientsTo, opt => opt.MapFrom(src => src.RecipientsTo))
                //    //.ForMember(dest => dest.RecipientsCc, opt => opt.MapFrom(src => src.RecipientsCc));


                cfg.CreateMap<Area, AreaWrapper>();
                cfg.CreateMap<BusinessUnit, Model.DTOs.Wrapper.BusinessUnitWrapper>();
                cfg.CreateMap<Country, Model.DTOs.Wrapper.CountryWrapper>();
                cfg.CreateMap<Region, RegionWrapper>();
                cfg.CreateMap<Recipient, RecipientWrapper>();
                cfg.CreateMap<DistributionInformation, DistributionInformationWrapper>();

            });

            Mapper = config.CreateMapper();
        }

    }
}
