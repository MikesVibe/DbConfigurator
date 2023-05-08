using System.Threading.Tasks;

namespace DbConfigurator.DataAccess
{
    public interface ISeeder
    {
        Task Seed();
    }
}