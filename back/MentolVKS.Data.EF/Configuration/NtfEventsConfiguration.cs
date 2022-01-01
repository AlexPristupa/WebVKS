using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class NtfEventsConfiguration : IEntityTypeConfiguration<NtfEvents>
    {
        public void Configure(EntityTypeBuilder<NtfEvents> builder)
        {
            builder.ToTable("NtfEvents");
            builder.HasKey(p => p.Idr);
        }
    }
}
