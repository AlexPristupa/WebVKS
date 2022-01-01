using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MentolVKS.Model.Error
{
    public class ValidationError
    {
        public List<Error> Validation { get; set; } = new List<Error>();

        public void AddFieldError(string fieldName, string message, string exception)
        {
           var el = Validation.FirstOrDefault(c => c.Field == fieldName);
            if (el == null)
            {
                var buf = new Error();
                buf.Field = fieldName;
                buf.Errors.Add(new ErrorDetail { Exception =exception, Message = message });
                Validation.Add(buf);
            }
            else
            {
                el.Errors.Add(new ErrorDetail { Exception = exception, Message = message });
            }
        }

        public void AddFormError(string message, string exception)
        {
            AddFieldError(string.Empty, message, exception);
        }
    }


    public class Error
    {
        public string Field { get; set; }
        public List<ErrorDetail> Errors { get; set; } = new List<ErrorDetail>();
    }

    public class ErrorDetail
    {
        public string Exception { get; set; }
        public string Message { get; set; }
    }
}
