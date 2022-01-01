using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class ServicesConfiguration : IEntityTypeConfiguration<Services>
    {
        public void Configure(EntityTypeBuilder<Services> builder)
        {
            builder.ToTable("Services");
            builder.HasKey(p => p.Id);
        }
    }
}
