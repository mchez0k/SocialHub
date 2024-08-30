using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialHub.Auth.Domain.Entities;

namespace SocialHub.Auth.Persistance;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(DbContextOptions<AppDbContext> options,
        IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
        Database.Migrate();    
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        Console.WriteLine(_configuration.GetConnectionString("DefaultConnection"));
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
    }
    
    public DbSet<User> Users { get; set; }
}