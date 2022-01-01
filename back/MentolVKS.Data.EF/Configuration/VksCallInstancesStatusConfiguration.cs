using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksCallInstancesStatusConfiguration : IEntityTypeConfiguration<VksCallInstancesStatus>
    {
        public void Configure(EntityTypeBuilder<VksCallInstancesStatus> builder)
        {
            builder.ToTable("vksCallInstancesStatus");
            builder.HasKey(p => p.Idr);
        }
    }
}
