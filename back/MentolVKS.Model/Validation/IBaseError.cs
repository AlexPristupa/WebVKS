using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Validation
{
    public interface IBaseError
    {
        string PropertyName { get; }
        string PropertyExceptionMessage { get; }
    }
}
