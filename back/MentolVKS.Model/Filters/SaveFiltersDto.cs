using MentolVKS.Model.Filters.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Filters
{
    /// <summary>
    /// Фильтр сохраняемый из комбика
    /// </summary>
    public class SaveFiltersDto
    {
        /// <summary>
        /// Dto колоночных фильтров из TableGridGeneral
        /// </summary>
        public FilterQuery ListQuery { get; set; }
        /// <summary>
        /// Имя выплнеямой функции
        /// </summary>
        public string Func { get; set; }
        /// <summary>
        /// Новое имя сохраняемого фильтра
        /// </summary>
        public string NewName { get; set; }
        /// <summary>
        /// Тип фильтра
        /// </summary>
        public int Iscommon { get; set; }
        /// <summary>
        /// Идентификатор фильтра
        /// </summary>
        public int FilterId { get; set; }
    }
}
