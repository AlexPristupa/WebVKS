using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class FilterForColumnTypeListConfiguration : IEntityTypeConfiguration<FilterForColumnTypeList>
    {
        public void Configure(EntityTypeBuilder<FilterForColumnTypeList> builder)
        {
            builder.ToTable("FilterForColumnTypeList");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
            builder.HasMany(p => p.FilterColumnsList).WithOne(p => p.FilterType).HasForeignKey(p => p.FilterTypeId);
        }
    }
}
