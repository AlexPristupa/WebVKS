using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Тип конференций для Booking
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<List<BookingType>> GetBookingTypeSelectDirectoryAsync(string search, int limit);
    }
}
