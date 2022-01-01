using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces
{
    /// <summary>
    /// Базовый интерфейс репозитория
    /// </summary>
    /// <typeparam name="TEntity">Тип объектов</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Добавление данных в базу
        /// </summary>
        /// <param name="entity">Данные для добавления</param>
        /// <returns>Добавленные данные</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Сохранение данных в базу
        /// </summary>
        /// <param name="entity">Данные для сохранения</param>
        /// <returns>Сохранённые данные</returns>
        Task<TEntity> SaveAsync(TEntity entity);

        /// <summary>
        /// Удаление данных из базы
        /// </summary>
        /// <param name="entity">Удаляемые данные</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Удаление данных из базы
        /// </summary>
        /// <param name="keyValues">Идентификатор удаляемой записи</param>
        Task DeleteAsync(params object[] keyValues);

        /// <summary>
        /// Проверка существования сущности в таблице.
        /// </summary>
        /// <param name="id">Идентификатор записи.</param>
        /// <returns>Существование сущности: True/False.</returns>
        Task<bool> ExistByIdAsync(params object[] keyValues);

        /// <summary>
        /// Получение записи по идентификатору
        /// </summary>
        /// <param name="keyValues">Идентификатор записи</param>
        /// <returns>Данные записи</returns>
        Task<TEntity> GetByIdAsync(params object[] keyValues);

        /// <summary>
        /// Получение всех записей таблицы
        /// </summary>
        /// <returns>Список записей</returns>
        Task<IEnumerable<TEntity>> AllAsync();

        /// <summary>
        /// Добавление нескольких записей в базу
        /// </summary>
        /// <param name="entities">Список добавляемых записей</param>
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Сохранение нескольких записей в базу
        /// </summary>
        /// <param name="entities">Список сохраняемых записей</param>
        Task SaveRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Удаление нескольких записей из базы
        /// </summary>
        /// <param name="entities">Список удаляемых записей</param>
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Получает количество записей в таблице
        /// </summary>
        /// <returns>Количество записей</returns>
        Task<int> CountAsync();
    }
}
