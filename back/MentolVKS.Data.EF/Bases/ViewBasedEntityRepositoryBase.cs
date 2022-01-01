using MentolVKS.Data.Interfaces;
using MentolVKS.Model.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Bases
{
    /// <summary>
    /// Базовые класс представлений БД
    /// </summary>
    /// <typeparam name="TEntity">Тип объекта</typeparam>
    public abstract class ViewBasedEntityRepositoryBase<TEntity> : RepositoryBase<TEntity> where TEntity : ViewBasedEntityBase
    {
        /// <summary>
        /// Инициализирует экземпляр класса
        /// </summary>
        /// <param name="context">Контекст БД</param>
        /// <param name="mappings">Конфигурация маппинга</param>
        protected ViewBasedEntityRepositoryBase(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings) { }

        #region Overrides of RepositoryBase<TEntity>

        /// <inheritdoc />
        public override Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override Task<TEntity> SaveAsync(TEntity entity)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override Task DeleteAsync(TEntity entity)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override Task DeleteAsync(params object[] keyValues)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override Task<bool> ExistByIdAsync(params object[] keyValues)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override Task<TEntity> GetByIdAsync(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await DbSet.ToListAsync();
        }

        /// <inheritdoc />
        public override Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override Task SaveRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override Task<int> CountAsync()
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}