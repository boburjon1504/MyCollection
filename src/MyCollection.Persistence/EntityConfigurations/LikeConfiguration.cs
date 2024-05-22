using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCollection.Domain.Entities;

namespace MyCollection.Persistence.EntityConfigurations;
internal class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(l => l.UserId);

        builder
            .HasOne<CollectionItem>()
            .WithMany(i=>i.Likes)
            .HasForeignKey(l => l.ItemId);
    }
}
