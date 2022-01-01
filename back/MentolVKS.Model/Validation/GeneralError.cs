using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Validation
{
    public class GeneralError : IBaseError
    {
        #region Implementation of IBaseError

        public string PropertyName { get { return string.Empty; } }
        public string PropertyExceptionMessage { get; set; }

        public GeneralError(string errorMessage)
        {
            this.PropertyExceptionMessage = errorMessage;
        }

        #endregion
    }
}
