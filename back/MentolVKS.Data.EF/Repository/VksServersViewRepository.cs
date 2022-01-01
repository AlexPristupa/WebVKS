using MentolVKS.Common.TypeExtensions;
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
    internal class VksServersViewRepository : ViewBasedEntityRepositoryBase<VksServersView>, IVksServersViewRepository
    {
        public VksServersViewRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

 
    }
}
