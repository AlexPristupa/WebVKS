﻿using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    public interface IPinPoliticsRepository : IRepository<PinPolitics>
    {
        Task<List<PinPolitics>> GetSelectDirectoryAsync(string search, int limit);
    }
}
