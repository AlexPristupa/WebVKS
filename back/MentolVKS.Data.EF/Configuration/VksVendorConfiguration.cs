using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksVendorConfiguration : IEntityTypeConfiguration<VksVendor>
    {
        public void Configure(EntityTypeBuilder<VksVendor> builder)
        {
            builder.ToTable("vksVendor");
            builder.HasKey(p => p.Idr);
        }
    }
}
