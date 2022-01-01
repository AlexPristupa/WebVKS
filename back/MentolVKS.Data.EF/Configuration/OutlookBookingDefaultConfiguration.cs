using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class OutlookBookingDefaultConfiguration : IEntityTypeConfiguration<OutlookBookingDefault>
    {
        public void Configure(EntityTypeBuilder<OutlookBookingDefault> builder)
        {
            builder.ToTable("OutlookBookingDefault");
            builder.HasKey(p => p.Id);
        }
    }
}
