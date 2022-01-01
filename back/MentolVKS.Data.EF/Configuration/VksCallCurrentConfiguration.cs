using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksCallCurrentConfiguration : IEntityTypeConfiguration<VksCallCurrent>
    {
        public void Configure(EntityTypeBuilder<VksCallCurrent> builder)
        {
            builder.ToTable("vksCallCurrent");
            builder.HasKey(p => p.Idr);
        }
    }
}
