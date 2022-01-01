using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    /// <summary>
    /// Реализация интерфейса репозитория <see cref="IRecordingVksUsersRepository"/>
    /// </summary>
    public class RecordingVksUsersRepository : TableBasedEntityRepositoryBase<RecordingVksUsers>, IRecordingVksUsersRepository
    {
        /// <inheritdoc />
        public RecordingVksUsersRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        /// <inheritdoc/>
        public async Task<RecordingVksUsers> GetByRecordingIdAndUserIdAsync(int recordingId, int userId)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.RecordingId == recordingId && p.UserId == userId);
        }

        /// <inheritdoc/>
        public async Task<bool> ChekcByRecrodingIdAsync(int recordingId)
        {
            return await DbSet.AnyAsync(c => c.RecordingId == recordingId);
        }
        #region Overrides of TableBasedEntityRepositoryBase<RecordingVksUsers>

        /// <inheritdoc/>
        public override async Task<RecordingVksUsers> GetByIdAsync(params object[] keyValues)
        {
            return await DbSet.Include(p => p.Recording).Include(p => p.User).Include(p => p.Recording.Booking).FirstOrDefaultAsync(p => p.Id == (int)keyValues[0]);
        }

        #endregion
    }
}
