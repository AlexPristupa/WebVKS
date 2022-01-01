using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class AspNetTreePageConfiguration : IEntityTypeConfiguration<AspNetTreePage>
    {
        #region Implementation of IEntityTypeConfiguration<AspNetTreePage>

        public void Configure(EntityTypeBuilder<AspNetTreePage> builder)
        {
            builder.ToTable("AspNetTreePages");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
            builder.HasOne(p => p.Role).WithMany(p => p.AspNetTreePages).HasForeignKey(p => p.RoleId);
        }

        #endregion
    }
}
