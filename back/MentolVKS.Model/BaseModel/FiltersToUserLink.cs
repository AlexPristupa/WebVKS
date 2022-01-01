using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
    public class FiltersToUserLink : TableBasedEntityBase
    {
        public int Id { get; set; }
        public int? FilterId { get; set; }
        public int? UserId { get; set; }
        public int? IsActive { get; set; }

        public FiltersList Filter { get; set; }
    }
}