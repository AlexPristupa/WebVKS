using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MentolVKS.Data.Interfaces
{
    public interface IColumnMapping<T>
    {
        string TableName { get; }

        IReadOnlyDictionary<string, IPropertyMap> Mappings { get; }

        string ColumnName(Expression<Func<T, object>> expression);

        string PropertyName(Expression<Func<T, object>> expression);
    }
}