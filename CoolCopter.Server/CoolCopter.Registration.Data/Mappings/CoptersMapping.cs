using CoolCopter.Registration.Core.Copter.CopterAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoolCopter.Registration.Data.Mappings
{
    public class CoptersMapping : IEntityTypeConfiguration<Copters>
    {
        public void Configure(EntityTypeBuilder<Copters> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.LastConnectedTimestamp).IsRequired();
            builder.HasIndex(x => x.LastConnectedTimestamp).IsUnique();
        }
    }
}