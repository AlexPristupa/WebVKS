using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class FilterTablesListConfiguration : IEntityTypeConfiguration<FilterTablesList>
    {
        public void Configure(EntityTypeBuilder<FilterTablesList> builder)
        {
            builder.ToTable("FilterTablesList");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
            builder.HasMany(p => p.FilterColumnsList).WithOne(p => p.Table).HasForeignKey(p => p.TableId);
        }
    }
}
