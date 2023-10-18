
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    }
    
    public DbSet<SuperHero> SuperHeroes { get; set; }
}