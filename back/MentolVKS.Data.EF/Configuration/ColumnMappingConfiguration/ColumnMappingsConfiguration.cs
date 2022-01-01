using System;
using System.Linq;
using MentolVKS.Data.Interfaces;

namespace MentolVKS.Data.EF.Configuration.ColumnMapping
{
    public class ColumnMappingConfiguration : IColumnMappingConfiguration
    {
        private readonly DataContext _context;

        public ColumnMappingConfiguration(DataContext context)
        {
            _context = context;
        }

        public IColumnMapping<T> GetMapping<T>()
        {
            var entityType = _context.Model.GetEntityTypes().FirstOrDefault(i => i.ClrType == typeof(T));

            if (entityType == null)
                throw new Exception($"Mapping information for type '{typeof(T).FullName}' is not found");

            return new ColumnMapping<T>(entityType);
        }

        public void AddMap<T>(IColumnMapping<T> mapping)
        {
            throw new NotSupportedException();
        }
    }
}
