using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentolVKS.Common.TypeExtensions;
using MentolVKS.Data.Interfaces;

namespace MentolVKS.Data.Bases
{
    /// <summary>
    /// Базовый класс репозитория
    /// </summary>
    /// <typeparam name="TEntity">Тип объектов</typeparam>
    public abstract class RepositoryBase<TEntity>
    {
        /// <summary>
        /// Инициализирует экземпляр класса
        /// </summary>
        /// <param name="mappings">Конфигурация маппинга</param>
        protected RepositoryBase(IColumnMappingConfiguration mappings)
        {
            Mappings = mappings;
        }

        /// <summary>
        /// Конфигурация маппинга
        /// </summary>
        protected IColumnMappingConfiguration Mappings { get; }

        /// <summary>
        /// Добавление данных в базу
        /// </summary>
        /// <param name="entity">Данные для добавления</param>
        /// <returns>Добавленные данные</returns>
        public abstract Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Сохранение данных в базу
        /// </summary>
        /// <param name="entity">Данные для сохранения</param>
        /// <returns>Сохранённые данные</returns>
        public abstract Task<TEntity> SaveAsync(TEntity entity);

        /// <summary>
        /// Удаление данных из базы
        /// </summary>
        /// <param name="entity">Удаляемые данные</param>
        public abstract Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Удаление данных из базы
        /// </summary>
        /// <param name="keyValues">Идентификатор удаляемой записи</param>
        public abstract Task DeleteAsync(params object[] keyValues);

        /// <summary>
        /// Проверка существования сущности в таблице.
        /// </summary>
        /// <param name="id">Идентификатор записи.</param>
        /// <returns>Существование сущности: True/False.</returns>
        public abstract Task<bool> ExistByIdAsync(params object[] keyValues);

        /// <summary>
        /// Получение записи по идентификатору
        /// </summary>
        /// <param name="keyValues">Идентификатор записи</param>
        /// <returns>Данные записи</returns>
        public abstract Task<TEntity> GetByIdAsync(params object[] keyValues);

        /// <summary>
        /// Получение всех записей таблицы
        /// </summary>
        /// <returns>Список записей</returns>
        public abstract Task<IEnumerable<TEntity>> AllAsync();

        /// <summary>
        /// Добавление нескольких записей в базу
        /// </summary>
        /// <param name="entities">Список добавляемых записей</param>
        public abstract Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Сохранение нескольких записей в базу
        /// </summary>
        /// <param name="entities">Список сохраняемых записей</param>
        public abstract Task SaveRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Удаление нескольких записей в базу
        /// </summary>
        /// <param name="entities">Список удаляемых записей</param>
        public abstract Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Получает количество записей в таблице
        /// </summary>
        /// <returns>Количество записей</returns>
        public abstract Task<int> CountAsync();

        /// <summary>
        /// Выполняет запрос
        /// </summary>
        /// <param name="sql">SQL-запрос</param>
        /// <param name="parameters">Параметры запроса</param>
        /// <returns>Результат выполнения запроса</returns>
        protected abstract Task<object> ExecuteScalar(string sql, IDictionary<string, object> parameters);

        /// <summary>
        /// Получает количество записей из запроса
        /// </summary>
        /// <param name="sql">SQL-запрос</param>
        /// <param name="parameters">Параметры запроса</param>
        /// <returns></returns>
        protected virtual async Task<int> GetCountAsync(string sql, IDictionary<string, object> parameters)
        {
            var count = await ExecuteScalar(sql, parameters);
            //Npgsql возвращает object(Int64), а Mssql object(Int32)
            return count == null ? 0 : Convert.ToInt32(count);
        }

        /// <summary>
        /// Возвращает словарь связки свойства с именем поля
        /// </summary>
        /// <returns>Словарь связки</returns>
        protected Dictionary<string, string> GetColumnMapping()
        {
            return Mappings.GetMapping<TEntity>().Mappings.ToDictionary(v => v.Value.PropertyName.ToLower(), v => v.Value.ColumnName);
        }

        /// <summary>
        /// Словарь связки имени свойства с именем поля
        /// </summary>
        private Dictionary<string, string> Mapping { get; set; }

        /// <summary>
        /// Установка словаря связки
        /// </summary>
        private void SetMapping()
        {
            if (Mapping == null) Mapping = GetColumnMapping();
        }

        /// <summary>
        /// Добавление параметра маппинга, если такой ключ уже есть, то добавлен не будет
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        public void AddMapping(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return;

            key = key.ToLower();

            SetMapping();

            if (!Mapping.ContainsKey(key)) Mapping.Add(key, value);
        }

        /// <summary>
        /// Изменение параметра маппинга
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        public void UpdateMapping(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return;

            key = key.ToLower();

            SetMapping();

            if (Mapping.ContainsKey(key)) Mapping[key] = value;
        }

        /// <summary>
        /// Удаляет параметр маппинга
        /// </summary>
        /// <param name="key">Ключ</param>
        public void RemoveMapping(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            key = key.ToLower();

            SetMapping();

            Mapping.Remove(key);
        }

        /// <summary>
        /// Возвращает поля БД
        /// </summary>
        public IEnumerable<string> GetFields()
        {
            SetMapping();

            return Mapping.Values;
        }

        /// <summary>
        /// Проверяет наличие ключа маппинга
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Результат проверки</returns>
        public bool MappingContainsKey(string key)
        {
            SetMapping();

            return Mapping.ContainsKey(key.ToLower());
        }

        /// <summary>
        /// Получает имя поля по имени свойства
        /// </summary>
        /// <param name="propertyName">Имя свойства</param>
        /// <returns></returns>
        protected string GetColumnNameByProperty(string propertyName)
        {
            SetMapping();

            if (Mapping == null) return propertyName;

            if (string.IsNullOrEmpty(propertyName)) return string.Empty;

            if (!Mapping.TryGetValue(propertyName.ToLower(), out var columnName))
            {
                columnName = Mapping.Values.FirstOrDefault(m => m.Is(propertyName));

                return columnName ?? propertyName;
            }

            return columnName;
        }
    }
}