using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public interface IDataModel
    {

        Task SaveChangesAsync();

        IEnumerable<DistributionInformation> DistributionInformations{ get; }
        IEnumerable<Area> Areas { get; }
        IEnumerable<BuisnessUnit> BuisnessUnits { get; }
        IEnumerable<Country> Countries { get; }
        IEnumerable<Priority> Priorities { get; }
        IEnumerable<Recipient> Recipients { get; }
        Country DefaultCountry { get; }
        Priority DefaultPriority { get; }
        Area DefaultArea { get; }
        BuisnessUnit DefaultBuisnessUnit { get; }

        bool HasChanges();
        void ReloadEntryPriority(DistributionInformation disInfo);
        void ReloadEntryCountry(DistributionInformation disInfo);
        void Add<T>(T item) where T : class;
        void AddRecipientTo(int distributionInfoId, int recipientId);
        void Load<T>(T item, string propertName) where T : class;
        void AddRecipientCc(DistributionInformation disInfo, int recipientId);
        Task ReloadEntityAsync(DistributionInformation item);
        Task AddAsync<T>(T item) where T : class;
    }
}