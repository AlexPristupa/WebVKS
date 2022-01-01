using MentolVKS.Data.Interfaces;
using MentolVKS.Model.Filters.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Text;

namespace MentolVKS.Data.EF.Classes
{
    public class ColumnFilters : IColumnFilters
    {
        /// <summary>
        /// Словарь типов колонок
        /// </summary>
        protected IDictionary<string, string> DictionaryNameFieldToType { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dictionaryNameFieldToType">Словарь типов колонок</param>
        public ColumnFilters(IDictionary<string, string> dictionaryNameFieldToType)
        {
            DictionaryNameFieldToType = dictionaryNameFieldToType;
        }
        /// <inheritdoc/>
        public IQueryable GetColumnFiltersTypeDate(IQueryable query, FilterHandler item = null)
        {
            if (DictionaryNameFieldToType[item.NameField.ToLower()].Contains("DateTime"))
            {
                return GetColumnFiltersTypeDateDateTime(query, item);
            }
            else
            {
                return GetColumnFiltersTypeDateNullable(query, item);
            }
        }

        /// <summary>
        /// Получить колоночный фильтр date с DateTime?
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <param name="item">Dto</param>
        /// <returns></returns>
        private static IQueryable GetColumnFiltersTypeDateNullable(IQueryable query, FilterHandler item = null)
        {
            string sql = "";
            DateTime? dF = null;
            DateTime? dS = null;
            foreach (var v in item.ValuesField)
            {
                JObject ObjectFiltersDate = v as JObject;
                var children = ObjectFiltersDate.Children();
                // Названия очередности операции, как они названы на фронте
                List<string> priznakOperations = new List<string> { "selectFirstContains", "selectSecondContains" };
                // Значение даты операции, как они названы на фронте
                List<string> dateOperations = new List<string> { "valueFirstDateFilter", "valueSecondDateFilter" };
                // Значение времени операции, как они названы на фронте
                List<string> timeOperations = new List<string> { "timerFirstFilter", "timeSecondFilter" };
                // Значение даты для подстановки в запрос
                string valueDateFilter = "";

                string dateS = "";
                foreach (var itemChildren in children)
                {
                    JProperty poperty = itemChildren as JProperty;

                    // Выбираем знак сравнения для операции
                    if (priznakOperations.Contains(poperty.Name))
                    {
                        valueDateFilter += $"{item.NameField} {poperty.Value} ";
                    }
                    // Подставляем значение даты
                    else if (dateOperations.Contains(poperty.Name))
                    {
                        dateS = poperty.Value.ToString();
                    }

                    // Подставляем значение времени
                    else if (timeOperations.Contains(poperty.Name))
                    {
                        dateS += " " + poperty.Value;
                        if (poperty.Name == "timerFirstFilter")
                        {
                            try
                            {
                                dF = Convert.ToDateTime(dateS);
                            }
                            catch
                            {
                                dF = null;
                            }
                            valueDateFilter += "@0 AND ";
                        }
                        else
                        {
                            try
                            {
                                dS = Convert.ToDateTime(dateS);
                            }
                            catch
                            {
                                dS = null;
                            }
                            valueDateFilter += "@1";
                        }
                    }
                }

                sql += valueDateFilter;
            }

            return query.Where(sql, dF, dS);
        }

        /// <summary>
        /// Получить колоночный фильтр date с DateTime
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <param name="item">Dto</param>
        /// <returns></returns>
        private static IQueryable GetColumnFiltersTypeDateDateTime(IQueryable query, FilterHandler item = null)
        {
            string sql = "";
            DateTime dF = DateTime.Today;
            DateTime dS = DateTime.Today;
            foreach (var v in item.ValuesField)
            {
                JObject ObjectFiltersDate = v as JObject;
                var children = ObjectFiltersDate.Children();
                // Названия очередности операции, как они названы на фронте
                List<string> priznakOperations = new List<string> { "selectFirstContains", "selectSecondContains" };
                // Значение даты операции, как они названы на фронте
                List<string> dateOperations = new List<string> { "valueFirstDateFilter", "valueSecondDateFilter" };
                // Значение времени операции, как они названы на фронте
                List<string> timeOperations = new List<string> { "timerFirstFilter", "timeSecondFilter" };
                // Значение даты для подстановки в запрос
                string valueDateFilter = "";

                string dateS = "";
                foreach (var itemChildren in children)
                {
                    JProperty poperty = itemChildren as JProperty;

                    // Выбираем знак сравнения для операции
                    if (priznakOperations.Contains(poperty.Name))
                    {
                        valueDateFilter += $"{item.NameField} {poperty.Value} ";
                    }
                    // Подставляем значение даты
                    else if (dateOperations.Contains(poperty.Name))
                    {
                        dateS = poperty.Value.ToString();
                    }

                    // Подставляем значение времени
                    else if (timeOperations.Contains(poperty.Name))
                    {
                        dateS += " " + poperty.Value;
                        if (poperty.Name == "timerFirstFilter")
                        {
                            try
                            {
                                dF = Convert.ToDateTime(dateS);
                            }
                            catch
                            {
                                dF = DateTime.Today;
                            }
                            valueDateFilter += "@0 AND ";
                        }
                        else
                        {
                            try
                            {
                                dS = Convert.ToDateTime(dateS);
                            }
                            catch
                            {
                                dS = DateTime.Today;
                            }
                            valueDateFilter += "@1";
                        }
                    }
                }

                sql += valueDateFilter;
            }

            return query.Where(sql, dF, dS);
        }

        /// <inheritdoc/>
        public IQueryable GetColumnFiltersTypeInteger(IQueryable query, FilterHandler item)
        {
            string sql = "";

            foreach (var v in item.ValuesField)
            {
                (string, string) valueFilter = ParseValueFilter(v);
                sql += GetIntegerOperator(item.NameField.Trim(), valueFilter.Item1, valueFilter.Item2) + " OR ";
            }
            sql = sql.Remove(sql.Length - 4);

            return query.Where(sql);
        }

        /// <inheritdoc/>
        public IQueryable GetColumnFiltersTypeSelect(IQueryable query, FilterHandler filterHandler)
        {
            var listS = filterHandler.ValuesField?.Where(x => x != null).Select(x => x.ToString()).ToList();
            string sub_query = "";
            foreach (string it in listS)
            {
                sub_query += $@"it.{filterHandler.NameField}=""{it}"" OR ";
            }
            sub_query = sub_query.Remove(sub_query.Length - 4);

            return query.Where(sub_query);
        }

        /// <inheritdoc/>
        public IQueryable GetColumnFiltersTypeSelectPk(IQueryable query, FilterHandler filterHandler)
        {
            var listS = filterHandler.ValuesField?.Where(x => x != null).Select(x => x.ToString()).ToList();
            string sub_query = "";
            foreach (string it in listS)
            {
                sub_query += $@"it.{filterHandler.NameField}=""{it}"" AND ";
            }
            sub_query = sub_query.Remove(sub_query.Length - 4);

            return query.Where(sub_query);
        }

        /// <inheritdoc/>
        public IQueryable GetColumnFiltersTypeStringTree(IQueryable query, FilterHandler item)
        {
            string sql = "";

            foreach (var v in item.ValuesField)
            {
                (string, string) valueFilter = ParseValueFilter(v);
                sql += GetLikeOperator(item.NameField.Trim(), valueFilter.Item1.Trim(), valueFilter.Item2) + " OR ";
            }

            sql = sql.Remove(sql.Length - 4);

            return query.Where(sql);
        }

        /// <inheritdoc/>
        public IQueryable GetColumnFiltersTypeTime(IQueryable query, FilterHandler item)
        {
            string sql = "";
            DateTime dF = DateTime.Today;
            DateTime dS = DateTime.Today;
            foreach (var v in item.ValuesField)
            {
                JObject ObjectFiltersDate = v as JObject;
                var children = ObjectFiltersDate.Children();
                // Названия очередности операции, как они названы на фронте
                List<string> priznakOperations = new List<string> { "selectFirstContains", "selectSecondContains" };

                // Значение времени операции, как они названы на фронте
                List<string> timeOperations = new List<string> { "timerFirstFilter", "timeSecondFilter" };
                // Значение даты для подстановки в запрос
                string valueDateFilter = "";
                // TODO: Протестить формирование DLINQ
                string dateS = "1900-01-01";
                foreach (var itemChildren in children)
                {
                    JProperty poperty = itemChildren as JProperty;

                    // Выбираем знак сравнения для операции
                    if (priznakOperations.Contains(poperty.Name))
                    {
                        valueDateFilter += $"{item.NameField} {poperty.Value} ";
                    }

                    // Подставляем значение времени
                    else if (timeOperations.Contains(poperty.Name))
                    {
                        dateS += " " + poperty.Value;
                        if (poperty.Name == "timerFirstFilter")
                        {
                            dF = Convert.ToDateTime(dateS);
                            valueDateFilter += "@0 AND ";
                        }
                        else
                        {
                            dS = Convert.ToDateTime(dateS);
                            valueDateFilter += "@1";
                        }
                    }
                }

                sql += valueDateFilter;
            }

            return query.Where(sql, DateTime.Parse(dF.ToString("yyyy-MM-dd HH:mm:ss")), DateTime.Parse(dS.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        /// <summary>
        /// Сформировать числовой фильтр
        /// </summary>
        /// <param name="nameField">Поле для числового фильтра</param>
        /// <param name="operand">Операнд</param>
        /// <param name="valueFilter">Значение фильтрации</param>
        /// <returns></returns>
        private string GetIntegerOperator(string nameField, string operand, string valueFilter)
        {
            return $"{nameField} {operand} {valueFilter}";
        }

        /// <summary>
        /// Формируем LIKE для DLINQ
        /// </summary>
        /// <param name="nameField">Имя поля в БД</param>
        /// <param name="operand">Условие для LIKE</param>
        /// <param name="valueFilter">Значение фильтрации</param>
        private string GetLikeOperator(string nameField, string operand, string valueFilter)
        {
            string rc = "";
            operand = operand.Trim();
            char contStart = '"';
            // like '%{value}%'";
            char contEnd = '"';
            if (operand == "*~*")
            {
                rc += new StringBuilder($"{nameField}.Contains({contStart}{valueFilter}{contEnd})");
            }
            else if (operand == "!*~*")
            {
                rc += new StringBuilder($"!{nameField}.Contains({contStart}{valueFilter}{contEnd})");
            }
            else if (operand == "~")
            {
                rc += new StringBuilder($"{nameField}.Equals({contStart}{valueFilter}{contEnd})");
            }
            else if (operand == "!~")
            {
                rc += new StringBuilder($"!{nameField}.Equals({contStart}{valueFilter}{contEnd})");
            }
            else if (operand == "~*")
            {
                rc += new StringBuilder($"{nameField}.StartsWith({contStart}{valueFilter}{contEnd})");
            }
            else if (operand == "*~")
            {
                rc += new StringBuilder($"{nameField}.EndsWith({contStart}{valueFilter}{contEnd})");
            }
            else
            {
                rc += new StringBuilder($"{nameField} {operand} {contStart}{valueFilter}{contEnd}");
            }

            return rc;
        }

        /// <summary>
        /// Парсинг значений фильтра
        /// </summary>
        /// <param name="v">Объект фильтра</param>
        /// <returns></returns>
        private (string, string) ParseValueFilter(object v)
        {
            JObject ObjectFiltersDate = v as JObject;
            var children = ObjectFiltersDate.Children();

            // Названия очередности операции, как они названы на фронте
            List<string> priznakOperations = new List<string> { "operand" };
            // Значение операции, как они названы на фронте
            List<string> stringOperations = new List<string> { "compareValue" };

            // Значение даты для подстановки в запрос
            string operand = "";
            string stringValue = "";

            foreach (var itemChildren in children)
            {
                JProperty poperty = itemChildren as JProperty;

                // Выбираем знак сравнения для операции
                if (priznakOperations.Contains(poperty.Name))
                {
                    operand = $"{poperty.Value} ";
                }
                // Подставляем значение фильтра
                else if (stringOperations.Contains(poperty.Name))
                {
                    stringValue = poperty.Value.ToString().Replace('"', '\"').Replace("\"", "\\" + "\"");
                }
            }

            return (operand, stringValue.Trim());
        }
    }
}
