using AutoMapper;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Table;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.Model.Entities.Wrapper.Table;
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
                cfg.CreateMap<Region, RegionDto>().ReverseMap();
                cfg.CreateMap<Area, AreaDto>().ReverseMap();
                cfg.CreateMap<BuisnessUnit, BuisnessUnitDto>().ReverseMap();
                cfg.CreateMap<Country, CountryDto>().ReverseMap();
                cfg.CreateMap<Priority, PriorityDto>();
                cfg.CreateMap<Recipient, RecipientDto>().ReverseMap();
                cfg.CreateMap<DistributionInformation, DistributionInformationDto>()
                            .ForMember(d => d.RecipientsTo, opt => opt.MapFrom(
                                rg => (rg.RecipientsTo != null) ? rg.RecipientsTo : Enumerable.Empty<Recipient>()))
                            .ForMember(d => d.RecipientsCc, opt => opt.MapFrom(
                                rg => (rg.RecipientsCc != null) ? rg.RecipientsCc : Enumerable.Empty<Recipient>()));

                cfg.CreateMap<Area, AreaTableItem>();
                cfg.CreateMap<BuisnessUnit, BuisnessUnitTableItem>();
                cfg.CreateMap<Country, CountryTableItem>();

                cfg.CreateMap<DistributionInformation, DistributionInformationTableItem>()
                    .ForMember(d => d.RecipientsTo, opt => opt.MapFrom(
                        rg => (rg.RecipientsTo != null) ? rg.RecipientsTo : Enumerable.Empty<Recipient>()))
                    .ForMember(d => d.RecipientsCc, opt => opt.MapFrom(
                        rg => (rg.RecipientsCc != null) ? rg.RecipientsCc : Enumerable.Empty<Recipient>()));

                cfg.CreateMap<AreaTableItem, AreaDto>();
                cfg.CreateMap<BuisnessUnitTableItem, BuisnessUnitDto>();


                cfg.CreateMap<Area, AreaTableItemWrapper>();
                cfg.CreateMap<BuisnessUnit, BuisnessUnitTableItemWrapper>();

            });

            Mapper = config.CreateMapper();
        }

    }
}
