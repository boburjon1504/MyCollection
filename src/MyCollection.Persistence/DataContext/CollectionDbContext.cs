using Microsoft.EntityFrameworkCore;
using MyCollection.Domain.Entities;

namespace MyCollection.Persistence.DataContext;
public class CollectionDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CollectionDbContext).Assembly);
    }
}
