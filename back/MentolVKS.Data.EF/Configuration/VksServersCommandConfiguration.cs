using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksServersCommandConfiguration : IEntityTypeConfiguration<VksServersCommand>
    {
        public void Configure(EntityTypeBuilder<VksServersCommand> builder)
        {
            builder.ToTable("vksServersCommand");
            builder.HasKey(p => p.Idr);
        }
    }
}
