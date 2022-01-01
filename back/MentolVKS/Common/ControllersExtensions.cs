using MentolVKS.Model.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace MentolVKS.Common
{
    public static class ControllersExtensions
    {
        public static void AddValidationErrors(this ModelStateDictionary modelState, IValidationErrors propertyErrors)
        {
            foreach (var databaseValidationError in propertyErrors.Errors)
            {
                modelState.AddModelError(databaseValidationError.PropertyName, databaseValidationError.PropertyExceptionMessage);
            }
        }

        public static void AddExceptionErrors(this ModelStateDictionary modelState, Exception propertyError)
        {
            modelState.AddModelError("", propertyError.Message);            
        }
    }
}
