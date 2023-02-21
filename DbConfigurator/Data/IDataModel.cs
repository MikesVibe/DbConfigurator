using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;

namespace DbConfigurator.Model
{
    public interface IDataModel
    {

        void SaveChangesAsync();

        IEnumerable<DistributionInformation> DistributionInformations{ get; set; }
        IEnumerable<Area> Areas { get; set; }
        IEnumerable<BuisnessUnit> BuisnessUnits { get; set; }
        IEnumerable<Country> Countries { get; set; }
        IEnumerable<Priority> Priorities { get; set; }
        IEnumerable<Recipient> Recipients { get; set; }
        bool HasChanges();
        void ReloadEntryPriority(DistributionInformation disInfo);
        void ReloadEntryCountry(DistributionInformation disInfo);
        void Add<T>(T item) where T : class;
        void AddRecipientTo(int id, Recipient value);
        void Load<T>(T item, string propertName) where T : class;
    }
}