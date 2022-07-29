using Microsoft.EntityFrameworkCore;

namespace parser;

class AppDbContext : DbContext
{
    public DbSet<CitySearchInfo> Cities { get; set; }

    public AppDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cities;Username=postgres;Password=admin");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<CitySearchInfo>().HasKey(x => x.Query);
        //modelBuilder.Entity<SearchResultItem>().HasNoKey();
    }
}