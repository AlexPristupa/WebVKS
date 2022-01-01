using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksServerConfiguration : IEntityTypeConfiguration<VksServer>
    {
        public void Configure(EntityTypeBuilder<VksServer> builder)
        {
            builder.ToTable("vksServers");
            builder.HasKey(p => p.Idr);
        }
    }
}
