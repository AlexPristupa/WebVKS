using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksUserProfilesConfiguration : IEntityTypeConfiguration<VksUsersProfile>
    {
        public void Configure(EntityTypeBuilder<VksUsersProfile> builder)
        {
            builder.ToTable("vksUsersProfileConfiguration");
            builder.HasKey(p => p.Idr);
        }
    }
}
