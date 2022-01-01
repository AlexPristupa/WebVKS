using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    public class OutlookBookingDefaultRepository : TableBasedEntityRepositoryBase<OutlookBookingDefault>, IOutlookBookingDefaultRepository
    {
        public OutlookBookingDefaultRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }


    }
}
