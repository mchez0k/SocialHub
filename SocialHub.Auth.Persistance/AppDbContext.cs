using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialHub.Auth.Domain.Entities;

namespace SocialHub.Auth.Persistance;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();    
    }
}