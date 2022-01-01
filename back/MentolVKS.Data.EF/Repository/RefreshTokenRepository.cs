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
    public class RefreshTokenRepository : TableBasedEntityRepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        public async Task<RefreshToken> SearchRefreshTokenAsync(string token, string fingerPrint, string ipAddress)
        {
            return await DbSet
                .FirstOrDefaultAsync(c => c.Token == token && c.FingerPrint == fingerPrint && c.Ip == ipAddress && c.EndDate > DateTime.UtcNow);
                
        }
    }
}
