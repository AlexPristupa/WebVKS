using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    /// <summary>
    /// Реализация интерфейса репозитория <see cref="IServicesRepository"/>
    /// </summary>
    public class ServicesRepository : TableBasedEntityRepositoryBase<Services>, IServicesRepository
    {
        public ServicesRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        #region Implementation of IServicesRepository

        /// <inheritdoc />
        public async Task<Services> GetByNameAsync(string name)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Name == name);
        }

        #endregion
    }
}
