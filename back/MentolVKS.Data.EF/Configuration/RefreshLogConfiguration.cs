using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class RefreshLogConfiguration : IEntityTypeConfiguration<RefreshLog>
    {
        public void Configure(EntityTypeBuilder<RefreshLog> builder)
        {
            builder.ToTable("RefreshLog");
            builder.HasKey(p => p.Idr);
        }
    }
}
