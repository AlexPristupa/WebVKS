using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class LinkSpaceToParticipantConfiguration : IEntityTypeConfiguration<LinkSpaceToParticipant>
    {
        public void Configure(EntityTypeBuilder<LinkSpaceToParticipant> builder)
        {
            builder.ToTable("LinkSpaceToParticipant");
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Space).WithMany(p => p.LinkSpaceToParticipants).HasForeignKey(p => p.SpaceId);
        }
    }
}
