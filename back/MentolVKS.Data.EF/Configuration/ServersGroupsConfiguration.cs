using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class ServersGroupsConfiguration : IEntityTypeConfiguration<ServersGroups>
    {
        public void Configure(EntityTypeBuilder<ServersGroups> builder)
        {
            builder.ToTable("ServersGroups");
            builder.HasKey(p => p.Id);
        }
    }
}
