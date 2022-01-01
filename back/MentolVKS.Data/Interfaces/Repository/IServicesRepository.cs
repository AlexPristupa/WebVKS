using MentolVKS.Model.BaseModel;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces.Repository
{
    /// <summary>
    /// Интерфейс репозитория для объектов типа <see cref="Services"/>
    /// </summary>
    public interface IServicesRepository : IRepository<Services>
    {
        /// <summary>
        /// Получает сервис по наимнованию
        /// </summary>
        /// <param name="name">Наименование сервиса</param>
        /// <returns></returns>
        Task<Services> GetByNameAsync(string name);
    }
}
