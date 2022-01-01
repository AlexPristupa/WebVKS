using LogicCore.Extensions;
using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Модель лицензии
    /// </summary>
    public class LicenseXml : TableBasedEntityBase
	{
        /// <summary>
        /// Список разрешённых продуктов
        /// </summary>
        public string Products
        {
            get
            {
                return string.Join(",", GetPages());
            }
            set
            {
                ProductList = value.Split(",").Select(p => new LicenseProduct { Value = p }).ToList();
            }
        }

        /// <summary>
        /// Серийный номер
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public string DateStart { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public string DateEnd { get; set; }

        /// <summary>
        /// Хэш
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Содержимое файла
        /// </summary>
        public string FileAll { get; set; }

        /// <summary>
        /// Список разрешённых продуктов
        /// </summary>
        public List<LicenseProduct> ProductList { get; set; } = new List<LicenseProduct>();

        /// <summary>
        /// Проверка продукта
        /// </summary>
        /// <param name="name">Имя продукта</param>
        /// <returns>Результат проверки</returns>
        public bool CheckProduct(string name)
        {
            return ProductList.Any(p => p.Value.Is(name));
        }

        /// <summary>
        /// Возвращает количество дней до окончания лицензии
        /// </summary>
        public int GetDateEnd()
        {
            try
            {
                var dateEnd = DateTime.ParseExact(DateEnd, "yyyyMMdd", CultureInfo.CurrentCulture);
                var bufDateEnd = (dateEnd - DateTime.Now).Days;
                if ((dateEnd - DateTime.Now).TotalDays > bufDateEnd)
                    bufDateEnd += 1;

                return bufDateEnd;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Возвращает количество дней с начала лицензии
        /// </summary>
        public int GetDateStart()
        {
            try
            {
                var dateStart = DateTime.ParseExact(DateStart, "yyyyMMdd", CultureInfo.CurrentCulture);
                return (DateTime.Now - dateStart).Days;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Возвращает список страниц
        /// </summary>
        public IEnumerable<string> GetPages()
        {
            var pages = ProductList.Select(p => p.Value).ToList();

            pages.AddRange(ProductList.SelectMany(p => p.Pages).Select(p => p.Value));

            return pages;
        }
    }

    /// <summary>
    /// Данные продукта
    /// </summary>
    public class LicenseProduct
    {
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Список страниц
        /// </summary>
        public List<LicensePage> Pages { get; set; } = new List<LicensePage>();
    }

    /// <summary>
    /// Данные страницы
    /// </summary>
    public class LicensePage
    {
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
    }
}
