using MentolVKS.Model.Bases;
using System.Collections.Generic;

namespace MentolVKS.Model.BaseModel
{
    public class FilterColumnsList : TableBasedEntityBase
    {
        public int Id { get; set; }
        public int? TableId { get; set; }
        public string ColumnName { get; set; }
        public int? FilterTypeId { get; set; }
        public string DataQuery { get; set; }
        public string ConditionColumn { get; set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }
        public string Title { get; set; }
        public string FilterSql { get; set; }
        public string WhereColumn { get; set; }
        public bool IsTableColumn { get; set; }

        public FilterForColumnTypeList FilterType { get; set; }
        public FilterTablesList Table { get; set; }

        public ICollection<FilterValue> FilterValue { get; set; } = new HashSet<FilterValue>();
    }
}