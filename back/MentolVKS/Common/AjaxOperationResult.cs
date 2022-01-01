using MentolVKS.Common.TypeExtensions;
using MentolVKS.Model.Enums;
using MentolVKS.Model.Error;

namespace MentolVKS.Common
{
    /// <summary>
    /// Результат Ajax вызова
    /// </summary>
    public class AjaxOperationResult : ServerOperationResult<object>
    {
        /// <summary>
        /// TODO Comment
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static AjaxOperationResult Success(object data = null, string message = null)
        {
            return CreateResult(OperationResult.Success, message, data);
        }

        /// <summary>
        /// TODO Comment
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static AjaxOperationResult Error(string message = null, object data = null)
        {
            return CreateResult(OperationResult.Error, message, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static AjaxOperationResult Error(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary data)
        {
            var result = new ValidationError();
            
            foreach(var el in data)
            {
                foreach(var el2 in data[el.Key].Errors)
                {
                    result.AddFieldError(el.Key.ToCamelCase(), el2.ErrorMessage, el2.ErrorMessage);
                }
            }

            return CreateResult(OperationResult.Error, null, result);
        }

        /// <summary>
        /// Возвращает предупреждение на фронт.
        /// </summary>
        /// <param name="message">Общая информация по предупреждению.</param>
        /// <param name="data">Уточняющая информация по предупреждению.</param>
        /// <returns>Результат Ajax вызова.</returns>
        public static AjaxOperationResult Warning(string message = null, object data = null)
        {
            return CreateResult(OperationResult.Warning, message, data);
        }

        private static AjaxOperationResult CreateResult(OperationResult result, string message, object data)
        {
            return new AjaxOperationResult { Result = result, Message = message, Data = data };
        }
    }
}
