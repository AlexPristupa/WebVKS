using System.Collections.Generic;
using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
    public class FilterOperationsList : TableBasedEntityBase
    {
        public int Id { get; set; }
        public string OperationName { get; set; }
        public string Operand { get; set; }
        public int ColumnTypeFilt { get; set; }

        public ICollection<FilterValue> FilterValue { get; set; } = new HashSet<FilterValue>();
    }
}