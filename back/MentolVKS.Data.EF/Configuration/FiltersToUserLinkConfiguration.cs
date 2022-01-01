using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class FiltersToUserLinkConfiguration : IEntityTypeConfiguration<FiltersToUserLink>
    {
        public void Configure(EntityTypeBuilder<FiltersToUserLink> builder)
        {
            builder.ToTable("FiltersToUserLink");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
            builder.HasOne(p => p.Filter).WithMany(p => p.FiltersToUserLink).HasForeignKey(p => p.FilterId);
        }
    }
}
