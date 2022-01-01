using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Filters
{
    /// <summary>
    /// Dto - выбранных structureId с операндами для отбора должности
    /// </summary>
    public class ValuesFieldDto
    {
        /// <summary>
        /// Операнд выбранной стуктуры
        /// </summary>
        public string Operand { get; set; }

        /// <summary>
        /// Идентификатор выбранной структуры
        /// </summary>
        public string CompareValue { get; set; }

        /// <summary>
        /// Определить видимость
        /// </summary>
        public bool Visible { get; set; }
    }
}
