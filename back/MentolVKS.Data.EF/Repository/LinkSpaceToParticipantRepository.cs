using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    public class LinkSpaceToParticipantRepository : TableBasedEntityRepositoryBase<LinkSpaceToParticipant>, ILinkSpaceToParticipantRepository
    {
        public LinkSpaceToParticipantRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
            
        }

        public async Task<IEnumerable<LinkSpaceToParticipant>> GetBySpaceIdAsync(int spaceId)
        {
            return await DbSet.Where(c => c.SpaceId == spaceId).ToListAsync();
        }
    }
}
