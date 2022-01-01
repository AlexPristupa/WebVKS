using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.Interfaces
{
    /// <summary>
    /// Фабрика моделей 
    /// </summary>
    public interface IModelFactory
    {
        /// <summary>
        /// Сопоставление имени таблицы определенной модели
        /// </summary>
        IList<ITableNameToModel> TableNameToModels { get; }
    }
}
