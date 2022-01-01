using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MentolVKS.Common;
using MentolVKS.Common.TypeExtensions;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Enums;
using MentolVKS.Model.Validation;
using MentolVKS.Model.ViewModel;
using MentolVKS.Service.Contract;

namespace MentolVKS.Service
{
    public partial class Service : IService
    {
        #region Implementation of IService

        /// <inheritdoc/>
        public async Task<AspNetUser> GetUserByIdAsync(int id)
        {
            return await UnitOfWork.AspNetUserRepository.GetByIdAsync(id);
        }

        /// <inheritdoc/>
        public async Task<AspNetUser> GetUserByLoginAsync(string login)
        {
            return await UnitOfWork.AspNetUserRepository.GetByNameAsync(login);
        }

        /// <inheritdoc/>
        public async Task<List<AspNetRole>> GetUserRolesAsync(int userId)
        {
            return await UnitOfWork.AspNetRoleRepository.GetByUserIdAsync(userId);
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetUserRolesLicenseAsync(int userId)
        {
            var roles = await UnitOfWork.AspNetRoleRepository.GetByUserIdAsync(userId);
            var rolesTree = roles.GenerateTree(c => c.Id, c => c.ParentId);
            var license = (await UnitOfWork.LicenseXmlRepository.AllAsync()).FirstOrDefault().Products.Split(",").ToList();

            var realUserRole = rolesTree.ContainsTreeChildFull(c => c.Name, license);

            var roleIds = realUserRole.GetFlatKeys(c => c.Id);

            var removeRole = roles.Where(c => !roleIds.Contains(c.Id)).Select(c => new AspNetUserRole { UserId = userId, RoleId = c.Id });
            if (removeRole.Count() > 0)
            {
                await UnitOfWork.AspNetUserRoleRepository.DeleteRangeAsync(removeRole);
            }

            return realUserRole.GetFlatKeys(c => c.Name).ToList();
        }

        /// <inheritdoc/>
        public async Task<AspNetUser> CreateUserAsync(string login, string passwrod, string fullName, string email, string post, Provider Provider, bool auto = false)
        {
            var findUser = await GetUserByLoginAsync(login);

            if (findUser != null)
            {
                throw new ValidationErrors(new GeneralError(Localizer["User exist!"]));
            }

            var result = await UnitOfWork.AspNetUserRepository.AddAsync(new AspNetUser
            {
                UserName = login,
                Email = email,
                PasswordHash = passwrod,
                Provider = Provider.GetDisplayName(),
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = login.ToUpper(),
                UserFullName = fullName,
                Post = post
            });

            if (auto)
                await AddSuccessLog(ProductType.MMS, LogTypes.USERS, Localizer["add"], Localizer["Automatic addition. Added user {0} ({1})", result.UserName, result.UserFullName], null);
            else
                await AddSuccessLog(ProductType.MMS, LogTypes.USERS, Localizer["add"], Localizer["Added user \"{0}\" with Login \"{1}\"", result.UserFullName, result.UserName], result.Id);

            return result;
        }

        /// <inheritdoc/>
        public async Task<AspNetUser> UpdateUserAsync(AspNetUser item)
        {
            var old = await GetUserByIdAsync(item.Id);

            if (old == null)
            {
                throw new ValidationErrors(new GeneralError(Localizer["User not found!"]));
            }

            var findUser = await GetUserByLoginAsync(item.UserName);
            if (findUser != null)
            {
                if (findUser.Id != item.Id)
                {
                    throw new ValidationErrors(new GeneralError(Localizer["User exist!"]));
                }
            }

            var result = await UnitOfWork.AspNetUserRepository.SaveAsync(item);

            Type t = result.GetType();
            foreach (PropertyInfo info in t.GetProperties())
            {
                var oldValue = old.GetType().GetProperties().FirstOrDefault(c => c.Name == info.Name).GetValue(old);
                var newValue = info.GetValue(result);
                oldValue = oldValue != null ? oldValue.ToString() : string.Empty;
                newValue = newValue != null ? newValue.ToString() : string.Empty;

                if (!oldValue.Equals(newValue))
                {
                    if (info.Name == nameof(item.PasswordHash))
                    {
                        oldValue = "******";
                        newValue = "******";
                    }

                    await AddSuccessLog(ProductType.MMS, LogTypes.USERS, Localizer["edit"], Localizer["Edited user \"{0}\". Changed value for \"{1}\" from \"{2}\" to \"{3}\"", item.UserName, info.Name, oldValue, newValue], item.Id);
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task DeleteUserAsync(int id)
        {
            var check = await UnitOfWork.AspNetUserRepository.GetByIdAsync(id);

            if (check == null)
            {
                throw new ValidationErrors(new GeneralError(Localizer["User with id {0} not found!", id]));
            }

            await UnitOfWork.AspNetUserRepository.DeleteAsync(check);

            await AddSuccessLog(ProductType.MMS, LogTypes.USERS, Localizer["delete"], Localizer["Deleted user \"{0}\" : \"{1}\"", check.UserName, check.UserFullName], check.Id);
        }

        /// <inheritdoc/>
        public async Task ChangeUserRoles(int userId, List<int> roles)
        {
            var userRole = (await UnitOfWork.AspNetRoleRepository.GetByUserIdAsync(userId)).Select(c => c.Id);
            var fullRole = await UnitOfWork.AspNetRoleRepository.AllAsync();

            var rolesTree = fullRole.GenerateTree(c => c.Id, c => c.ParentId);

            var realUserRole = rolesTree.ContainsTree(c => c.Id, roles);
            var roleIds = realUserRole.GetFlatKeys(c => c.Id);

            var addRole = roleIds.Except(userRole).Select(c => new AspNetUserRole { RoleId = c, UserId = userId });
            var removeRole = userRole.Except(roleIds).Select(c => new AspNetUserRole { RoleId = c, UserId = userId });

            if (addRole.Any())
            {
                await UnitOfWork.AspNetUserRoleRepository.AddRangeAsync(addRole);
            }

            if (removeRole.Any())
            {
                await UnitOfWork.AspNetUserRoleRepository.DeleteRangeAsync(removeRole);
            }
        }

        /// <inheritdoc/>
        public async Task SetRls(int userId)
        {
            await UnitOfWork.AspNetUserRepository.SetUserRlsAsync(userId);
        }

        /// <inheritdoc/>
        public async Task SetLdapUserDefaultRlsAsync(int userId)
        {
            await UnitOfWork.AspNetUserRepository.SetLdapUserDefaultRlsAsync(userId);
        }

        /// <inheritdoc />
        public async Task<List<SelectDirectoryView>> AspNetUserGetSelectDirectoryAsync(string search, int limit)
        {
            return await UnitOfWork.AspNetUserRepository.GetSelectDirectoryAsync(search, limit);
        }

        /// <inheritdoc/>
        public async Task<IdentityModel> AuthUserAsync(string login, string password, Provider provider)
        {
            IdentityModel result = new IdentityModel();

            #region Проверка лицензии
            var license = await LicenseXmlCheckLicenseAsync("license.xml");
            switch (license)
            {
                case LicenseXmlStatus.NotFound:
                    throw new ValidationErrors(new GeneralError(Localizer["NotFound"]));
                case LicenseXmlStatus.Fatal:
                    throw new ValidationErrors(new GeneralError(Localizer["Fatal"]));
                case LicenseXmlStatus.NoValid:
                    throw new ValidationErrors(new GeneralError(Localizer["No Valid"]));
                case LicenseXmlStatus.Overdue:
                    throw new ValidationErrors(new GeneralError(Localizer["Overdue"]));
                case LicenseXmlStatus.NonCorrect:
                    throw new ValidationErrors(new GeneralError(Localizer["Non Correct"]));
            }
            #endregion

            if (provider == Provider.Ldap && !login.Contains("@"))
            {
                if(Ldap.LdapConfigCount()>1)
                    throw new ValidationErrors(new GeneralError(Localizer["Use @domain for username"]));

                login = login + "@" + Ldap.GetDefaulDomain();
            }
                

            var user = await GetUserByLoginAsync(login);

            #region Проверка на соответствие типа авторизации
            if (user != null)
            {
                // Если в БД провайдер NULL думаем проверяем по БД.
                user.Provider = user.Provider ?? "Integrated";

                if (provider == Provider.Ldap && user.Provider != "Ldap")
                {
                    throw new ValidationErrors(new GeneralError(Localizer["Non-domain account"]));
                }

                if (provider == Provider.Integrated && user.Provider != "Integrated")
                {
                    throw new ValidationErrors(new GeneralError(Localizer["Not a built-in account"]));
                }
            }
            #endregion

            switch (provider)
            {
                case Provider.Ldap:
                    var ldapUser = Ldap.Search(login).FirstOrDefault(c => c.Login.ToLower()==login.ToLower());

                    if (ldapUser == null)
                    {
                        throw new ValidationErrors(new GeneralError(Localizer["Wrong login or password"]));
                    }

                    try
                    {
                        Ldap.Login(login, password);
                    }
                    catch (Exception ex)
                    {
                        throw new ValidationErrors(new GeneralError(Localizer["Wrong login or password"]));
                    }

                    // Создаем Ldap пользователя и назначеням права.
                    if (user == null)
                    {
                        PasswordHasher hashPass = new PasswordHasher();
                        user = await CreateUserAsync(ldapUser.Login, hashPass.HashPassword(password), ldapUser.DisplayName, ldapUser.Email, string.Empty, Provider.Ldap, true);
                        await ChangeUserRoles(user.Id, new List<int> { 1, 10, 100, 102, 103 });
                        await SetLdapUserDefaultRlsAsync(user.Id);

                        var vksUser = await UnitOfWork.VksUserRepository.GetByJidAsync(ldapUser.Login);
                        
                        if(vksUser==null)
                            await UnitOfWork.VksUserRepository.AddAsync(new VksUser
                            {
                                Email = ldapUser.Email,
                                JID = ldapUser.Login,
                                Name=ldapUser.DisplayName
                            });
                    }

                    break;
                case Provider.Integrated:
                    if (user == null)
                        throw new ValidationErrors(new GeneralError(Localizer["Wrong login or password"]));

                    if (!UnitOfWork.AspNetUserRepository.CheckUserPassword(password, user.PasswordHash))
                        throw new ValidationErrors(new GeneralError(Localizer["Wrong login or password"]));
                    break;
            }

            var roles = await GetUserRolesLicenseAsync(user.Id);
            await SetRls(user.Id);

            result.Roles = roles;
            result.UserName = user.UserName;
            result.Id = user.Id;
            result.UserFullName = user.UserFullName;

            return result;
        }

        /// <inheritdoc/>
        public async Task<string> SaveRefreshTokenAsync(int userId, int idleMinute, string fingerPrint, string ip)
        {
            var result = await UnitOfWork.RefreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                CreateDt = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMinutes(idleMinute),
                FingerPrint = fingerPrint,
                AspNetUserId = userId,
                Ip = ip
            });

            return CryptoManager.AesEncrypt(result.Token + ";" + result.FingerPrint + ";" + result.Ip);
        }

        /// <inheritdoc/>
        public async Task<IdentityModel> GetRefreshTokenAsync(string token)
        {
            var result = new IdentityModel();
            var decToken = CryptoManager.AesDecrypt(token);

            if (string.IsNullOrEmpty(decToken))
                throw new ValidationErrors(new GeneralError("ошибка дешифрации токена"));

            var decSplit = decToken.Split(";");

            var refresToken = await UnitOfWork.RefreshTokenRepository.SearchRefreshTokenAsync(decSplit[0], decSplit[1], decSplit[2]);

            if (refresToken == null)
                throw new ValidationErrors(new GeneralError("ошибка дешифрации токена"));

            var user = await UnitOfWork.AspNetUserRepository.GetByIdAsync(refresToken.AspNetUserId);
            var roles = await GetUserRolesLicenseAsync(user.Id);

            result.Id = user.Id;
            result.UserName = user.UserName;
            result.Roles = roles;

            return result;
        }

        /// <inheritdoc/>
        public async Task DeleteRefreshTokenAsync(string token)
        {
            var result = new IdentityModel();
            var decToken = CryptoManager.AesDecrypt(token);

            if (string.IsNullOrEmpty(decToken))
                throw new ValidationErrors(new GeneralError("ошибка дешифрации токена"));

            var decSplit = decToken.Split(";");

            var refresToken = await UnitOfWork.RefreshTokenRepository.SearchRefreshTokenAsync(decSplit[0], decSplit[1], decSplit[2]);

            if (refresToken == null)
                throw new ValidationErrors(new GeneralError("ошибка дешифрации токена"));

            await UnitOfWork.RefreshTokenRepository.DeleteAsync(refresToken);
        }

        #endregion
    }
}