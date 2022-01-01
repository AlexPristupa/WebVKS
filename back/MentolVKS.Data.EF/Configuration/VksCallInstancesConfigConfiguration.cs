using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksCallInstancesConfigConfiguration : IEntityTypeConfiguration<VksCallInstancesConfig>
    {
        public void Configure(EntityTypeBuilder<VksCallInstancesConfig> builder)
        {
            builder.ToTable("vksCallInstancesConfig");
            builder.HasKey(p => p.Idr);
        }
    }
}
