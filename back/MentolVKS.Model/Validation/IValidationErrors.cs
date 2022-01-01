using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Validation
{
    public interface IValidationErrors
    {
        List<IBaseError> Errors { get; set; }
    }
}
