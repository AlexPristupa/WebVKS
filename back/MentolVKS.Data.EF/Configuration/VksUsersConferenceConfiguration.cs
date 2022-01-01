using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksUsersConferenceConfiguration : IEntityTypeConfiguration<VksUsersConference>
    {
        public void Configure(EntityTypeBuilder<VksUsersConference> builder)
        {
            builder.ToTable("vksUsersConference");
            builder.HasKey(p => p.Idr);
        }
    }
}
