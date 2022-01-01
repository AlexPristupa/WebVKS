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
    /// Реализация интерфейса репозитория <see cref="ILinkBookingTovksUsersOthersRepository"/>.
    /// </summary>
    internal class LinkBookingTovksUsersOthersRepository : TableBasedEntityRepositoryBase<LinkBookingTovksUsersOther>, ILinkBookingTovksUsersOthersRepository
    {
        /// <inheritdoc />
        public LinkBookingTovksUsersOthersRepository(DataContext context, IColumnMappingConfiguration mappings)
            : base(context, mappings)
        {
        }

        #region Implementation of ILinkBookingTovksUsersOthersRepository

        /// <inheritdoc />
        public async Task<List<LinkBookingTovksUsersOther>> AllByBookingAsync(int bookingId)
        {
            return await DbSet.Where(l => l.BookingId == bookingId).Include(l => l.VksUsersOther).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<LinkBookingTovksUsersOther>> AllByUriAsync(string uri)
        {
            return await DbSet.Where(l => l.VksUsersOther.Uri == uri).ToListAsync();
        }

        #endregion
    }
}
