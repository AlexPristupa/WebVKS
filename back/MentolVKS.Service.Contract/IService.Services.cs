using MentolVKS.Model.BaseModel;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Получает сервис по наимнованию
        /// </summary>
        /// <param name="name">Наименование сервиса</param>
        /// <returns></returns>
        Task<Services> ServicesGetByNameAsync(string name);
    }
}
