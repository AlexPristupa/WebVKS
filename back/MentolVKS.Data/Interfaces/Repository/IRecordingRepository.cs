using MentolVKS.Model.BaseModel;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IRecordingRepository : IRepository<Recording>
    {
        Task<bool> CheckByBookingIdAsync(int bookingId);
    }
}
