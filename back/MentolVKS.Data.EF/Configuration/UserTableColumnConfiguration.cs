using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class UserTableColumnConfiguration : IEntityTypeConfiguration<UserTableColumn>
    {
        public void Configure(EntityTypeBuilder<UserTableColumn> builder)
        {
            builder.ToTable("UserTableColumns");
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Settings).WithMany(p => p.UserColumns).HasForeignKey(p => p.TableColumnId);
        }
    }
}
