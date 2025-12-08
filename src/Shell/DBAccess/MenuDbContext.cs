using Microsoft.EntityFrameworkCore;

using FW.PAS.BFFWeb.Entities;

namespace FW.PAS.BFFWeb.DBAccess;

public sealed class MenuDbContext
    : DbContext
{
    public MenuDbContext(DbContextOptions<MenuDbContext> options)
        : base(options)
    {
    }

    public DbSet<MenuDetail> MenuDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuDetail>().HasNoKey();

        base.OnModelCreating(modelBuilder);
    }
}