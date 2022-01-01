using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentolVKS.Data.EF.Configuration
{
    public class RecordingConfiguration : IEntityTypeConfiguration<Recording>
    {
        public void Configure(EntityTypeBuilder<Recording> builder)
        {
            builder.ToTable("Recording");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
        }
    }
}
