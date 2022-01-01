using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksCallInstanceConfiguration : IEntityTypeConfiguration<VksCallInstance>
    {
        public void Configure(EntityTypeBuilder<VksCallInstance> builder)
        {
            builder.ToTable("vksCallInstance");
            builder.HasKey(p => p.Idr);
        }
    }
}
