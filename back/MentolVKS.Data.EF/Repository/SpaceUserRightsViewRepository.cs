using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model.BaseModel;

namespace MentolVKS.Data.EF.Repository
{
    /// <summary>
    /// Реализация интерфейса репозитория <see cref="ISpaceUserRightsViewRepository"/>
    /// </summary>
    public class SpaceUserRightsViewRepository : TableBasedEntityRepositoryBase<SpaceUserRightsView>, ISpaceUserRightsViewRepository
    {
        /// <inheritdoc />
        public SpaceUserRightsViewRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
            
        }
    }
}
