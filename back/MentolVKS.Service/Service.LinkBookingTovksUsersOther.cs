using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service
    {
        /// <inheritdoc/>
        public async Task<bool> LinkBookingTovksUsersOtherCheckUriAsync(string uri, int userId)
        {
            var link = await UnitOfWork.LinkBookingTovksUsersOthersRepository.AllByUriAsync(uri);

            return link.Where(l => l.VksUsersOtherId != userId).Count()==0 ? false : true;
        }
    }
}
