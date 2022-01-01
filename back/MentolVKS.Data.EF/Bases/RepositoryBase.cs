using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MentolVKS.Data.Interfaces;
using MentolVKS.Model.Bases;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using DbType = MentolVKS.Model.Enums.DbType;

namespace MentolVKS.Data.EF.Bases
{
    /// <summary>
    /// Базовый класс репозитория
    /// </summary>
    /// <typeparam name="TEntity">Тип модели</typeparam>
    public abstract class RepositoryBase<TEntity> : Data.Bases.RepositoryBase<TEntity>, IDisposable where TEntity : EntityBase
    {
        /// <summary>
        /// Инициализирует экземпляр класса
        /// </summary>
        /// <param name="context">Контекст БД</param>
        /// <param name="mappings">Маппинг полей</param>
        protected RepositoryBase(DataContext context, IColumnMappingConfiguration mappings) : base(mappings)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }
        /// <summary>
        /// Контекст БД
        /// </summary>
        protected DataContext Context { get; }

        /// <summary>
        /// Набор данных модели
        /// </summary>
        protected DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// Получает набор данных из запроса
        /// </summary>
        /// <param name="query">Данные запроса</param>
        /// <returns></returns>
        /*protected IQueryable<TEntity> FromQuery(IQuery<TEntity> query)
        {
            var result = query.Compile();

            if (Context.Database.IsNpgsql())
            {
                return Context.Set<TEntity>()
                    .FromSqlRaw(result.Sql.ToLower(), result.Bindings.ToArray()).AsNoTracking();
            }

            return Context.Set<TEntity>()
                .FromSqlRaw(result.Sql, result.Bindings.ToArray());
        }*/

        /// <summary>
        /// Получает количество записей из запроса
        /// </summary>
        /// <param name="query">SQL-запрос</param>
        /// <returns>Количество записей</returns>
       /* protected async Task<int> GetCountAsync(IQuery<TEntity> query)
        {
            var result = query.Compile();

            var parameters = result.Bindings.Select((value, index) => new { Value = value, Index = index }).ToDictionary(item => $"@p{item.Index}", item => item.Value);

            if (Context.Database.IsNpgsql())
            {
                //TODO Надо убрать SQlKata пока костыль
                var sql = result.Sql.ToLower();
                sql = sql.Replace("select \"id\"", "select * ");
                sql = sql.Replace("count(*)", "cast(count(*) as int)");

                return await GetCountAsync(sql, parameters);
            }

            return await GetCountAsync(result.Sql, parameters);
        }*/

        /// <summary>
        /// Возвращает команду в зависимости от текущий БД
        /// </summary>
        protected DbCommand GetDBCommand()
        {
            DbCommand cmd;

            if (Context.Database.IsNpgsql())
            {
                cmd = new NpgsqlCommand();
            }
            else
            {
                cmd = new SqlCommand();
            }

            return cmd;
        }

        /// <summary>
        /// Возвращает направление сортировки
        /// </summary>
        /// <param name="filterParams">Данные фильтра</param>
        /// <returns>Направление сортировки</returns>
        /*protected string GetOrderDir(FilterParametrsDto filterParams)
        {
            return filterParams.OrderDir == SortDirection.Ascending ? " asc " : " desc ";
        }*/

        /// <summary>
        /// Возвращает имя поля для сортировки
        /// </summary>
        /// <param name="filterParams">Данные фильтра</param>
        /// <returns>Имя поля</returns>
        /*protected string GetOrderColumn(FilterParametrsDto filterParams)
        {
            return GetColumnNameByProperty(filterParams.OrderColumnName);
        }*/

        /// <summary>
        /// Возвращает запрос для текстового поля
        /// </summary>
        /// <param name="operand">Операнд</param>
        /// <param name="columnName">Имя поля</param>
        /// <param name="value">Значение</param>
        /// <returns>Запрос для поля</returns>
        /*protected string GetStringCondition(string operand, string columnName, string value)
        {
            value = string.IsNullOrEmpty(value) ? string.Empty : value.ToLower();

            if (Context.Database.IsNpgsql())
            {
                switch (operand)
                {
                    case "~": return $"{columnName} ilike '{value}'";
                    case "!~": return $"{columnName} not ilike '{value}'";
                    case "=": return $"lower({columnName}) = '{value}'";
                    case "!=": return $"lower({columnName}) != '{value}'";
                    case "~*": return $"{columnName} ilike '{value}%'";
                    case "!~*": return $"{columnName} not ilike '%{value}'";
                    case "*~": return $"{columnName} ilike '%{value}'";
                    case "*!~": return $"{columnName} not ilike '{value}%'";
                    case "*~*": return $"{columnName} ilike '%{value}%'";
                    case "!*~*": return $"{columnName} not ilike '%{value}%'";
                    default: return string.Empty;
                }
            }
            else
            {
                switch (operand)
                {
                    case "~": return $"{columnName} like '{value}'";
                    case "!~": return $"{columnName} not like '{value}'";
                    case "=": return $"{columnName} = '{value}'";
                    case "!=": return $"{columnName} != '{value}'";
                    case "~*": return $"{columnName} like '{value}%'";
                    case "!~*": return $"{columnName} not like '%{value}'";
                    case "*~": return $"{columnName} like '%{value}'";
                    case "*!~": return $"{columnName} not like '{value}%'";
                    case "*~*": return $"{columnName} like '%{value}%'";
                    case "!*~*": return $"{columnName} not like '%{value}%'";
                    default: return string.Empty;
                }
            }
        }*/

        /// <summary>
        /// Возвращает часть запроса после where
        /// </summary>
        /// <param name="filterParams">Данные фильтра</param>
        /// <param name="field">Фильтр только по указанному полю</param>
        /// <param name="exceptField">Фильтр по всем полям кроме указанного</param>
        /// <returns>Запрос выборки</returns>
       /* protected string GetQuery(FilterParametrsDto filterParams, string field = null,
            string[] exceptFields = null, DbType dbType = DbType.MsSql)
        {
            //Не понятно зачем передавать как параметр. И с какой целью может понадобится на PG запрос от MSSQL и наоборот
            //Установил по умолчанию т.к. почти везде вызов без параметров и на PG ошибки
            dbType = Context.DataBaseType;

            if (filterParams.ColumnFilters == null || filterParams.ColumnFilters.Count == 0) return string.Empty;

            var strSql = string.Empty;
            var separator = "and";

            foreach (var filter in filterParams.ColumnFilters)
            {
                var sql = string.Empty;

                if (!string.IsNullOrEmpty(field) && !filter.ColumnName.Is(field)) continue;

                if (exceptFields != null && exceptFields.Contains(filter.ColumnName, StringComparer.OrdinalIgnoreCase)) continue;

                var vm = filter.SelectList;
                var columnName = GetColumnNameByProperty(filter.ColumnName);

                if (string.IsNullOrEmpty(columnName)) continue;

                switch (filter.Type)
                {
                    case ColumnFiltersType.Select:
                        sql += SqlGenerationUtility.GetSelectFilter(filter, columnName);
                        break;
                    case ColumnFiltersType.String:
                    case ColumnFiltersType.Tree:
                        sql += SqlGenerationUtility.GetStringFilter(filter, columnName, dbType);
                        break;
                    case ColumnFiltersType.SelectFts:
                        sql += SqlGenerationUtility.GetSelectFtsFilter(filter, columnName);
                        break;
                    case ColumnFiltersType.StringFts:
                    case ColumnFiltersType.StructureFts:
                        sql += SqlGenerationUtility.GetStringFtsFilter(filter, columnName);
                        break;
                    case ColumnFiltersType.Integer:
                        sql += SqlGenerationUtility.GetIntegerFilter(filter, columnName);
                        break;
                    case ColumnFiltersType.Date:
                        sql += SqlGenerationUtility.GetDateFilter(filter, columnName);
                        break;
                    case ColumnFiltersType.DateAsString:
                        sql += SqlGenerationUtility.GetDateAsStringFilter(filter, columnName);
                        break;
                    case ColumnFiltersType.Time:
                        sql += SqlGenerationUtility.GetTimeFilter(filter, columnName);
                        break;
                    case ColumnFiltersType.Boolean:
                        sql += SqlGenerationUtility.GetBooleanFilter(filter, columnName);
                        break;
                }

                if (!string.IsNullOrEmpty(sql))
                {
                    strSql += (string.IsNullOrEmpty(strSql) ? string.Empty : $" {separator} ") + "(" + sql + ")";
                }
            }

            return strSql;
        }*/


        /// <summary>
        /// Возвращает функцию для вычисления года из даты в зависимости от БД
        /// </summary>
        /// <param name="fieldName">Имя поля</param>
        protected string GetYearFunction(string fieldName)
        {
            return Context.Database.IsNpgsql()
                    ? $"extract(YEAR from {fieldName})"
                    : $"YEAR({fieldName})";
        }

        /// <summary>
        /// Возвращает функцию получения текущей даты в зависимости от БД
        /// </summary>
        protected string GetCurentDateFunction()
        {
            return Context.Database.IsNpgsql() ? "now()" : "getdate()";
        }

        /// <summary>
        /// Оператор SQL LIKE
        /// </summary>
        protected string SqlLike => Context.Database.IsNpgsql() ? "ilike" : "like";

        /// <summary>
        /// Возвращает имя таблицы со схемой dbo
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        protected string GetTableName(string tableName)
        {
            if (string.IsNullOrEmpty(tableName)) return string.Empty;

            var dboRegex = new Regex(@"^dbo\.\w+");

            return dboRegex.IsMatch(tableName) ? tableName : $"dbo.{tableName}";
        }


#warning переделать
        /// <summary>
        /// Возвращает часть запроса с сортировкой
        /// </summary>
        /// <param name="filter">Данные фильтра</param>
        /// <returns>Часть SQL-запроса с сортировкой</returns>
       /* protected string GetOrderQuery(FilterParametrsDto filter)
        {
            var sql = string.Empty;
            var orderBy = GetOrderColumn(filter);

            if (!string.IsNullOrEmpty(orderBy))
            {
                var orderDir = GetOrderDir(filter);

                sql += $" order by {orderBy} {orderDir} offset {filter.Start} rows";

                if (filter.Length > 0) sql += $" fetch next {filter.Length} rows only";
            }

            return sql;
        }*/

        /// <summary>
        /// Возвращает таймаут выполнения команды
        /// </summary>
        /// <param name="timeOut">Пользовательское значение</param>
        /// <returns>Таймаут в секундах</returns>
        protected virtual int GetCommandTimeout(int timeOut = 180)
        {
            var systemTimeout = Context.Database.GetCommandTimeout() ?? timeOut;

            return Math.Max(systemTimeout, timeOut);
        }

        /// <summary>
        /// Возвращает параметр команды SQL
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <param name="value">Значение параметра</param>
        protected DbParameter CreateParameter(string name, object value)
        {
            if (Context.Database.IsNpgsql())
                return new NpgsqlParameter { ParameterName = name, Value = value };

            return new SqlParameter { ParameterName = name, Value = value };
        }

        /// <summary>
        /// Возвращает параметр команды SQL
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <param name="value">Значение параметра</param>
        /// <param name="type">Тип параметра</param>
        protected DbParameter CreateParameter(string name, object value, System.Data.DbType type)
        {
            if (Context.Database.IsNpgsql())
                return new NpgsqlParameter { ParameterName = name, Value = value, DbType = type };

            return new SqlParameter { ParameterName = name, Value = value, DbType = type };
        }

        /// <summary>
        /// Возвращает строку с оператором ARITHABORT для ms sql
        /// </summary>
        /// <returns>Строка с оператором ARITHABORT</returns>
        protected string Arithabort()
        {
            // todo поправить ещё в 3-х местах
            var arithabort = string.Empty;
            if (Context.Database.IsSqlServer())
                arithabort = $@" SET ARITHABORT ON
                                         {Environment.NewLine}";
            return arithabort;
        }

        /// <summary>
        /// Унифицированный вызов процедуры для MS и PG
        /// </summary>
        /// <param name="procedureName">Наименование процедуры</param>
        /// <param name="parameters">Параметры которые принимает процедуры</param>
        /// <returns>Результат выполнения</returns>
        protected async Task<object> CallProcedure(string procedureName, object[] parameters = null)
        {
            if (Context.Database.IsNpgsql())
            {
                if (parameters == default)
                {
                    return await Context.Database.ExecuteSqlRawAsync($"select {procedureName}()");
                }

                return await Context.Database.ExecuteSqlRawAsync($"select {procedureName}({GetProcedureDeclaration(parameters)})", parameters);
            }
            else
                return await Context.Database.ExecuteSqlRawAsync($"execute {procedureName} {GetProcedureDeclaration(parameters)}", parameters);
        }

        /// <summary>
        /// Возвращает набор параметров сформированных для процедуры
        /// </summary>
        /// <param name="parameters">Параметры которые принимает процедуры</param>
        /// <returns>Декларация параметров в виде строки</returns>
        private string GetProcedureDeclaration(object[] parameters)
        {
            if (parameters == default) return string.Empty;

            var result = string.Empty;

            var parCount = parameters.Count();

            for (var i = 0; i < parCount; i++)
                result += $"@p{i}" + (i + 1 < parCount ? "," : string.Empty);

            return result;
        }

        /// <summary>
        /// Унифицированный метод для работы с bool в MS и PG
        /// </summary>
        /// <param name="value">Значение которое нужно конвертировать</param>
        /// <returns>Сконвертированное значение в виде строки</returns>
        protected string ConvertIntToBoolStr(int value)
        {
            return Context.Database.IsNpgsql() ? Convert.ToBoolean(value).ToString() : value.ToString();
        }

        /// <summary>
        /// Унифицированный метод для работы с DateTime в MS и PG
        /// </summary>
        /// <returns>DateTime в виде строки</returns>
        protected string GetCurrentDate()
        {
            return Context.Database.IsNpgsql() ? "now()" : "getdate()";
        }

        /// <summary>
        /// Унифицированный метод для работы с RECURSIVE в конструкции WITH в MS и PG 
        /// </summary>
        /// <returns>Строка для WITH</returns>
        protected string GetRecursive()
        {
            return Context.Database.IsNpgsql() ? "RECURSIVE" : string.Empty;
        }

        /// <summary>
        /// Унифицированный метод для работы с RECURSIVE в конструкции WITH в MS и PG 
        /// </summary>
        /// <param name="start">Начальный элемент</param>
        /// <param name="length">Количество элементов</param>
        /// <returns>Строка для фильтрации</returns>
        protected string GetOffset(long start, int length)
        {
            if (Context.Database.IsNpgsql())
                return $"OFFSET {start} limit {length}";
            else
                return $"OFFSET {start} ROWS FETCH NEXT {length} ROWS ONLY";
        }

        #region Overrides of RepositoryBase<TEntity>

        /// <inheritdoc />
        protected override async Task<object> ExecuteScalar(string sql, IDictionary<string, object> parameters)
        {
            using (var cmd = Context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandTimeout = GetCommandTimeout();

                if (cmd.Connection.State != ConnectionState.Open) await cmd.Connection.OpenAsync();

                cmd.Parameters.AddRange(parameters.Select(i =>
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = i.Key;
                    dbParameter.Value = i.Value;

                    return dbParameter;
                }).ToArray());

                return await cmd.ExecuteScalarAsync();
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Освобождает ресурсы
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion
    }
}