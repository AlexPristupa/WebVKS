using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Configuration
{
    public class ConnectionTypeConfiguration : IEntityTypeConfiguration<ConnectionType>
    {
        public void Configure(EntityTypeBuilder<ConnectionType> builder)
        {
            builder.ToTable("ConnectionType");
            builder.HasKey(p => p.Id);
        }
    }
}
