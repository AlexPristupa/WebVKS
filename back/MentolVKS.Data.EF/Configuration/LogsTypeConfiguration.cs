using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class LogsTypeConfiguration : IEntityTypeConfiguration<LogsType>
    {
        public void Configure(EntityTypeBuilder<LogsType> builder)
        {
            builder.ToTable("LogsType");
            builder.HasKey(p => p.Idr);
        }
    }
}
