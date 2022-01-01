using MentolVKS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Classes.ModelsFactory
{
    public partial class ModelFactory : IModelFactory
    {
        public ModelFactory()
        {
            GetUser();
        }
        /// <summary>
        /// Список сопоставления имен таблиц моделям в БД
        /// </summary>
        public IList<ITableNameToModel> TableNameToModels { get; set; } = new List<ITableNameToModel>();
    }
}
