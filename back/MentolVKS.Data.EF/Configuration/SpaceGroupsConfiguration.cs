using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class SpaceGroupsConfiguration : IEntityTypeConfiguration<SpaceGroups>
    {
        public void Configure(EntityTypeBuilder<SpaceGroups> builder)
        {
            builder.ToTable("SpaceGroups");
            builder.HasKey(p => p.Id);
        }
    }
}
