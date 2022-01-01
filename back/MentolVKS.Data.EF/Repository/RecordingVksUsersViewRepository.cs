using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model.BaseModel;

namespace MentolVKS.Data.EF.Repository
{
    internal class RecordingVksUsersViewRepository : ViewBasedEntityRepositoryBase<RecordingVksUsersView>, IRecordingVksUsersViewRepository
    {
        public RecordingVksUsersViewRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }
    }
}
