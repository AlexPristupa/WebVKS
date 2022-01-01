using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentolVKS.Data.EF.Configuration
{
    public class VksUsersOtherConfiguration : IEntityTypeConfiguration<VksUsersOther>
    {
        public void Configure(EntityTypeBuilder<VksUsersOther> builder)
        {
            builder.ToTable("vksUsersOther");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("idr");
        }
    }
}
