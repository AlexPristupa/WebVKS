using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class TimeZoneConfiguration : IEntityTypeConfiguration<Model.BaseModel.TimeZone>
    {
        public void Configure(EntityTypeBuilder<Model.BaseModel.TimeZone> builder)
        {
            builder.ToTable("TimeZone");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
            builder.Property(p => p.StandartId).HasColumnName("standartid");
        }
    }
}
