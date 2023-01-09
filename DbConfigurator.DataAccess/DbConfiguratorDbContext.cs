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
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DbConfiguration"].ConnectionString);
            optionsBuilder.UseSqlServer("server=\"MIKI-PC\\SQLEXPRESS01\";database=\"DbConfiguration\";trusted_connection=true;Integrated Security=True;Encrypt=False");
        }
    }
}
