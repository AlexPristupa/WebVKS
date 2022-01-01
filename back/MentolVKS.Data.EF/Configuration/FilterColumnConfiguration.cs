using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class FilterColumnConfiguration : IEntityTypeConfiguration<FilterColumn>
    {
        public void Configure(EntityTypeBuilder<FilterColumn> builder)
        {            
            builder.HasKey(p => p.Id);
        }
    }
}
