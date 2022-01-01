using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentolVKS.Data.EF.Configuration
{
    public class LinkBookingTovksUsersOtherConfiguration : IEntityTypeConfiguration<LinkBookingTovksUsersOther>
    {
        #region Implementation of IEntityTypeConfiguration<LinkBookingTovksUsersOther>

        public void Configure(EntityTypeBuilder<LinkBookingTovksUsersOther> builder)
        {
            builder.ToTable("LinkBookingTovksUsersOther");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("idr");
        }

        #endregion
    }
}
