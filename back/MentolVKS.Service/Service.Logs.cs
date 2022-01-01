using MentolVKS.Data.Interfaces;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Enums;
using MentolVKS.Service.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        public async Task AddSuccessLog(ProductType product, LogTypes type, string action,string description, int? objectId = null, int? propertyId=null)
        {
            var log = new Logs();
            log.ProductId = (int)product;
            log.TypeId = (int)type;
            log.LevelId = (int)LogLevels.Success;
            log.UserName = UserName.GetUserName();
            log.DateRecord = System.DateTime.Now;
            log.Action = action;
            log.Description = description;
            log.Ip = UserName.GetUserIp();
            log.ObjectId = objectId;
            log.PropertyId= propertyId;

            await UnitOfWork.LogsRepository.AddAsync(log);
        }

        public async Task AddWarningLog(ProductType product, LogTypes type, string action, string description, int? objectId = null, int? propertyId = null)
        {
            var log = new Logs();
            log.ProductId = (int)product;
            log.TypeId = (int)type;
            log.LevelId = (int)LogLevels.Warn;
            log.UserName = UserName.GetUserName();
            log.DateRecord = System.DateTime.Now;
            log.Action = action;
            log.Description = description;
            log.Ip = UserName.GetUserIp();
            log.ObjectId = objectId;
            log.PropertyId = propertyId;

            await UnitOfWork.LogsRepository.AddAsync(log);
        }

        public async Task AddErrorLog(ProductType product, LogTypes type, string action, string description, int? objectId = null, int? propertyId = null)
        {
            var log = new Logs();
            log.ProductId = (int)product;
            log.TypeId = (int)type;
            log.LevelId = (int)LogLevels.Error;
            log.UserName = UserName.GetUserName();
            log.DateRecord = System.DateTime.Now;
            log.Action = action;
            log.Description = description;
            log.Ip = UserName.GetUserIp();
            log.ObjectId = objectId;
            log.PropertyId= propertyId;

            await UnitOfWork.LogsRepository.AddAsync(log);
        }
    }
}