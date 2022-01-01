using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class AspNetUserConfiguration : IEntityTypeConfiguration<AspNetUser>
    {
        #region Implementation of IEntityTypeConfiguration<AspNetUser>

        public void Configure(EntityTypeBuilder<AspNetUser> builder)
        {
            builder.ToTable("AspNetUsers");
            builder.HasKey(p => p.Id);
            builder.HasIndex(e => e.NormalizedEmail).HasName("EmailIndex");
            builder.HasIndex(e => e.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.Property(p => p.Email).HasMaxLength(256);
            builder.Property(p => p.NormalizedEmail).HasMaxLength(256);
            builder.Property(p => p.NormalizedUserName).HasMaxLength(256).IsRequired();
            builder.Property(p => p.UserName).HasMaxLength(256);

            builder.HasMany(p => p.AspNetUserRoles).WithOne(p => p.User).HasForeignKey(p => p.UserId);
        }

        #endregion
    }
}
