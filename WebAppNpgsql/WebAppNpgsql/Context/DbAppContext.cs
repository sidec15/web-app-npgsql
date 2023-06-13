using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebAppNpgsql.Dal;

namespace WebAppNpgsql.Context
{
  public class DbAppContext : DbContext
  {
    public DbAppContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasPostgresExtension("postgis");
      base.OnModelCreating(modelBuilder);
    }

  }
}
