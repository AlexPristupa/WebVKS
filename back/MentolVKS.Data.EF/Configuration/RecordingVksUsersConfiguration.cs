using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentolVKS.Data.EF.Configuration
{
    public class RecordingVksUsersConfiguration : IEntityTypeConfiguration<RecordingVksUsers>
    {
        public void Configure(EntityTypeBuilder<RecordingVksUsers> builder)
        {
            builder.ToTable("recordingvksusers");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
        }
    }
}
