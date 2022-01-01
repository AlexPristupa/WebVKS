using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentolVKS.Data.EF.Configuration
{
    public class SpaceUserRightsViewConfiguration : IEntityTypeConfiguration<SpaceUserRightsView>
    {
        public void Configure(EntityTypeBuilder<SpaceUserRightsView> builder)
        {
            builder.ToTable("SpaceUserRightsView");
            builder.HasKey(p => p.Id);
        }
    }
}
