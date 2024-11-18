using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutoRepairMainCore.Infrastructure
{
    public class MySqlContextFactory : IDesignTimeDbContextFactory<MySqlContext>
    {
        public MySqlContext CreateDbContext(string[] args)
        {
           
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MySqlConnection");

            var optionsBuilder = new DbContextOptionsBuilder<MySqlContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new MySqlContext(optionsBuilder.Options);
        }
    }
}
