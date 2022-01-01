using MentolVKS.Model;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Service.Contract
{
    public partial interface IService
    {
        /// <summary>
        /// Возрващает данные лицензии из файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        Task<LicenseXml> LicenseXmlGetFromFileAsync(string filePath);

        /// <summary>
        /// Возвращает данные лицензии из базы
        /// </summary>
        Task<LicenseXml> LicenseXmlGetFromBaseAsync();

        /// <summary>
        /// Проверяет лицензию
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns>Статус лицензии</returns>
        Task<LicenseXmlStatus> LicenseXmlCheckLicenseAsync(string filePath);
    }
}
