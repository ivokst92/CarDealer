namespace CarDealer.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using CarDealer.Data.Models;

    public class CarDealerDbContext : IdentityDbContext<User>
    {
        public CarDealerDbContext(DbContextOptions<CarDealerDbContext> options)
            : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PartCar> PartCar { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PartCar>()
                .HasKey(pc => new { pc.PartId, pc.CarId });

            builder.Entity<PartCar>()
                .HasOne(pc => pc.Car)
                .WithMany(c => c.Parts)
                .HasForeignKey(pc => pc.CarId);

            builder.Entity<PartCar>()
               .HasOne(pc => pc.Part)
               .WithMany(c => c.Cars)
               .HasForeignKey(pc => pc.PartId);

            builder
                 .Entity<Sale>()
                 .HasOne(s => s.Car)
                 .WithMany(c => c.Sales)
                 .HasForeignKey(s => s.CarId);

            builder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(s => s.Sales)
                .HasForeignKey(f => f.CustomerId);

            builder.Entity<Supplier>()
                .HasMany(s => s.Parts)
                .WithOne(s => s.Supplier)
                .HasForeignKey(f => f.SupplierId);

            base.OnModelCreating(builder);
        }
    }
}
