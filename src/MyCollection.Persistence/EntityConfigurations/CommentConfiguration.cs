using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCollection.Domain.Entities;
using System.Reflection.Emit;

namespace MyCollection.Persistence.EntityConfigurations;
internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
        .HasGeneratedTsVectorColumn(c => c.SearchVector, "english",c=>new { c.Message })
        .HasIndex(p => p.SearchVector)
        .HasMethod("GIN");

        builder
            .HasOne<Comment>()
            .WithMany()
            .HasForeignKey(c => c.ParentId);

        builder
            .HasOne(c=>c.Sender)
            .WithMany()
            .HasForeignKey(c => c.SenderId);

        builder
            .HasOne<CollectionItem>()
            .WithMany(i => i.Comments)
            .HasForeignKey(c => c.ItemId);
    }
}
