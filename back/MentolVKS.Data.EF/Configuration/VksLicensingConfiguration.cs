using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksLicensingConfiguration : IEntityTypeConfiguration<VksLicensing>
    {
        public void Configure(EntityTypeBuilder<VksLicensing> builder)
        {
            builder.ToTable("vksLicensing");
            builder.HasKey(p => p.Idr);
        }
    }
}
