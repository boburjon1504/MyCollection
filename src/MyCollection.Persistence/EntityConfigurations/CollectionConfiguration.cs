﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCollection.Domain.Entities;

namespace MyCollection.Persistence.EntityConfigurations;
internal class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder
            .HasIndex(c => new { c.OwnerId, c.Name })
            .IsUnique();

        builder
            .HasOne(c => c.Owner)
            .WithMany(u => u.Collections)
            .HasForeignKey(c => c.OwnerId);
    }
}
