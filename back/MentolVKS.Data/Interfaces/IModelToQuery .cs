using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.Interfaces
{
    /// <summary>
    /// Вызвать фабрику запросов для модели
    /// </summary>
    /// <typeparam name="T">Модель</typeparam>
    public interface IModelToQuery
    {
        /// <summary>
        /// Выполнить зарос DLINQ для модели
        /// </summary>
        /// <param name="model">Модель</param>
        /// <returns></returns>
        Task<dynamic> GetDataAsync<T>() where T : EntityBase;
    }
}
