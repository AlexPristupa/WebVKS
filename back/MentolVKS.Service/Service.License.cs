using MentolVKS.Data.Interfaces;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Enums;
using MentolVKS.Service.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        /// <inheritdoc />
        public async Task<LicenseXml> LicenseXmlGetFromFileAsync(string filePath)
        {
            string fileContent;
            if (!File.Exists(filePath))
            {
                return null;
            }

            using (var reader = File.OpenText(filePath))
            {
                fileContent = await reader.ReadToEndAsync();
            }

            return fileContent.Length == 0 ? null : LicenseXmlParse(fileContent);
        }

        /// <inheritdoc />
        public async Task<LicenseXml> LicenseXmlGetFromBaseAsync()
        {
            var result = (await UnitOfWork.LicenseXmlRepository.AllAsync()).ToList();

            return !result.Any() ? null : result.First();
        }

        /// <inheritdoc />
        public async Task<LicenseXmlStatus> LicenseXmlCheckLicenseAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return LicenseXmlStatus.NotFound;
            }

            var lic = await LicenseXmlGetFromFileAsync(filePath);
            if (lic == null) return LicenseXmlStatus.Fatal;

            if (lic.Products == null || !lic.Products.Any()) return LicenseXmlStatus.NonCorrect;

            if (LicenseXmlComputeMd5Checksum(lic.FileAll))
            {
                if (lic.GetDateStart() < 0 || lic.GetDateEnd() < 0) return LicenseXmlStatus.Overdue;
            }
            else
            {
                return LicenseXmlStatus.NoValid;
            }

            /*Update*/
            await UnitOfWork.LicenseXmlRepository.UpdateLicenseXmlAsync(lic);

            return LicenseXmlStatus.Success;
        }

        /// <summary>
        /// Возвращает данные лицензии из файла
        /// </summary>
        /// <param name="fileContent">Содержимое файла лицензии</param>
        private static LicenseXml LicenseXmlParse(string fileContent)
        {
            try
            {
                var license = new LicenseXml();
                var licFile = XDocument.Parse(fileContent);

                license.SerialNumber = licFile.Elements().Descendants().FirstOrDefault(r => r.Name == "SerialNumber")?.Value;
                license.Hash = licFile.Elements().Descendants().FirstOrDefault(r => r.Name == "Hash")?.Value;
                license.DateStart = licFile.Elements().Descendants().FirstOrDefault(r => r.Name == "DateStart")?.Value;
                license.DateEnd = licFile.Elements().Descendants().FirstOrDefault(r => r.Name == "DateEnd")?.Value;

                var notProductPages = licFile.Elements().Descendants().FirstOrDefault(r => r.Name == "Products").Elements("Page").ToList();

                if (notProductPages.Any()) return null;

                var query = from f in licFile.Elements("License").Descendants()
                            where f.Name.LocalName == "Product"
                            select f;

                license.FileAll = fileContent;

                if (query.Any(item => item.Attribute("value") == null))
                {
                    return license;
                }

                license.ProductList = query.Select(item => new LicenseProduct
                {
                    Value = item.Attribute("value").Value,
                    Pages = item.Elements("Page").Select(pageItem => new LicensePage { Value = pageItem.Attribute("value").Value }).ToList()
                }).ToList();

                return license;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Проверяет хэш лицензии
        /// </summary>
        /// <param name="licString">Содержимое файла лицензии</param>
        /// <returns>Результат проверки</returns>
        private static bool LicenseXmlComputeMd5Checksum(string licString)
        {
            var license = XDocument.Parse(licString);
            var query = from f in license.Elements("License").Descendants()
                        where f.Name.LocalName != "Hash" && f.Name.LocalName != "Products"
                        select f;

            var regex = new Regex(@"([^\r])\n");

            var text = string.Empty;
            foreach (var t in query)
            {
                // для linux
                text += regex.Replace(t.ToString(), "$1\r\n");
            }

            string newHash;
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(text));
                var strResult = BitConverter.ToString(result);
                newHash = strResult.Replace("-", string.Empty);
            }

            query = from f in license.Elements("License").Descendants()
                    where f.Name.LocalName == "Hash"
                    select f;
            var lastHash = query.First().Value;

            return string.CompareOrdinal(newHash, lastHash) == 0;
        }
    }
}