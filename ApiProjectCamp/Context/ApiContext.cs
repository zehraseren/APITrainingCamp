using Microsoft.EntityFrameworkCore;
using ApiProjectCamp.WebApi.Entities;

namespace ApiProjectCamp.WebApi.Context;

public class ApiContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=Zehra;initial catalog=ApiYummyDb;integrated security=true;TrustServerCertificate=True");
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Chef> Chefs { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Testimonial> Testimonials { get; set; }
    public DbSet<YummyEvent> YummyEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<YummyEvent>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        base.OnModelCreating(modelBuilder);
    }
}
