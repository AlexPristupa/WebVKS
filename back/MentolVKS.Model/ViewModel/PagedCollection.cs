using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MentolVKS.Model.ViewModel
{
    /// <summary>
    /// Данные пэйджинга таблицы
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    [JsonObject]
    [DataContract]
    public class PagedCollection<T> : IEnumerable<T> where T : class
    {
        public PagedCollection()
        {
            Items = Enumerable.Empty<T>();
        }

        public PagedCollection(IEnumerable<T> items, int pageIndex = 0, int pageSize = 0, int total = 0)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Total = total;
        }

        /// <summary>
        /// Список данных
        /// </summary>
        [DataMember]
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Количество строк
        /// </summary>
        [DataMember]
        public int Total { get; set; }

        /// <summary>
        /// Количество странциц
        /// </summary>
        [DataMember]
        public int TotalPages => PageSize > 0 ? Total / PageSize : 0;

        /// <summary>
        /// Размер страницы
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// Номер страницы
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }

        /// <summary>
        ///  Имя таблицы, полученное из dto, отданного в запросе к фабрике
        /// </summary>
        [DataMember]
        public string TableName { get; set; }

        public static implicit operator PagedCollection<T>(List<T> items)
        {
            return new PagedCollection<T>(items, 0, items.Count, items.Count);
        }

        public static implicit operator List<T>(PagedCollection<T> pagedCollection)
        {
            return pagedCollection.Items.ToList();
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// Возвращает перечислитель
        /// </summary>
        /// <returns>Перечислитель</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        /// Возвращает перечислитель
        /// </summary>
        /// <returns>Перечислитель</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
