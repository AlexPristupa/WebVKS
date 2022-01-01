using MentolVKS.Model;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface ILinkSpaceToParticipantRepository : IRepository<LinkSpaceToParticipant>
    {
        Task<IEnumerable<LinkSpaceToParticipant>> GetBySpaceIdAsync(int spaceId);
    }
}
