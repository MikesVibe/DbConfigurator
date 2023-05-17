﻿using DbConfigurator.Model.DTOs;
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

        ICollection<Area> Areas { get; }
        ICollection<BuisnessUnit> BuisnessUnits { get; }
        ICollection<Country> Countries { get; }
        ICollection<Priority> Priorities { get; }
        ICollection<Recipient> Recipients { get; }
        Country DefaultCountry { get; }
        Priority DefaultPriority { get; }
        Area DefaultArea { get; }
        BuisnessUnit DefaultBuisnessUnit { get; }
        ICollection<AreaDto> AreasDto { get; }
        ICollection<BuisnessUnitDto> BuisnessUnitsDto { get; }
        ICollection<CountryDto> CountriesDto { get; }
        ICollection<PriorityDto> PrioritiesDto { get; }
        ICollection<RecipientDto> RecipientsDto { get; }

        bool HasChanges();
        Task ReloadEntryPriorityAsync(DistributionInformation disInfo);
        Task ReloadEntryCountryAsync(DistributionInformation disInfo);
        Task AddAsync<T>(T item) where T : class;
        //Task AddDistributionInformationAsync(DistributionInformation item);
        Task<Recipient> GetRecipientAsync(int id);

        void Remove<T>(T item) where T : class;
        Task<ICollection<DistributionInformation>> GetAllDistributionInformationAsync();
        Task<DistributionInformation> GetDistributionInformationByIdAsync(int id);
        bool IsDefaultCountry(int countryId);
    }
}