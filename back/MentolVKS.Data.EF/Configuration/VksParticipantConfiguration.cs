using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksParticipantConfiguration : IEntityTypeConfiguration<VksParticipant>
    {
        public void Configure(EntityTypeBuilder<VksParticipant> builder)
        {
            builder.ToTable("vksParticipant");
            builder.HasKey(p => p.Idr);
        }
    }
}
