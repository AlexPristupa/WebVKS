using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentolVKS.Data.EF.Configuration
{
    public class RecordingVksUsersViewConfiguration : IEntityTypeConfiguration<RecordingVksUsersView>
    {
        public void Configure(EntityTypeBuilder<RecordingVksUsersView> builder)
        {
            builder.ToView("RecordingVksUsersView");            
            builder.HasKey(p => p.Id);
        }
    }
}
