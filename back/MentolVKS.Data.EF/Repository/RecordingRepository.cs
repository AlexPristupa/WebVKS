using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    public class RecordingRepository : TableBasedEntityRepositoryBase<Recording>, IRecordingRepository
    {
        public RecordingRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        #region Overrides of TableBasedEntityRepositoryBase<Recording>

        /// <inheritdoc/>
        public override async Task<Recording> GetByIdAsync(params object[] keyValues)
        {
            return await DbSet.Include(p => p.Booking).FirstOrDefaultAsync(p => p.Id == (int)keyValues[0]);
        }

        public async Task<bool> CheckByBookingIdAsync(int bookingId)
        {
            return await DbSet.AnyAsync(c => c.BookingId == bookingId);
        }
        #endregion
    }
}
