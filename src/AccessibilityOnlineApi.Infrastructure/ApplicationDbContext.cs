using AccessibilityOnlineApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace AccessibilityOnlineApi.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<User>? Users { get; set; }
    public DbSet<Key>? Keys { get; set; }
}