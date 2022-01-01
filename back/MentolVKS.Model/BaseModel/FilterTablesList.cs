using System.Collections.Generic;
using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
    public class FilterTablesList : TableBasedEntityBase
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string Dbtable { get; set; }
        public string AccessCondition { get; set; }

        public ICollection<FilterColumnsList> FilterColumnsList { get; set; } = new HashSet<FilterColumnsList>();
    }
}