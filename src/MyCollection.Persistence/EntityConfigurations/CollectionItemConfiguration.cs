using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCollection.Domain.Entities;

namespace MyCollection.Persistence.EntityConfigurations;
internal class CollectionItemConfiguration : IEntityTypeConfiguration<CollectionItem>
{
    public void Configure(EntityTypeBuilder<CollectionItem> builder)
    {
        builder
            .HasGeneratedTsVectorColumn(c => c.SearchVector, "english", c => new { c.Name, c.Description })
            .HasIndex(c => c.SearchVector)
            .HasMethod("GIN");

        builder
            .HasOne(c => c.Owner)
            .WithMany()
            .HasForeignKey(i => i.OwnerId);

        builder
            .HasOne(i=>i.Collection)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CollectionId);
    }
}
