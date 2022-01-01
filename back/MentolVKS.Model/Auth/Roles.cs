using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Model.Auth
{
    /// <summary>
    /// Данные берутся из таблицы [dbo].[AspNetRoles]
    /// </summary>
    public enum Role
    {
        MMS = 1,
        MMS_BOOKING = 10,
        MMS_SETTINGS = 11,
        MMS_CMS = 12,
        MMS_REPORTS = 13,
        MMS_USERS = 14,
        MMS_BOOKING_BOOKING = 100,
        MMS_BOOKING_ROOMS = 101,
        MMS_BOOKING_RECORDS = 102,
        MMS_SETTINGS_USERPROFILES = 200,
        MMS_SETTINGS_GROUPSOFROOMS = 201,
        MMS_SETTINGS_RECORDSTORES = 202,
        MMS_SETTINGS_EXCHANGE = 203,
        MMS_CMS_SERVERS = 300,
        MMS_CMS_GROUPS = 301,
        MMS_REPORTS_REPORTS = 400,
        MMS_REPORTS_DISTRIBUTIONOFREPORTS = 401,
        MMS_USERS_USERS = 500,
        MMS_USERS_ROLES = 501,
        MMS_USERS_LOGS = 502
    }
}
