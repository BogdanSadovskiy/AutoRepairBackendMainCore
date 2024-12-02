using AutoRepairMainCore.Entity;
using AutoRepairMainCore.Entity.CarsGeneralFolder;
using AutoRepairMainCore.Entity.ErrorCodesGeneralFolder;
using AutoRepairMainCore.Entity.ServiceFolder;
using Microsoft.EntityFrameworkCore;
namespace AutoRepairMainCore.Infrastructure

{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
        }
        public DbSet<Brand> brands { get; set; }
        public DbSet<Model> models { get; set; }
        public DbSet<Engine> engines { get; set; }
        public DbSet<Car> cars { get; set; }

        public DbSet<Block> blocks { get; set; }
        public DbSet<ErrorCode> errorCodes { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<MyService> services { get; set; }

        public DbSet<ClientCar> clientCars { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<ErrorCodeOrder> errorCodeOrders { get; set; }
        public DbSet<Order> orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<MyService>(entity =>
            {

                entity.HasKey(e => e.id);

                entity.HasOne<Role>()  
                         .WithMany()  
                         .HasForeignKey(e => e.role_id)  
                         .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.role_id)
                .IsRequired();

                entity.Property(e => e.service_name)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.service_password)
                      .IsRequired()
                      .HasMaxLength(255);

               

            });

        }
    }
}
