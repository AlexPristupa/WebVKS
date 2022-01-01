using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksConferenceHistoryConfiguration : IEntityTypeConfiguration<VksConferenceHistory>
    {
        public void Configure(EntityTypeBuilder<VksConferenceHistory> builder)
        {
            builder.ToTable("vksConferenceHistory");
            builder.HasKey(p => p.Idr);
        }
    }
}
