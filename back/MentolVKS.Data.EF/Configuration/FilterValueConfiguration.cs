using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class FilterValueConfiguration : IEntityTypeConfiguration<FilterValue>
    {
        public void Configure(EntityTypeBuilder<FilterValue> builder)
        {
            builder.ToTable("FilterValue");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
            builder.HasOne(p => p.Column).WithMany(p => p.FilterValue).HasForeignKey(p => p.ColumnId);
            builder.HasOne(p => p.Filter).WithMany(p => p.FilterValue).HasForeignKey(p => p.FilterId);
            builder.HasOne(p => p.Operation).WithMany(p => p.FilterValue).HasForeignKey(p => p.OperationId);
        }
    }
}
