using Microsoft.EntityFrameworkCore;

namespace DynamicFilter.Lib.EFCore.Tests.Entities;

public class TestDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TestDb");
    }

    public DbSet<User> Users { get; set; }
}