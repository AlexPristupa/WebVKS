using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MentolVKS.Model.Bases
{
    /// <summary>
    /// Базовый класс для классов объектной модели системы.
    /// </summary>
    public abstract class ModelBase
    {
        /// <summary>
        /// Возвращает JSON-представление объекта.
        /// </summary>
        /// <returns></returns>
        public virtual string ToJson()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(this, Formatting.None, settings);
        }
    }
}
