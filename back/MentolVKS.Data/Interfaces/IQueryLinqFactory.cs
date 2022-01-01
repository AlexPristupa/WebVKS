using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.Interfaces
{
    /// <summary>
    /// Фабрика LINQ Запросов
    /// </summary>
    public interface IQueryLinqFactory
    {
        /// <summary>
        /// Интерфейс создания linq запроса для объектов типа <see cref="TEntity" /> 
        /// </summary>
        /// <typeparam name="TEntity">Сущность БД</typeparam>
        /// <param name="model">Модель сопоставления имени таблицы с сущностью БД</param>     
        /// <returns></returns>
        IQueryLinq Create<TEntity>(ITableNameToModel model) where TEntity : EntityBase;
    }
}
