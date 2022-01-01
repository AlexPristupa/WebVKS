using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class FilterNameConfiguration : IEntityTypeConfiguration<FilterName>
    {
        public void Configure(EntityTypeBuilder<FilterName> builder)
        {            
            builder.HasKey(p => p.Id);
        }
    }
}
