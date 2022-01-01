using MentolVKS.Data.Interfaces;
using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Classes
{
    public class QueryLinqFactory : IQueryLinqFactory
    {
        private readonly DataContext _context;
        public QueryLinqFactory(DataContext context)
        {
            _context = context;
        }
        /// <interitdoc/>     
        public IQueryLinq Create<TEntity>(ITableNameToModel model) where TEntity : EntityBase
        {
            return new QueryLinq<TEntity>(_context, model);
        }
    }
}
