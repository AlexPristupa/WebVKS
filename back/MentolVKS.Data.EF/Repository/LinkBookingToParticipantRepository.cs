using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;

namespace MentolVKS.Data.EF.Repository
{
    /// <summary>
    /// Реализация интерфейса репозитория <see cref="ILinkBookingToParticipantRepository"/>.
    /// </summary>
    internal class LinkBookingToParticipantRepository : TableBasedEntityRepositoryBase<LinkBookingToParticipant>, ILinkBookingToParticipantRepository
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="LinkBookingToParticipantRepository"/>.
        /// </summary>
        /// <param name="context">Контекст подключения к БД.</param>
        /// <param name="mappings">Соответствие полей.</param>
        public LinkBookingToParticipantRepository(DataContext context, IColumnMappingConfiguration mappings)
            : base(context, mappings)
        {
        }

        /// <inheritdoc/>
        public async Task<List<LinkBookingToParticipant>> AllByBookingIdAsync(int bookingId)
        {
            return await DbSet.Where(c => c.BookingId == bookingId).Include(c => c.VksParticipant).ToListAsync();
        }
    }
}
