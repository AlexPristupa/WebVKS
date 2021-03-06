using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentolVKS.Data.EF.Configuration
{
    public class NfsServersConfiguration : IEntityTypeConfiguration<NfsServers>
    {
        public void Configure(EntityTypeBuilder<NfsServers> builder)
        {
            builder.ToTable("NfsServers");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
        }
    }
}
