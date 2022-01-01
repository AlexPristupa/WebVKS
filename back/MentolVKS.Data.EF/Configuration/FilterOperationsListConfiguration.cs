using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class FilterOperationsListConfiguration : IEntityTypeConfiguration<FilterOperationsList>
    {
        public void Configure(EntityTypeBuilder<FilterOperationsList> builder)
        {
            builder.ToTable("FilterOperationsList");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
            builder.HasMany(p => p.FilterValue).WithOne(p => p.Operation).HasForeignKey(p => p.OperationId);
        }
    }
}
