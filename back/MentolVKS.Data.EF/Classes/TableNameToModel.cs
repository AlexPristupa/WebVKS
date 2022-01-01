using MentolVKS.Data.Interfaces;
using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Classes
{
    public class TableNameToModel : ITableNameToModel
    {
        public TableNameToModel(string tableName, EntityBase model, IEnumerable<string> excludeFields = null, IEnumerable<string> selectTitle = null)
        {
            TableName = tableName;
            Model = model;
            ExcludeFields = excludeFields;
            SelectTitle = selectTitle;
        }
        /// <inheritdoc />
        public string TableName { get; set; }
        /// <inheritdoc />      
        public EntityBase Model { get; set; }
        /// <inheritdoc />
        public IEnumerable<string> ExcludeFields { get; set; }
        /// <inheritdoc/>
        public IEnumerable<string> SelectTitle { get; set; }
    }
}
