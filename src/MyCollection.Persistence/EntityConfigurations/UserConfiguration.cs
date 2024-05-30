using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCollection.Domain.Entities;

namespace MyCollection.Persistence.EntityConfigurations;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasGeneratedTsVectorColumn(u => u.SearchVector, "english", u => new { u.FirstName, u.LastName, u.Email, u.UserName })
            .HasIndex(u => u.SearchVector)
            .HasMethod("GIN");

        builder.HasIndex(u => u.UserName).IsUnique();

        builder.HasIndex(u => u.Email).IsUnique();

    }
}
