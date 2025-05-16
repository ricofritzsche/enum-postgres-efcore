using Microsoft.EntityFrameworkCore;

namespace EnumPostgresDotnet;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=localhost;Port=5432;Database=orders;Username=postgres;Password=password";
        optionsBuilder.UseNpgsql(
            connectionString,
            o => o.MapEnum<OrderStatus>("order_status")
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<OrderStatus>();

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(o => o.Status)
                .HasColumnType("order_status");
        });
    }
}