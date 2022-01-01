using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Enums
{
    public enum OperationResult
    {
        [Display(Name = "Success")]
        Success = 0,

        [Display(Name = "Error")]
        Error = -1,

        [Display(Name = "Warning")]
        Warning = -2
    }
}
