using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class LinkBookingToParticipantConfiguration : IEntityTypeConfiguration<LinkBookingToParticipant>
    {
        public void Configure(EntityTypeBuilder<LinkBookingToParticipant> builder)
        {
            builder.ToTable("LinkBookingToParticipant");
            builder.HasKey(p => p.Id);
        }
    }
}
