using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Repository
{
    public class VksVendorRepository : TableBasedEntityRepositoryBase<VksVendor>, IVksVendorRepository
    {
        public VksVendorRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }
    }
}
