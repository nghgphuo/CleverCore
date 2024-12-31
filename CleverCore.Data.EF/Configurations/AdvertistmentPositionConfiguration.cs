using CleverCore.Data.EF.Extensions;
using CleverCore.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace CleverCore.Data.EF.Configurations
{
    public class AdvertisementPositionConfiguration : DbEntityConfiguration<AdvertisementPosition>
    {
        public override void Configure(EntityTypeBuilder<AdvertisementPosition> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(20).IsRequired();
            // etc.
        }
    }
}
