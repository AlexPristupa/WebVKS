using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class AspNetUserRoleConfiguration : IEntityTypeConfiguration<AspNetUserRole>
    {
        #region Implementation of IEntityTypeConfiguration<AspNetUserRole>

        public void Configure(EntityTypeBuilder<AspNetUserRole> builder)
        {
            builder.ToTable("AspNetUserRoles");
            builder.HasKey(e => new { e.UserId, e.RoleId }).HasName("PK_AspNetUserRoles");
            builder.HasIndex(e => e.RoleId).HasName("IX_AspNetUserRoles_RoleId");
            builder.HasIndex(e => e.UserId).HasName("IX_AspNetUserRoles_UserId");
            builder.Property(p => p.RoleId).HasMaxLength(450);

            builder.HasOne(p => p.User).WithMany(p => p.AspNetUserRoles).HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.Role).WithMany(p => p.AspNetUserRoles).HasForeignKey(p => p.RoleId);
        }

        #endregion
    }
}
