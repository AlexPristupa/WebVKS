using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class PinPoliticsConfiguration : IEntityTypeConfiguration<PinPolitics>
    {
        public void Configure(EntityTypeBuilder<PinPolitics> builder)
        {
            builder.ToTable("pinpolitics");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Idr");
        }
    }
}
