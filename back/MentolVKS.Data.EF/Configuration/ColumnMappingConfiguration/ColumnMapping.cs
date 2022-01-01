using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using MentolVKS.Data.EF.Configuration.ColumnMapping;
using MentolVKS.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MentolVKS.Data.EF.Configuration.ColumnMapping
{
    public class ColumnMapping<T> : IColumnMapping<T>
    {
        private readonly IDictionary<string, IPropertyMap> _innerMappings;

        public ColumnMapping(IEntityType entityType)
        {
            _innerMappings = entityType.GetProperties().ToDictionary(i => i.Name, j =>
            {
                var property = new PropertyMap(j.Name);
                property.ToColumn(j.GetColumnName());
                return (IPropertyMap) property;
            });

            TableName = entityType.GetTableName();
        }

        public string TableName { get; }

        public IReadOnlyDictionary<string, IPropertyMap> Mappings => new ReadOnlyDictionary<string, IPropertyMap>(_innerMappings);

        public string ColumnName(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public string PropertyName(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }
    }
}