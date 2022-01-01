using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface ITimeZoneRepository : IRepository<Model.BaseModel.TimeZone>
    {
        Task<List<Model.BaseModel.TimeZone>> GetSelectDirectoryAsync(string search, int limit);
        Task<Model.BaseModel.TimeZone> GetTimeZoneByOffset(int minute);
        Task<Model.BaseModel.TimeZone> GetByStandartId(string id);
    }
}
