using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCollection.Domain.Entities;

namespace MyCollection.Persistence.EntityConfigurations;
internal class CollectionItemConfiguration : IEntityTypeConfiguration<CollectionItem>
{
    public void Configure(EntityTypeBuilder<CollectionItem> builder)
    {
        builder
            .HasOne(c => c.Owner)
            .WithMany()
            .HasForeignKey(i => i.OwnerId);

        builder
            .HasOne<Collection>()
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CollectionId);
    }
}
