using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksVendorModelConfiguration : IEntityTypeConfiguration<VksVendorModel>
    {
        public void Configure(EntityTypeBuilder<VksVendorModel> builder)
        {
            builder.ToTable("vksVendorModel");
            builder.HasKey(p => p.Idr);
        }
    }
}
