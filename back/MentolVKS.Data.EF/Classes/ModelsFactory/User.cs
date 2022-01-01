using MentolVKS.Common.TypeExtensions;
using MentolVKS.Data.Bases;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Filters.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Data.EF.Classes.ModelsFactory
{
    public partial class ModelFactory
    {
        /// <summary>
        /// Получение пользователей
        /// </summary>
        protected void GetUser()
        {
            TableNameToModels.Add(new TableNameToModel(Tables.User, new AspNetUser(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
                ));

            TableNameToModels.Add(new TableNameToModel(Tables.Booking, new BookingView(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));

            TableNameToModels.Add(new TableNameToModel(Tables.BookingDialog, new BookingView(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));

            TableNameToModels.Add(new TableNameToModel(Tables.Spaces, new SpacesView(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));

            TableNameToModels.Add(new TableNameToModel(Tables.Recordings, new RecordingsView(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));

            TableNameToModels.Add(new TableNameToModel(Tables.VksServers, new VksServersView(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));

            TableNameToModels.Add(new TableNameToModel(Tables.ServersGroup, new ServersGroupView(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));

            TableNameToModels.Add(new TableNameToModel(Tables.VksUserProfiles, new VksUserProfilesView(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));

            TableNameToModels.Add(new TableNameToModel(Tables.SpaceGroups, new SpaceGroups(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));

            TableNameToModels.Add(new TableNameToModel(Tables.AspNetRoles, new AspNetRole(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));

            TableNameToModels.Add(new TableNameToModel(Tables.AspNetUsers, new AspNetUser(),
                 new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));

            TableNameToModels.Add(new TableNameToModel(Tables.RecordingVksUsersDialog, new RecordingVksUsersView(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));

            TableNameToModels.Add(new TableNameToModel(Tables.SpaceUserRightsDialog, new SpaceUserRightsView(),
                new List<string> { ExcludeField.Actions.GetDisplayName() }
            ));
        }
    }
}
