using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class LicenseXmlConfiguration : IEntityTypeConfiguration<LicenseXml>
    {
        public void Configure(EntityTypeBuilder<LicenseXml> builder)
        {
            builder.ToTable("licensexml");
            builder.HasKey(p => p.Products);

            builder.Ignore(p => p.ProductList);
        }
    }
}
