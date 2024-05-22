using Microsoft.EntityFrameworkCore;
using MyCollection.Domain.Entities;

namespace MyCollection.Persistence.DataContext;
public class CollectionDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Collection> Collections => Set<Collection>();
    public DbSet<CollectionItem> Items => Set<CollectionItem>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Like> Likes => Set<Like>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CollectionDbContext).Assembly);
    }
}
