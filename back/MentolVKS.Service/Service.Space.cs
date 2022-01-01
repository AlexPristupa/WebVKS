using MentolVKS.Common;
using MentolVKS.Data.Interfaces;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Validation;
using MentolVKS.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        /// <inheritdoc />
        public async Task<List<SelectDirectoryView>> GetSpaceSelectDirectoryAsync(string search, int limit)
        {
            return await UnitOfWork.SpaceRepository.GetSelectDirectoryAsync(search, limit);
        }

        /// <inheritdoc />
        public async Task<Space> GetSpaceByIdAsync(int id)
        {
            var result = await UnitOfWork.SpaceRepository.GetByIdAsync(id);
            if (result != null)
            {
                result.LinkSpaceToParticipants = (await UnitOfWork.LinkSpaceToParticipantRepository.GetBySpaceIdAsync(result.Id)).ToList();               
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<Space> AddSpaceAsync(Space item)
        {
            var checkUri = await UnitOfWork.SpaceRepository.GetByUriAsync(item.Uri);
            var checkUriAlt = await UnitOfWork.SpaceRepository.GetByUriAltAsync(item.UriAlt);
            var checkCallId= await UnitOfWork.SpaceRepository.GetByCallIdAsync(item.CallId);

            if (!string.IsNullOrEmpty(item.Guid))
            {
                var checkGuid = await UnitOfWork.SpaceRepository.GetByGuidAndGroupId(item.Guid, item.ServersGroupsId);

                if (checkGuid.Any())
                {
                    throw new ValidationErrors(new PropertyError(nameof(item.Guid), Localizer["The space with the Guid {0} already exists!", item.Uri]));
                }
            }

            if (checkUri != null)
            {
                throw new ValidationErrors(new PropertyError(nameof(item.Uri), Localizer["The space with the URI {0} already exists!", item.Uri]));
            }

            if (checkUriAlt != null)
            {
               throw new ValidationErrors(new PropertyError(nameof(item.UriAlt), Localizer["The space with the URIalt {0} already exists!", item.UriAlt]));
            }

            if (checkCallId != null)
            {
               throw new ValidationErrors(new PropertyError(nameof(item.CallId), Localizer["The space with the CallId {0} already exists!", item.CallId]));
            }

            var result = await UnitOfWork.SpaceRepository.AddAsync(item);

            await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.ROOMS, Localizer["add"], Localizer["Added room \"{0}\" with URI \"{1}\" on CMS server group \"{2}\"", item.Name, item.Uri, await GetServerGroupName(item)], item.Id);

            return result;
        }

        /// <inheritdoc />
        public async Task<Space> UpdateSpaceAsync(Space item)
        {
            var old = await UnitOfWork.SpaceRepository.GetByIdAsync(item.Id);
            var checkUri = await UnitOfWork.SpaceRepository.GetByUriAsync(item.Uri);
            var checkUriAlt = await UnitOfWork.SpaceRepository.GetByUriAltAsync(item.UriAlt);
            var checkCallId = await UnitOfWork.SpaceRepository.GetByCallIdAsync(item.CallId);

            if (!string.IsNullOrEmpty(item.Guid))
            {
                var checkGuid = await UnitOfWork.SpaceRepository.GetByGuidAndGroupId(item.Guid, item.ServersGroupsId);

                if (checkGuid.Any())
                {
                    if (checkGuid.FirstOrDefault(c => c.Id == item.Id) == null)
                        throw new ValidationErrors(new PropertyError(nameof(item.Guid), Localizer["The space with the Guid {0} already exists!", item.Uri]));
                }
            }

            if (checkUri != null)
            {
                if(checkUri.Id!=item.Id)
                    throw new ValidationErrors(new PropertyError(nameof(item.Uri), Localizer["The space with the URI {0} already exists!", item.Uri]));
            }
            
            if (checkUriAlt != null)
            {
                if (checkUriAlt.Id != item.Id)
                    throw new ValidationErrors(new PropertyError(nameof(item.UriAlt), Localizer["The space with the URIalt {0} already exists!", item.UriAlt]));
            }
            
            if (checkCallId != null)
            {
                if (checkCallId.Id != item.Id)
                    throw new ValidationErrors(new PropertyError(nameof(item.CallId), Localizer["The space with the CallId {0} already exists!", item.CallId]));
            }

            var link = item.LinkSpaceToParticipants;
            item.LinkSpaceToParticipants = null;
            var result = await UnitOfWork.SpaceRepository.SaveAsync(item);

            String LogText = "";
            Type t = result.GetType();
            foreach (PropertyInfo info in t.GetProperties())
            {

                var oldValue = old.GetType().GetProperties().FirstOrDefault(c => c.Name == info.Name).GetValue(old);
                var newValue = info.GetValue(result);
                oldValue = oldValue != null ? oldValue.ToString() : "";
                newValue = newValue != null ? newValue.ToString() : "";

                if(!oldValue.Equals(newValue))
                {
                    if (info.Name == nameof(item.Password))
                    {
                        oldValue = "******";
                        newValue = "******";
                    }

                    LogText += Localizer["Changed value for \"{0}\" from \"{1}\" to \"{2}\".", info.Name, oldValue, newValue].ToString();
                    //await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.ROOMS, Localizer["edit"], Localizer["Edited room \"{0}\" with URI \"{1}\" on CMS server group \"{2}\". Changed value for \"{3}\" from \"{4}\" to \"{5}\"", item.Name, item.Uri, await GetServerGroupName(item),info.Name, oldValue, newValue], item.Id);
                }
            }

            if (!String.IsNullOrEmpty(LogText))
            {
                LogText = Localizer["Edited room \"{0}\" with URI \"{1}\" on CMS server group \"{2}\".", item.Name, item.Uri, await GetServerGroupName(item)] + LogText;
                await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.ROOMS, Localizer["edit"], LogText, item.Id);
            }

            var oldLink = await UnitOfWork.LinkSpaceToParticipantRepository.GetBySpaceIdAsync(item.Id);
            await UnitOfWork.LinkSpaceToParticipantRepository.DeleteRangeAsync(oldLink);
            await UnitOfWork.LinkSpaceToParticipantRepository.AddRangeAsync(link);
            result.LinkSpaceToParticipants = link;

            return result;
        }

        /// <inheritdoc />
        public async Task DeleteSpaceAsync(int id)
        {
            var check = await UnitOfWork.SpaceRepository.GetByIdAsync(id);

            if (check == null)
            {
                throw new ValidationErrors(new GeneralError(Localizer["Room with id {0} not found!", id]));
            }

            await UnitOfWork.SpaceRepository.DeleteAsync(check);
           
            await AddSuccessLog(Model.Enums.ProductType.MMS, Model.Enums.LogTypes.ROOMS, Localizer["delete"], Localizer["Added room \"{0}\" with URI \"{1}\" on CMS server group \"{2}\"", check.Name, check.Uri, await GetServerGroupName(check)], check.Id);
        }

        private async Task<string> GetServerGroupName(Space item)
        {
            var serverGroupName = "";

            if (item.ServersGroupsId != null)
            {
                serverGroupName = (await UnitOfWork.ServersGroupsRepository.GetByIdAsync(item.ServersGroupsId)).Name;
            }

            return serverGroupName;
        }
    }
}