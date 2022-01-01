using System.ComponentModel.DataAnnotations;

namespace MentolVKS.Model.Filters.Dto
{
    public enum SortDirection
    {
        [Display(Name = "Ascending")]
        Ascending = 1,
        [Display(Name = "Descending")]
        Descending
    }
}