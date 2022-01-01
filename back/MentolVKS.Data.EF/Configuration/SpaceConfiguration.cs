using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class SpaceConfiguration : IEntityTypeConfiguration<Space>
    {
        public void Configure(EntityTypeBuilder<Space> builder)
        {
            builder.ToTable("Space");
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.LinkSpaceToParticipants).WithOne(p => p.Space).HasForeignKey(p => p.SpaceId);
        }
    }
}
