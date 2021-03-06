using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<List<Booking>> GetBySpaceIdAsync(int spaceId);
        Task<Booking> GetByUidAsync(string uid);
    }
}
