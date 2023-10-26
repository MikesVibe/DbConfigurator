using AutoMapper;
using DbConfigurator.DataAccess.DTOs.AreaDtos;
using DbConfigurator.DataAccess.DTOs.BusinessUnitDtos;
using DbConfigurator.DataAccess.DTOs.CountryDtos;
using DbConfigurator.DataAccess.DTOs.DistributionInformationDto;
using DbConfigurator.DataAccess.DTOs.DistributionInformationDtos;
using DbConfigurator.DataAccess.DTOs.PriorityDtos;
using DbConfigurator.DataAccess.DTOs.RecipientDtos;
using DbConfigurator.DataAccess.DTOs.RegionDtos;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Wrapper;
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
                cfg.CreateMap<Area, CreateAreaDto>();
                cfg.CreateMap<BusinessUnit, CreateBuisnessUnitDto>();
                cfg.CreateMap<Country, CreateCountryDto>();
                cfg.CreateMap<DistributionInformation, CreateDistributionInformationDto>()
                    .ForMember(d => d.RegionId, opt => opt.MapFrom(di => di.Region.Id))
                    .ForMember(d => d.PriorityId, opt => opt.MapFrom(di => di.Priority.Id))
                    .ForMember(d => d.RecipientsTo, opt => opt.MapFrom(di => di.RecipientsTo.Select(r => new RecipientIdDto { Id = r.Id })))
                    .ForMember(d => d.RecipientsCc, opt => opt.MapFrom(di => di.RecipientsCc.Select(r => new RecipientIdDto { Id = r.Id })));
                cfg.CreateMap<Recipient, CreateRecipientDto>();
                cfg.CreateMap<Region, CreateRegionDto>()
                    .ForMember(r => r.AreaId, opt => opt.MapFrom(rg => rg.Area.Id))
                    .ForMember(r => r.CountryId, opt => opt.MapFrom(rg => rg.Country.Id))
                    .ForMember(r => r.BusinessUnitId, opt => opt.MapFrom(rg => rg.BusinessUnit.Id));

                cfg.CreateMap<Area, UpdateAreaDto>();
                cfg.CreateMap<BusinessUnit, UpdateBuisnessUnitDto>();
                cfg.CreateMap<Country, UpdateCountryDto>();
                cfg.CreateMap<DistributionInformation, UpdateDistributionInformationDto>()
                    .ForMember(d => d.RegionId, opt => opt.MapFrom(di => di.Region.Id))
                    .ForMember(d => d.PriorityId, opt => opt.MapFrom(di => di.Priority.Id))
                    .ForMember(d => d.RecipientsTo, opt => opt.MapFrom(di => di.RecipientsTo.Select(r => new RecipientIdDto { Id = r.Id })))
                    .ForMember(d => d.RecipientsCc, opt => opt.MapFrom(di => di.RecipientsCc.Select(r => new RecipientIdDto { Id = r.Id })));
                cfg.CreateMap<Recipient, UpdateRecipientDto>();
                cfg.CreateMap<Region, UpdateRegionDto>()
                    .ForMember(r => r.AreaId, opt => opt.MapFrom(rg => rg.Area.Id))
                    .ForMember(r => r.CountryId, opt => opt.MapFrom(rg => rg.Country.Id))
                    .ForMember(r => r.BusinessUnitId, opt => opt.MapFrom(rg => rg.BusinessUnit.Id));


                cfg.CreateMap<Region, RegionDto>().ReverseMap();
                cfg.CreateMap<Area, AreaDto>().ReverseMap();
                cfg.CreateMap<BusinessUnit, BusinessUnitDto>().ReverseMap();
                cfg.CreateMap<Country, CountryDto>().ReverseMap();
                cfg.CreateMap<Priority, PriorityDto>().ReverseMap();
                cfg.CreateMap<Recipient, RecipientDto>().ReverseMap();
                cfg.CreateMap<DistributionInformation, DistributionInformationDto>().ReverseMap();

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
