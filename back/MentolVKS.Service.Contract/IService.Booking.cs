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
        /// Добавить бронирование
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Booking> AddBookingAsync(Booking item, bool outlook=false);
        /// <summary>
        /// Обновить бронирование
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Booking> UpdateBookingAsync(Booking item, bool outlook = false);
        /// <summary>
        /// Удалить бронирование
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteBookingAsync(int id);
        /// <summary>
        /// Получить бронирование по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Booking> GetBookingByIdAsync(int id);
        /// <summary>
        /// Проверка расписания на пересечение
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="shedule"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        Task CheckShedule(int spaceId, string shedule, DateTime? startTime,int duration,int timeZone, DateTime? dateEnd, int repeatCount, int? bookingId);

        public Task<List<BookingView>> BookingViewGetBySpaceIdAsync(int spaceId);

        Task<BookingOutlook> AddOutlookBookingAsync(int userId, 
            string name, 
            string location, 
            DateTime dateStart, 
            int timeZoneMinute, 
            int connectionTypeId, 
            int openConferenceBefore, 
            string emails, 
            string ics, 
            string uuid, 
            int duration, 
            string organizer,
            DateTime dateEnd,
            string timezonename
            );
        Task DeleteBookingByUuid(string uuid);
    }
}
