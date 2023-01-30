using DbConfigurator.Model;
using DbConfigurator.Model;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


namespace DbConfigurator.DataAccess
{
    public class DbConfiguratorDbContext : DbContext
    {
        public DbConfiguratorDbContext()
        {

        }
        public DbSet<DistributionInformation> DistributionInformations { get; set; }
        public DbSet<BuisnessUnit> BuisnessUnits { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<BuisnessUnit> Countries{ get; set; }
        public DbSet<Priority> Priorities{ get; set; }
        public DbSet<RecipientsGroup> RecipientsGroups{ get; set; }
        public DbSet<Recipient> Recipients{ get; set; }
        public DbSet<DestinationField> DestinationFields{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DbConfiguration"].ConnectionString);
            //optionsBuilder.UseSqlServer("server=\"MIKI-PC\\SQLEXPRESS01\";database=\"DbConfiguration\";trusted_connection=true;Integrated Security=True;Encrypt=False");
        }
    }
}
