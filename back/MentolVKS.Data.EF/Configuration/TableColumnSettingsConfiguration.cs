using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class TableColumnSettingsConfiguration : IEntityTypeConfiguration<TableColumnSettings>
    {
        public void Configure(EntityTypeBuilder<TableColumnSettings> builder)
        {
            builder.ToTable("TableColumnSettings");
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.UserColumns).WithOne(p => p.Settings).HasForeignKey(p => p.TableColumnId);
        }
    }
}
