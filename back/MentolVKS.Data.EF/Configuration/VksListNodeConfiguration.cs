using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksListNodeConfiguration : IEntityTypeConfiguration<VksListNode>
    {
        public void Configure(EntityTypeBuilder<VksListNode> builder)
        {
            builder.ToTable("vksListNode");
            builder.HasKey(p => p.Idr);
        }
    }
}
