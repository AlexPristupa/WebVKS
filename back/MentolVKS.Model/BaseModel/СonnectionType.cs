using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Тип подключения участинков
    /// </summary>
    public class ConnectionType : TableBasedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrivateName { get; set; }
    }
}
