using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class FiltersListConfiguration : IEntityTypeConfiguration<FiltersList>
    {
        public void Configure(EntityTypeBuilder<FiltersList> builder)
        {
            builder.ToTable("FiltersList");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
            builder.HasMany(p => p.FilterValue).WithOne(p => p.Filter).HasForeignKey(p => p.FilterId);
            builder.HasMany(p => p.FiltersToUserLink).WithOne(p => p.Filter).HasForeignKey(p => p.FilterId);
        }
    }
}
