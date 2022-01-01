using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksUserConfiguration : IEntityTypeConfiguration<VksUser>
    {
        public void Configure(EntityTypeBuilder<VksUser> builder)
        {
            builder.ToTable("vksUsers");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
        }
    }
}
