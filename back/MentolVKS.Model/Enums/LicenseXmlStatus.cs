using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model.Enums
{
    /// <summary>
    ///     Перечисление статусов обработки файла лицензии
    /// </summary>
    public enum LicenseXmlStatus
    {
        /// <summary>
        ///     Успешно
        /// </summary>
        [Display(Name = "Успешно")]
        Success,

        /// <summary>
        ///     Ошибка чтения файла лицензии или разбора
        /// </summary>
        [Display(Name = "Отсутствует файл лицензии")]
        NotFound,

        /// <summary>
        ///     Ошибка чтения файла лицензии или разбора
        /// </summary>
        [Display(Name = "Ошибка чтения файла лицензии или разбора")]
        Fatal,

        /// <summary>
        ///     Не верный хэш
        /// </summary>
        [Display(Name = "Неверная хэш сумма файла лицензии")]
        NoValid,

        /// <summary>
        ///     Просроченная лицензия
        /// </summary>
        [Display(Name = "Просроченная лицензия")]
        Overdue,

        /// <summary>
        /// Некорректный файл лицензии
        /// </summary>
        [Display(Name = "Некорректный файл лицензии")]
        NonCorrect
    }
}
