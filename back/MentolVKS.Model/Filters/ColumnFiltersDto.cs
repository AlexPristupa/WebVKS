using MentolVKS.Common.TypeExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MentolVKS.Model.Filters.Dto
{
    /// <summary>
    /// Фильтр колонки
    /// </summary>
    public class ColumnFiltersDto
    {
        /// <summary>
        /// Имя колонки
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Список значений для выбора
        /// </summary>
        public string[] SelectList { get; set; }

        /// <summary>
        /// Операнд
        /// </summary>
        public string Operand { get; set; }

        /// <summary>
        /// Строка для поиска
        /// </summary>
        public string StrSearch { get; set; }

        /// <summary>
        /// Выборанные даны
        /// </summary>
        public string[] SelectDate { get; set; }

        /// <summary>
        /// Операнды даты
        /// </summary>
        public string[] OperandDate { get; set; }

        /// <summary>
        /// Выбранные значения времени
        /// </summary>
        public string[] SelectTime { get; set; }

        /// <summary>
        /// Операнды времени
        /// </summary>
        public string[] OperandTime { get; set; }

        /// <summary>
        /// Тип фильтра
        /// </summary>
        public ColumnFiltersType Type { get; set; }

        /// <summary>
        /// Логический тип фильтра. 
        /// </summary>
        public bool BooleanValue { get; set; }

        /// <summary>
        /// Список строк для поиска
        /// </summary>
        public StringFilterParametrsDto[] StringSearch { get; set; }

        /// <summary>
        /// Возвращает объект типа <see cref="FilterBase"/>
        /// </summary>
        public FilterBase ToFilterBase()
        {
            switch (Type)
            {
                case ColumnFiltersType.Select:
                    return new OptionsFilter
                    {
                        Name = ColumnName,
                        Selected = SelectList
                    };
                case ColumnFiltersType.String:
                case ColumnFiltersType.Tree:
                    if (StringSearch.Any() && StringSearch[0].Operand.Is("null"))
                    {
                        return new ExistsFilter
                        {
                            Name = ColumnName,
                            Exists = false
                        };
                    }

                    var filter = new StringFilter
                    {
                        Name = ColumnName,
                        Values = StringSearch.Select(item => new StringFilterValue { Operator = GetStringOperation(item.Operand), Value = item.StrSearch }).ToList()
                    };

                    if (!filter.Values.Any()) filter.Values.Add(new StringFilterValue { Operator = GetStringOperation(Operand), Value = StrSearch });

                    return filter;
                case ColumnFiltersType.Integer:
                    if (Operand.Is("null"))
                    {
                        return new ExistsFilter
                        {
                            Name = ColumnName,
                            Exists = false
                        };
                    }

                    return new NumberFilter { Name = ColumnName, Operator = GetOperation(Operand), Value = Convert.ToDouble(StrSearch) };
                case ColumnFiltersType.Date:
                    if (OperandDate.Any() && OperandDate[0].Is("null"))
                    {
                        return new ExistsFilter
                        {
                            Name = ColumnName,
                            Exists = false
                        };
                    }

                    return new DateFilter { Name = ColumnName, FromDate = Convert.ToDateTime(SelectDate[0]), ToDate = Convert.ToDateTime(SelectDate[1]) };
                case ColumnFiltersType.Time:
                    if (OperandTime.Any() && OperandTime[0].Is("null"))
                    {
                        return new ExistsFilter
                        {
                            Name = ColumnName,
                            Exists = false
                        };
                    }

                    return new TimeFilter { Name = ColumnName, FromTime = Convert.ToDateTime(SelectTime[0]), ToTime = Convert.ToDateTime(SelectTime[1]) };
                default:
                    return new StringFilter
                    {
                        Name = ColumnName,
                        Values = new List<StringFilterValue> { new StringFilterValue { Operator = GetStringOperation(Operand), Value = StrSearch } }
                    };
            }
        }

        /// <summary>
        /// Возвращает тип строкового операнда
        /// </summary>
        /// <param name="operand">Операнд</param>
        private StringOperationType GetStringOperation(string operand)
        {
            switch (operand)
            {
                case "~": return StringOperationType.LikeExactly;
                case "!~": return StringOperationType.NotLikeExactly;
                case "=": return StringOperationType.Eq;
                case "!=": return StringOperationType.NotEq;
                case "~*": return StringOperationType.LikeLeft;
                case "!~*": return StringOperationType.NotLikeLeft;
                case "*~": return StringOperationType.LikeRight;
                case "*!~": return StringOperationType.NotLikeRight;
                case "*~*": return StringOperationType.Like;
                case "!*~*": return StringOperationType.NotLike;
                default: return StringOperationType.Eq;
            }
        }

        /// <summary>
        /// Возвращает тип операнда
        /// </summary>
        /// <param name="operand">Операнд</param>
        private OperationType GetOperation(string operand)
        {
            switch (operand)
            {
                case "=": return OperationType.Eq;
                case ">": return OperationType.Greater;
                case ">=": return OperationType.GreaterOrEq;
                case "<": return OperationType.Lower;
                case "<=": return OperationType.LowerOrEq;
                case "!=": return OperationType.NotEq;
                default: return OperationType.Eq;
            }
        }
    }
}