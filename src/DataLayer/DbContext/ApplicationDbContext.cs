using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.DataLayer.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Distributor> Distributors { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>()
                .HasOne<ApplicationUser>(x => x.User)
                .WithOne()
                .IsRequired();

            builder.Entity<Distributor>()
                .HasOne<ApplicationUser>(x => x.User)
                .WithOne()
                .IsRequired();
        }
    }
}