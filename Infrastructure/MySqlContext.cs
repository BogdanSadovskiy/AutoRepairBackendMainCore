using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Entity.CarsGeneralFolder;
using AutoRepairMainCore.Entity.ErrorCodesGeneralFolder;
using AutoRepairMainCore.Entity.ServiceFolder;
using Microsoft.EntityFrameworkCore;
namespace AutoRepairMainCore.Infrastructure

{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        public DbSet<Brand> brands { get; set; }

        public DbSet<Model> models { get; set; }

        public DbSet<Engine> engines { get; set; }

        public DbSet<Car> cars { get; set; }

        public DbSet<Block> blocks { get; set; }

        public DbSet<ErrorCode> errorCodes { get; set; }

        public DbSet<Role> roles { get; set; }

        public DbSet<AutoService> services { get; set; }

        public DbSet<ClientCar> clientCars { get; set; }

        public DbSet<Employee> employees { get; set; }

        public DbSet<ErrorCodeOrder> errorCodeOrders { get; set; }

        public DbSet<Order> orders { get; set; }
    }
}
