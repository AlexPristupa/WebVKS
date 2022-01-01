using System.Collections.Generic;
using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
    public class FiltersList : TableBasedEntityBase
    {
        public int Id { get; set; }
        public string FilterName { get; set; }
        public int IsCommon { get; set; }

        public ICollection<FiltersToUserLink> FiltersToUserLink { get; set; } = new HashSet<FiltersToUserLink>();
        public ICollection<FilterValue> FilterValue { get; set; } = new HashSet<FilterValue>();
    }
}