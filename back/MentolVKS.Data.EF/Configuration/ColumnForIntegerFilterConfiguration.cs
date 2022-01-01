using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class ColumnForIntegerFilterConfiguration : IEntityTypeConfiguration<ColumnForIntegerFilter>
    {
        public void Configure(EntityTypeBuilder<ColumnForIntegerFilter> builder)
        {            
            builder.HasKey(p => p.Id);
        }
    }
}
