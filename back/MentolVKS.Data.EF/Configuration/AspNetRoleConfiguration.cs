using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class AspNetRoleConfiguration : IEntityTypeConfiguration<AspNetRole>
    {
        #region Implementation of IEntityTypeConfiguration<AspNetRole>

        public void Configure(EntityTypeBuilder<AspNetRole> builder)
        {
            builder.ToTable("AspNetRoles");
            builder.HasKey(p => p.Id);
            
            /*builder.HasOne(p => p.Parent).WithMany(p => p.Children).HasForeignKey(p => p.ParentId);

            builder.HasMany(p => p.Children).WithOne(p => p.Parent).HasForeignKey(p => p.ParentId);*/
            builder.HasMany(p => p.AspNetTreePages).WithOne(p => p.Role).HasForeignKey(p => p.RoleId);
            builder.HasMany(p => p.AspNetUserRoles).WithOne(p => p.Role).HasForeignKey(p => p.RoleId);
        }

        #endregion
    }
}
