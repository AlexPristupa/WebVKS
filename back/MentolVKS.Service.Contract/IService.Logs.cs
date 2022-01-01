using MentolVKS.Model;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        Task AddSuccessLog(ProductType product, LogTypes type, string action, string description, int? objectId = null, int? propertyId = null);
        Task AddWarningLog(ProductType product, LogTypes type, string action, string description, int? objectId = null, int? propertyId = null);
        Task AddErrorLog(ProductType product, LogTypes type, string action, string description, int? objectId = null, int? propertyId = null);
    }
}
