using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
    public class FilterValue : TableBasedEntityBase
    {
        public int Id { get; set; }
        public int? FilterId { get; set; }
        public int? ColumnId { get; set; }
        public int? OperationId { get; set; }
        public string Fvalue { get; set; }

        public FilterColumnsList Column { get; set; }
        public FiltersList Filter { get; set; }
        public FilterOperationsList Operation { get; set; }
    }
}