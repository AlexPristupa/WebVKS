﻿using MentolVKS.Model;

using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksUserProfilesViewConfiguration : IEntityTypeConfiguration<VksUserProfilesView>
    {
        public void Configure(EntityTypeBuilder<VksUserProfilesView> builder)
        {
            builder.ToView("VksUserProfilesView");            
            builder.HasKey(p => p.Id);
            /*builder.Property(p => p.Id).HasColumnName("idr");
            builder.HasMany(p => p.FilterValue).WithOne(p => p.Column).HasForeignKey(p => p.ColumnId);
            builder.HasOne(p => p.FilterType).WithMany(p => p.FilterColumnsList).HasForeignKey(p => p.FilterTypeId);
            builder.HasOne(p => p.Table).WithMany(p => p.FilterColumnsList).HasForeignKey(p => p.TableId);*/
        }
    }
}
