using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MentolVKS.Data.Interfaces;
using MentolVKS.Model.Bases;
using Microsoft.EntityFrameworkCore;

namespace MentolVKS.Data.EF.Bases
{
    /// <summary>
    /// Базовые класс таблиц БД
    /// </summary>
    /// <typeparam name="TEntity">Тип объекта</typeparam>
    public abstract class TableBasedEntityRepositoryBase<TEntity> : RepositoryBase<TEntity>
        where TEntity : TableBasedEntityBase
    {
        /// <summary>
        /// Инициализирует экземпляр класса
        /// </summary>
        /// <param name="context">Контекст БД</param>
        /// <param name="mappings">Конфигурация маппинга</param>
        protected TableBasedEntityRepositoryBase(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings) { }

        /// <summary>
        /// Сохраняет данные
        /// </summary>
        /// <returns>Количество сохранённых строк</returns>
        protected virtual Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="entity">Обновляемые данные</param>
        /// <returns>Обновлённые данные</returns>
        protected virtual TEntity Update(TEntity entity)
        {
            var entry = Context.Entry(entity);
            DbSet.Attach(entity);
            entry.State = EntityState.Modified;

            return entity;
        }

        /// <summary>
        /// Получает данные из БД
        /// </summary>
        /// <param name="filter">Условие отбора</param>
        /// <param name="orderBy">Условие сортировки</param>
        /// <param name="includeProperties">Связные данные</param>
        /// <returns>Список записей</returns>
        protected virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            var query = GetQuery(filter, orderBy, includeProperties);
            var list = await query.AsNoTracking().ToListAsync();

            return list;
        }

        /// <summary>
        /// Получает IQueryable данные из БД
        /// </summary>
        /// <returns>IQueryable данные</returns>
        protected virtual IQueryable<TEntity> Query()
        {
            return DbSet.AsQueryable();
        }

        /// <summary>
        /// Получает IQueryable данные из БД
        /// </summary>
        /// <param name="filter">Условие отбора</param>
        /// <param name="orderBy">Условие сортировки</param>
        /// <param name="includeProperties">Связные данные</param>
        /// <returns>IQueryable данные</returns>
        private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null) query = query.Where(filter);

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query) : query;
        }

        #region Overrides of RepositoryBase<TEntity>

        /// <inheritdoc />
        public override async Task<TEntity> AddAsync(TEntity entity)
        {
            DbSet.Add(entity);

            await SaveChangesAsync();

            Context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        /// <inheritdoc />
        public override async Task<TEntity> SaveAsync(TEntity entity)
        {
            Update(entity);

            await SaveChangesAsync();

            Context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        /// <inheritdoc />
        public override async Task DeleteAsync(TEntity entity)
        {
            if(entity == null)
                return;

            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            DbSet.Remove(entity);

            await SaveChangesAsync();
        }

        /// <inheritdoc />
        public override async Task DeleteAsync(params object[] keyValues)
        {
            var entityToDelete = await GetByIdAsync(keyValues);
            await DeleteAsync(entityToDelete);
        }

        /// <inheritdoc />
        public override async Task<bool> ExistByIdAsync(params object[] keyValues)
        {
            var entity = await DbSet.FindAsync(keyValues);

            return entity != null;
        }

        /// <inheritdoc />
        public override async Task<TEntity> GetByIdAsync(params object[] keyValues)
        {
            return await DbSet.FindAsync(keyValues);
        }

        /// <inheritdoc />
        public override async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await DbSet.ToListAsync();
        }

        /// <inheritdoc />
        public override async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);

            await SaveChangesAsync();
        }

        /// <inheritdoc />
        public override async Task SaveRangeAsync(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);

            await SaveChangesAsync();
        }

        /// <inheritdoc />
        public override async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);

            await SaveChangesAsync();
        }

        /// <inheritdoc />
        public override async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        #endregion

        /// <summary>
        /// Обновляет генератор последовательности
        /// </summary>
        /// <param name="sequenseName">Имя генератора</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="idField">Поле идентификатора</param>
        protected async Task UpdateSequenseAsync(string sequenseName = null, string tableName = null, string idField = "idr")
        {
            if (!Context.Database.IsNpgsql()) return;

            tableName = tableName ?? Mappings.GetMapping<TEntity>().TableName;
            sequenseName = sequenseName ?? $"{tableName}_{idField}_seq";

            using var cmd = Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = $"SELECT setval('dbo.{sequenseName}', (SELECT max({idField}) FROM dbo.{tableName}))";

            if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }
    }
}