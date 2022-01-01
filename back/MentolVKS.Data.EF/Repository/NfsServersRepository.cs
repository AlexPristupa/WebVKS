using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model.BaseModel;

namespace MentolVKS.Data.EF.Repository
{
    /// <summary>
    /// Реализация интерфейса репозитория <see cref="INfsServersRepository"/>
    /// </summary>
    public class NfsServersRepository : TableBasedEntityRepositoryBase<NfsServers>, INfsServersRepository
    {
        /// <inheritdoc />
        public NfsServersRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }
    }
}
