using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Validation
{
    public class PropertyError : IBaseError
    {
        public string PropertyName { get; set; }
        public string PropertyExceptionMessage { get; set; }
        public PropertyError(string propertyName, string errorMessage)
        {
            this.PropertyName =propertyName;
            this.PropertyExceptionMessage = errorMessage;
        }
    }
}
