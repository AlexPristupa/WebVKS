using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.Bases
{
    /// <summary>
    /// Константы с именами таблиц БД
    /// </summary>
    public static class Tables
    {
        public static string User { get; } = "Vks_User";
        public static string Booking { get; } = "Vks_Booking";
        public static string BookingDialog { get; } = "Vks_BookingDialog";
        public static string Spaces { get; } = "Vks_Spaces";
        public static string Recordings { get; } = "Vks_Recordings";
        public static string RecordingVksUsersDialog { get; } = "Vks_RecordingVksUsersDialog";
        public static string VksServers { get; } = "Vks_VksServers";
        public static string ServersGroup { get; } = "Vks_ServersGroup";
        public static string VksUserProfiles { get; } = "Vks_VksUserProfiles";
        public static string SpaceGroups { get; } = "Vks_SpaceGroups";
        public static string AspNetUsers { get; } = "Vks_AspNetUsers";
        public static string AspNetRoles { get; } = "Vks_AspNetRoles";
        public static string SpaceUserRightsDialog { get; } = "Vks_SpaceUserRightsDialog";
    }
}
