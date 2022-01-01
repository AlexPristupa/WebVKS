using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class ColumnForStringFilterConfiguration : IEntityTypeConfiguration<ColumnForStringFilter>
    {
        public void Configure(EntityTypeBuilder<ColumnForStringFilter> builder)
        {            
            builder.HasKey(p => p.Id);
        }
    }
}
