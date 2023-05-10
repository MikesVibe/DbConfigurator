using DbConfigurator.Model.DTOs;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public interface IDataModel
    {

        Task SaveChangesAsync();

        ICollection<DistributionInformation> DistributionInformations{ get; }
        ICollection<Area> Areas { get; }
        ICollection<BuisnessUnit> BuisnessUnits { get; }
        ICollection<Country> Countries { get; }
        ICollection<Priority> Priorities { get; }
        ICollection<Recipient> Recipients { get; }
        Country DefaultCountry { get; }
        Priority DefaultPriority { get; }
        Area DefaultArea { get; }
        BuisnessUnit DefaultBuisnessUnit { get; }
        //ICollection<DistributionInformationWithOnlyIdsDto> DistributionInformationsDto { get; }
        ICollection<AreaDto> AreasDto { get; }
        ICollection<BuisnessUnitDto> BuisnessUnitsDto { get; }
        ICollection<CountryDto> CountriesDto { get; }
        ICollection<PriorityDto> PrioritiesDto { get; }
        ICollection<RecipientDto> RecipientsDto { get; }

        bool HasChanges();
        void ReloadEntryPriority(DistributionInformation disInfo);
        void ReloadEntryCountry(DistributionInformation disInfo);
        void Add<T>(T item) where T : class;
        Task AddAsync<T>(T item) where T : class;
        Recipient GetRecipient(int id);

        void Remove<T>(T item) where T : class;
        DistributionInformationDto GetDistributionInformationDto(int id);
        Task AddDistributionInformationAsync(DistributionInformation distributionInformation);
        Task<ICollection<DistributionInformation>> GetAllDistributionInformationAsync();
        Task RefreshDistributionInformationAsync();
        Task<DistributionInformation> GetDistributionInformationByIdAsync(int id);
    }
}