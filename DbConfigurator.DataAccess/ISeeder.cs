using System.Threading.Tasks;

namespace DbConfigurator.DataAccess
{
    public interface ISeeder
    {
        Task<bool> AnyDistributionInformationAsync();
        Task<bool> AnyRecipientInDatabaseAsync();
        Task<bool> AnyRegionInDatabaseAsync();
        Task SeedDistributionInformation();
        Task SeedRecipients();
        Task SeedRegions(string regionsAsJson);
    }
}