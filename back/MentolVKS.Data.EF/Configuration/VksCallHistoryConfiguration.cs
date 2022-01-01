using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksCallHistoryConfiguration : IEntityTypeConfiguration<VksCallHistory>
    {
        public void Configure(EntityTypeBuilder<VksCallHistory> builder)
        {
            builder.ToTable("vksCallHistory");
            builder.HasKey(p => p.Idr);
        }
    }
}
