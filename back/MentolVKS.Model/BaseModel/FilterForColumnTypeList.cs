using MentolVKS.Model.Bases;
using System.Collections.Generic;

namespace MentolVKS.Model.BaseModel
{
    public class FilterForColumnTypeList : TableBasedEntityBase
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string TypeName { get; set; }
        public string DataQuery { get; set; }

        public ICollection<FilterColumnsList> FilterColumnsList { get; set; } = new HashSet<FilterColumnsList>();
    }
}