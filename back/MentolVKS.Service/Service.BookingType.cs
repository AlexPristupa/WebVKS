using MentolVKS.Data.Interfaces;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Service.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        /// <inheritdoc />
        public async Task<List<BookingType>> GetBookingTypeSelectDirectoryAsync(string search, int limit)
        {
            return await UnitOfWork.BookingTypeRepository.GetSelectDirectoryAsync(search, limit);
        }
    }
}