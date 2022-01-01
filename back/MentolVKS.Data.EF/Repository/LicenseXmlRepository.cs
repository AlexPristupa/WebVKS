using LogicCore.Extensions;
using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    public class LicenseXmlRepository : TableBasedEntityRepositoryBase<LicenseXml>, ILicenseXmlRepository
    {
        public LicenseXmlRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        /// <inheritdoc/>
        public async Task<LicenseXml> UpdateLicenseXmlAsync(LicenseXml license)
        {
            var result = (await GetAsync()).ToList();
            LicenseXml lic;

            if (!result.Any())
            {
                lic = await AddAsync(license);
            }
            else
            {
                lic = result.First();

                if (lic.Products.Is(license.Products))
                {
                    lic.DateEnd = license.DateEnd;
                    lic.DateStart = license.DateStart;
                    lic.FileAll = license.FileAll;
                    lic.Hash = license.Hash;
                    lic.SerialNumber = license.SerialNumber;

                    await SaveAsync(lic);
                }
                else
                {
                    await DeleteAsync(lic);

                    lic = await AddAsync(license);
                }
            }

            return lic;
        }
    }
}
