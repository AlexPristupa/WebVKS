using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksConferenceCurrentConfiguration : IEntityTypeConfiguration<VksConferenceCurrent>
    {
        public void Configure(EntityTypeBuilder<VksConferenceCurrent> builder)
        {
            builder.ToTable("vksConferenceCurrent");
            builder.HasKey(p => p.Idr);
        }
    }
}
