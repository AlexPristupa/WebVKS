SET QUOTED_IDENTIFIER ON
GO
--не стираем то что выше, вносим изменения ниже
if exists (select 1
          from sysobjects
          where  id = object_id('dbo.SetDefaultRLS')
          and type in ('P','PC'))
   drop procedure dbo.SetDefaultRLS
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.periodicSetRLS')
          and type in ('P','PC'))
   drop procedure dbo.periodicSetRLS
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.SetRLS')
          and type in ('P','PC'))
   drop procedure dbo.SetRLS
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.ntfGetNotifyBookingChange')
          and type in ('P','PC'))
   drop procedure dbo.ntfGetNotifyBookingChange
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.ntfGetNotifyBookingChangePincodeNotification')
          and type in ('P','PC'))
   drop procedure dbo.ntfGetNotifyBookingChangePincodeNotification
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.ntfGetNotifyListForSend')
          and type in ('P','PC'))
   drop procedure dbo.ntfGetNotifyListForSend
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.ntfGetNotifyRecordingAddDelete')
          and type in ('P','PC'))
   drop procedure dbo.ntfGetNotifyRecordingAddDelete
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.ntfGetNotifyRecordingNotification')
          and type in ('P','PC'))
   drop procedure dbo.ntfGetNotifyRecordingNotification
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.ntfGetNotifyRecordingVksUsersChange')
          and type in ('P','PC'))
   drop procedure dbo.ntfGetNotifyRecordingVksUsersChange
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.ntfGetNotifyConferenceEnded')
          and type in ('P','PC'))
   drop procedure dbo.ntfGetNotifyConferenceEnded
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.AspNetTreePages') and o.name = 'FK_ASPNETTR_FK_ASPNET_ASPNETTR')
alter table dbo.AspNetTreePages
   drop constraint FK_ASPNETTR_FK_ASPNET_ASPNETTR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.AspNetTreePages') and o.name = 'FK_aspnettreepages_aspnetroles')
alter table dbo.AspNetTreePages
   drop constraint FK_aspnettreepages_aspnetroles
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.AspNetUserRoles') and o.name = 'FK_AspNetUserRoles_AspNetRoles_RoleId')
alter table dbo.AspNetUserRoles
   drop constraint FK_AspNetUserRoles_AspNetRoles_RoleId
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.AspNetUserRoles') and o.name = 'FK_AspNetUserRoles_AspNetUsers_UserId')
alter table dbo.AspNetUserRoles
   drop constraint FK_AspNetUserRoles_AspNetUsers_UserId
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Booking') and o.name = 'FK_BOOKING_FK_BOOKIN_SPACE')
alter table dbo.Booking
   drop constraint FK_BOOKING_FK_BOOKIN_SPACE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Booking') and o.name = 'FK_BOOKING_FK_BOOKIN_BOOKINGT')
alter table dbo.Booking
   drop constraint FK_BOOKING_FK_BOOKIN_BOOKINGT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Booking') and o.name = 'FK_BOOKING_FK_BOOKIN_CONNECTI')
alter table dbo.Booking
   drop constraint FK_BOOKING_FK_BOOKIN_CONNECTI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Booking') and o.name = 'FK_BOOKING_FK_BOOKIN_TIMEZONE')
alter table dbo.Booking
   drop constraint FK_BOOKING_FK_BOOKIN_TIMEZONE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Booking') and o.name = 'FK_Booking_Vksusers')
alter table dbo.Booking
   drop constraint FK_Booking_Vksusers
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Booking') and o.name = 'FK_pinpolitics_booking')
alter table dbo.Booking
   drop constraint FK_pinpolitics_booking
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.BookingStatus') and o.name = 'FK_BookingStatus_Booking')
alter table dbo.BookingStatus
   drop constraint FK_BookingStatus_Booking
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.BookingStatusPin') and o.name = 'FK_BookingStatusPin_Booking')
alter table dbo.BookingStatusPin
   drop constraint FK_BookingStatusPin_Booking
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.BookingTaskStatus') and o.name = 'FK_BookingTaskStatus_Booking')
alter table dbo.BookingTaskStatus
   drop constraint FK_BookingTaskStatus_Booking
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.LinkBookingToParticipant') and o.name = 'FK_LINKBOOK_FK_LINKBO_BOOKING')
alter table dbo.LinkBookingToParticipant
   drop constraint FK_LINKBOOK_FK_LINKBO_BOOKING
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.LinkBookingToParticipant') and o.name = 'FK_LINKBOOK_FK_LINKBO_VKSUSERS')
alter table dbo.LinkBookingToParticipant
   drop constraint FK_LINKBOOK_FK_LINKBO_VKSUSERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.LinkBookingTovksUsersOther') and o.name = 'FK_LINKBOOKINGTOVKSUSERSOTHER_BOOKING')
alter table dbo.LinkBookingTovksUsersOther
   drop constraint FK_LINKBOOKINGTOVKSUSERSOTHER_BOOKING
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.LinkBookingTovksUsersOther') and o.name = 'FK_LINKBOOKINGTOVKSUSERSOTHER_VKSUSERSOTHER')
alter table dbo.LinkBookingTovksUsersOther
   drop constraint FK_LINKBOOKINGTOVKSUSERSOTHER_VKSUSERSOTHER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.LinkSpaceToParticipant') and o.name = 'FK_LINKSPAC_FK_LINKSP_SPACE')
alter table dbo.LinkSpaceToParticipant
   drop constraint FK_LINKSPAC_FK_LINKSP_SPACE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.LinkSpaceToParticipant') and o.name = 'FK_LINKSPAC_FK_LINKSP_VKSUSERS')
alter table dbo.LinkSpaceToParticipant
   drop constraint FK_LINKSPAC_FK_LINKSP_VKSUSERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ServersGroups') and o.name = 'FK_SERVERSG_FK_SERVER_BALANCER')
alter table dbo.ServersGroups
   drop constraint FK_SERVERSG_FK_SERVER_BALANCER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ServersGroups') and o.name = 'FK_serversgroups_timezone')
alter table dbo.ServersGroups
   drop constraint FK_serversgroups_timezone
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Space') and o.name = 'FK_SPACE_FK_SPACE__VKSUSERS')
alter table dbo.Space
   drop constraint FK_SPACE_FK_SPACE__VKSUSERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Space') and o.name = 'FK_Space_Spacegroups')
alter table dbo.Space
   drop constraint FK_Space_Spacegroups
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.Space') and o.name = 'FK_space_serversgroups')
alter table dbo.Space
   drop constraint FK_space_serversgroups
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.UserRoles') and o.name = 'FK_UserRoles_Role')
alter table dbo.UserRoles
   drop constraint FK_UserRoles_Role
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.UserRoles') and o.name = 'FK_UserRoles_Users')
alter table dbo.UserRoles
   drop constraint FK_UserRoles_Users
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ntfEvents') and o.name = 'FK_ntfevents_services')
alter table dbo.ntfEvents
   drop constraint FK_ntfevents_services
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ntfNotifyLog') and o.name = 'FK_NTFNOTIFYLOG_NTFEVENTS')
alter table dbo.ntfNotifyLog
   drop constraint FK_NTFNOTIFYLOG_NTFEVENTS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ntfNotifyParam') and o.name = 'FK_ntfrnotifyparam_ntfnotifytemplate')
alter table dbo.ntfNotifyParam
   drop constraint FK_ntfrnotifyparam_ntfnotifytemplate
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ntfSubscription') and o.name = 'FK_ntfsubscription_ntfnotifytemplate')
alter table dbo.ntfSubscription
   drop constraint FK_ntfsubscription_ntfnotifytemplate
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ntfSubscriptionParamValue') and o.name = 'FK_ntfsubscriptionparamvalue_ntfnotifyparam')
alter table dbo.ntfSubscriptionParamValue
   drop constraint FK_ntfsubscriptionparamvalue_ntfnotifyparam
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.ntfSubscriptionParamValue') and o.name = 'FK_ntfsubscriptionparamvalue_ntfsubscription')
alter table dbo.ntfSubscriptionParamValue
   drop constraint FK_ntfsubscriptionparamvalue_ntfsubscription
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.perObjectToAction') and o.name = 'FK_PEROBJEC_REF_PERACTIO2')
alter table dbo.perObjectToAction
   drop constraint FK_PEROBJEC_REF_PERACTIO2
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.perObjectToAction') and o.name = 'FK_PEROBJEC_REF_PEROBJEC2')
alter table dbo.perObjectToAction
   drop constraint FK_PEROBJEC_REF_PEROBJEC2
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.perRoleActions') and o.name = 'FK_PERROLEA_REF_PEROBJEC3')
alter table dbo.perRoleActions
   drop constraint FK_PERROLEA_REF_PEROBJEC3
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.perRoleActions') and o.name = 'FK_ROLEACTION_REF_ROLEMAIN3')
alter table dbo.perRoleActions
   drop constraint FK_ROLEACTION_REF_ROLEMAIN3
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.perUserToRole') and o.name = 'FK_PERUSERT_REF_ASPNETUS')
alter table dbo.perUserToRole
   drop constraint FK_PERUSERT_REF_ASPNETUS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.perUserToRole') and o.name = 'FK_PERUSERT_REF_PERROLEM')
alter table dbo.perUserToRole
   drop constraint FK_PERUSERT_REF_PERROLEM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.recording') and o.name = 'FK_recording_booking')
alter table dbo.recording
   drop constraint FK_recording_booking
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.recordingvksusers') and o.name = 'FK_recordingvksuser_aspnetusers')
alter table dbo.recordingvksusers
   drop constraint FK_recordingvksuser_aspnetusers
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.recordingvksusers') and o.name = 'FK_recordingvksuser_recording')
alter table dbo.recordingvksusers
   drop constraint FK_recordingvksuser_recording
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.rlsLinkUserToObject') and o.name = 'FK_RLSLINKU_FK_RLSLIN_ASPNETUS')
alter table dbo.rlsLinkUserToObject
   drop constraint FK_RLSLINKU_FK_RLSLIN_ASPNETUS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.rlsLinkUserToObject') and o.name = 'FK_RLSLINKU_FK_RLSLIN_RLSSETTI')
alter table dbo.rlsLinkUserToObject
   drop constraint FK_RLSLINKU_FK_RLSLIN_RLSSETTI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.rlsPermissionToSpace') and o.name = 'FK_rlspermissiontospace_rlslinkusertoobject')
alter table dbo.rlsPermissionToSpace
   drop constraint FK_rlspermissiontospace_rlslinkusertoobject
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.rlsPermissionToSpace') and o.name = 'FK_rlspermissiontospace_space')
alter table dbo.rlsPermissionToSpace
   drop constraint FK_rlspermissiontospace_space
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.rlsSettingObjects') and o.name = 'FK_RLSSETTI_FK_RLSSET_RLSSETTI')
alter table dbo.rlsSettingObjects
   drop constraint FK_RLSSETTI_FK_RLSSET_RLSSETTI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksCallCurrent') and o.name = 'FK_vksCallCurrent_vksConferenceCurrent')
alter table dbo.vksCallCurrent
   drop constraint FK_vksCallCurrent_vksConferenceCurrent
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksCallHistory') and o.name = 'FK_vksCallHistory_vksConferenceHistory')
alter table dbo.vksCallHistory
   drop constraint FK_vksCallHistory_vksConferenceHistory
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksConferenceCurrent') and o.name = 'FK_vksConferenceCurrent_vksUsers')
alter table dbo.vksConferenceCurrent
   drop constraint FK_vksConferenceCurrent_vksUsers
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksConferenceHistory') and o.name = 'FK_vksConferenceHistory_vksUsers')
alter table dbo.vksConferenceHistory
   drop constraint FK_vksConferenceHistory_vksUsers
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksListNode') and o.name = 'FK_vksListNode_vksServers')
alter table dbo.vksListNode
   drop constraint FK_vksListNode_vksServers
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksServers') and o.name = 'FK_VKSSERVE_FK_VKSSER_SERVERSG')
alter table dbo.vksServers
   drop constraint FK_VKSSERVE_FK_VKSSER_SERVERSG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksServersCommands') and o.name = 'FK_vksServersCommands_Services')
alter table dbo.vksServersCommands
   drop constraint FK_vksServersCommands_Services
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksServersCommands') and o.name = 'FK_vksServersCommands_vksListNode')
alter table dbo.vksServersCommands
   drop constraint FK_vksServersCommands_vksListNode
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksServersCommands') and o.name = 'FK_vksServersCommands_vksServer')
alter table dbo.vksServersCommands
   drop constraint FK_vksServersCommands_vksServer
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksUsersConference') and o.name = 'FK_vksUsersConference_vksConferenceCurrent')
alter table dbo.vksUsersConference
   drop constraint FK_vksUsersConference_vksConferenceCurrent
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksUsersConference') and o.name = 'FK_vksUsersConference_vksUsers')
alter table dbo.vksUsersConference
   drop constraint FK_vksUsersConference_vksUsers
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksUsersProfiles') and o.name = 'FK_vksUsersProfiles_Serversgroups')
alter table dbo.vksUsersProfiles
   drop constraint FK_vksUsersProfiles_Serversgroups
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.vksVendorModel') and o.name = 'FK_vksVendorModel_vksVendor')
alter table dbo.vksVendorModel
   drop constraint FK_vksVendorModel_vksVendor
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.BookingView')
            and   type = 'V')
   drop view dbo.BookingView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.RecordingVksUsersView')
            and   type = 'V')
   drop view dbo.RecordingVksUsersView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.RecordingsView')
            and   type = 'V')
   drop view dbo.RecordingsView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ServersGroupView')
            and   type = 'V')
   drop view dbo.ServersGroupView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SpaceUserRightsView')
            and   type = 'V')
   drop view dbo.SpaceUserRightsView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.SpacesView')
            and   type = 'V')
   drop view dbo.SpacesView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.VksServersView')
            and   type = 'V')
   drop view dbo.VksServersView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.VksUserProfilesView')
            and   type = 'V')
   drop view dbo.VksUserProfilesView
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vAPIGetConferenceList')
            and   type = 'V')
   drop view dbo.vAPIGetConferenceList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vAPIGetConferenceListActive')
            and   type = 'V')
   drop view dbo.vAPIGetConferenceListActive
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vAPIGetConferenceListInactive')
            and   type = 'V')
   drop view dbo.vAPIGetConferenceListInactive
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vAPIGetConferenceUsersList')
            and   type = 'V')
   drop view dbo.vAPIGetConferenceUsersList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.APISMSSettings')
            and   type = 'U')
   drop table dbo.APISMSSettings
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.AspNetRoles')
            and   name  = 'RoleNameIndex'
            and   indid > 0
            and   indid < 255)
   drop index dbo.AspNetRoles.RoleNameIndex
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.AspNetRoles')
            and   type = 'U')
   drop table dbo.AspNetRoles
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.AspNetTreePages')
            and   type = 'U')
   drop table dbo.AspNetTreePages
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.AspNetUserRoles')
            and   name  = 'IX_AspNetUserRoles_UserId'
            and   indid > 0
            and   indid < 255)
   drop index dbo.AspNetUserRoles.IX_AspNetUserRoles_UserId
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.AspNetUserRoles')
            and   name  = 'IX_AspNetUserRoles_RoleId'
            and   indid > 0
            and   indid < 255)
   drop index dbo.AspNetUserRoles.IX_AspNetUserRoles_RoleId
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.AspNetUserRoles')
            and   type = 'U')
   drop table dbo.AspNetUserRoles
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.AspNetUsers')
            and   name  = 'UserNameIndex'
            and   indid > 0
            and   indid < 255)
   drop index dbo.AspNetUsers.UserNameIndex
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.AspNetUsers')
            and   name  = 'EmailIndex'
            and   indid > 0
            and   indid < 255)
   drop index dbo.AspNetUsers.EmailIndex
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.AspNetUsers')
            and   type = 'U')
   drop table dbo.AspNetUsers
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.BalancerList')
            and   type = 'U')
   drop table dbo.BalancerList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Booking')
            and   type = 'U')
   drop table dbo.Booking
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.BookingStatus')
            and   name  = 'IX_BookingStatus_CallId'
            and   indid > 0
            and   indid < 255)
   drop index dbo.BookingStatus.IX_BookingStatus_CallId
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.BookingStatus')
            and   type = 'U')
   drop table dbo.BookingStatus
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.BookingStatusPin')
            and   type = 'U')
   drop table dbo.BookingStatusPin
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.BookingTaskStatus')
            and   type = 'U')
   drop table dbo.BookingTaskStatus
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ConnectionType')
            and   type = 'U')
   drop table dbo.ConnectionType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.FilterColumnsList')
            and   type = 'U')
   drop table dbo.FilterColumnsList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.FilterForColumnTypeList')
            and   type = 'U')
   drop table dbo.FilterForColumnTypeList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.FilterOperationsList')
            and   type = 'U')
   drop table dbo.FilterOperationsList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.FilterTablesList')
            and   type = 'U')
   drop table dbo.FilterTablesList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.FilterValue')
            and   type = 'U')
   drop table dbo.FilterValue
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.FiltersList')
            and   type = 'U')
   drop table dbo.FiltersList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.FiltersToUserLink')
            and   type = 'U')
   drop table dbo.FiltersToUserLink
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.LinkBookingToParticipant')
            and   name  = 'ix_linkbt_bookingid_vksparticipantid'
            and   indid > 0
            and   indid < 255)
   drop index dbo.LinkBookingToParticipant.ix_linkbt_bookingid_vksparticipantid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LinkBookingToParticipant')
            and   type = 'U')
   drop table dbo.LinkBookingToParticipant
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LinkBookingTovksUsersOther')
            and   type = 'U')
   drop table dbo.LinkBookingTovksUsersOther
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.LinkSpaceToParticipant')
            and   name  = 'ix_linkst_spaceid_vksuserid'
            and   indid > 0
            and   indid < 255)
   drop index dbo.LinkSpaceToParticipant.ix_linkst_spaceid_vksuserid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LinkSpaceToParticipant')
            and   type = 'U')
   drop table dbo.LinkSpaceToParticipant
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Logs')
            and   type = 'U')
   drop table dbo.Logs
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.LogsType')
            and   type = 'U')
   drop table dbo.LogsType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.NotifyTransportType')
            and   type = 'U')
   drop table dbo.NotifyTransportType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.OutlookBookingDefault')
            and   type = 'U')
   drop table dbo.OutlookBookingDefault
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Products')
            and   type = 'U')
   drop table dbo.Products
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.RefreshLog')
            and   type = 'U')
   drop table dbo.RefreshLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.RefreshToken')
            and   type = 'U')
   drop table dbo.RefreshToken
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Role')
            and   type = 'U')
   drop table dbo.Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SMTPSettings')
            and   type = 'U')
   drop table SMTPSettings
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ServersGroups')
            and   type = 'U')
   drop table dbo.ServersGroups
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Services')
            and   type = 'U')
   drop table dbo.Services
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Space')
            and   type = 'U')
   drop table dbo.Space
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Spacegroups')
            and   type = 'U')
   drop table dbo.Spacegroups
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.TableColumnSettings')
            and   name  = 'is_tablecolumnsettings_tablename_order'
            and   indid > 0
            and   indid < 255)
   drop index dbo.TableColumnSettings.is_tablecolumnsettings_tablename_order
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.TableColumnSettings')
            and   type = 'U')
   drop table dbo.TableColumnSettings
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.UserRoles')
            and   type = 'U')
   drop table dbo.UserRoles
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.UserTableColumns')
            and   type = 'U')
   drop table dbo.UserTableColumns
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Users')
            and   type = 'U')
   drop table dbo.Users
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.bookingtype')
            and   type = 'U')
   drop table dbo.bookingtype
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.licensexml')
            and   type = 'U')
   drop table dbo.licensexml
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.mmsSettings')
            and   type = 'U')
   drop table dbo.mmsSettings
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.nfsServers')
            and   type = 'U')
   drop table dbo.nfsServers
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ntfEvents')
            and   type = 'U')
   drop table dbo.ntfEvents
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ntfNotifyLog')
            and   type = 'U')
   drop table dbo.ntfNotifyLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ntfNotifyParam')
            and   type = 'U')
   drop table dbo.ntfNotifyParam
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ntfNotifyTemplate')
            and   type = 'U')
   drop table dbo.ntfNotifyTemplate
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ntfSubscription')
            and   type = 'U')
   drop table dbo.ntfSubscription
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.ntfSubscriptionParamValue')
            and   type = 'U')
   drop table dbo.ntfSubscriptionParamValue
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.perActionList')
            and   type = 'U')
   drop table dbo.perActionList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.perObjectList')
            and   type = 'U')
   drop table dbo.perObjectList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.perObjectToAction')
            and   type = 'U')
   drop table dbo.perObjectToAction
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.perRoleActions')
            and   type = 'U')
   drop table dbo.perRoleActions
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.perRoleMainList')
            and   type = 'U')
   drop table dbo.perRoleMainList
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.perUserToRole')
            and   type = 'U')
   drop table dbo.perUserToRole
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.pinpolitics')
            and   type = 'U')
   drop table dbo.pinpolitics
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.recording')
            and   type = 'U')
   drop table dbo.recording
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.recordingvksusers')
            and   name  = 'ix_recordingvksusers_recordingid_userid'
            and   indid > 0
            and   indid < 255)
   drop index dbo.recordingvksusers.ix_recordingvksusers_recordingid_userid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.recordingvksusers')
            and   type = 'U')
   drop table dbo.recordingvksusers
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.rlsLinkUserToObject')
            and   name  = 'ix_rlsLinkUserToObject_SettingObjectId_userId'
            and   indid > 0
            and   indid < 255)
   drop index dbo.rlsLinkUserToObject.ix_rlsLinkUserToObject_SettingObjectId_userId
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.rlsLinkUserToObject')
            and   type = 'U')
   drop table dbo.rlsLinkUserToObject
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.rlsPermissionToSpace')
            and   type = 'U')
   drop table dbo.rlsPermissionToSpace
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.rlsSettingList')
            and   type = 'U')
   drop table dbo.rlsSettingList
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.rlsSettingObjects')
            and   name  = 'ix_rlsSettingObjects_SettingListId_PrivateName'
            and   indid > 0
            and   indid < 255)
   drop index dbo.rlsSettingObjects.ix_rlsSettingObjects_SettingListId_PrivateName
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.rlsSettingObjects')
            and   type = 'U')
   drop table dbo.rlsSettingObjects
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.rlsSpace')
            and   name  = 'ix_rlsspace_spaceId_userId'
            and   indid > 0
            and   indid < 255)
   drop index dbo.rlsSpace.ix_rlsspace_spaceId_userId
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.rlsSpace')
            and   type = 'U')
   drop table dbo.rlsSpace
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.timezone')
            and   name  = 'IX_standartid'
            and   indid > 0
            and   indid < 255)
   drop index dbo.timezone.IX_standartid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.timezone')
            and   type = 'U')
   drop table dbo.timezone
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksCallCurrent')
            and   type = 'U')
   drop table dbo.vksCallCurrent
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksCallHistory')
            and   type = 'U')
   drop table dbo.vksCallHistory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksCallInstances')
            and   type = 'U')
   drop table dbo.vksCallInstances
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksCallInstancesConfig')
            and   type = 'U')
   drop table dbo.vksCallInstancesConfig
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksCallInstancesStatus')
            and   type = 'U')
   drop table dbo.vksCallInstancesStatus
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksConferenceCurrent')
            and   type = 'U')
   drop table dbo.vksConferenceCurrent
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksConferenceHistory')
            and   type = 'U')
   drop table dbo.vksConferenceHistory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksLicensing')
            and   type = 'U')
   drop table dbo.vksLicensing
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksListNode')
            and   type = 'U')
   drop table dbo.vksListNode
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksParticipants')
            and   type = 'U')
   drop table dbo.vksParticipants
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.vksServers')
            and   name  = 'ix_vksservers_name'
            and   indid > 0
            and   indid < 255)
   drop index dbo.vksServers.ix_vksservers_name
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksServers')
            and   type = 'U')
   drop table dbo.vksServers
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.vksServersCommands')
            and   name  = 'ix_vksservcomm_id3'
            and   indid > 0
            and   indid < 255)
   drop index dbo.vksServersCommands.ix_vksservcomm_id3
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksServersCommands')
            and   type = 'U')
   drop table dbo.vksServersCommands
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.vksUsers')
            and   name  = 'IX_jid'
            and   indid > 0
            and   indid < 255)
   drop index dbo.vksUsers.IX_jid
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksUsers')
            and   type = 'U')
   drop table dbo.vksUsers
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksUsersConference')
            and   type = 'U')
   drop table dbo.vksUsersConference
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksUsersOther')
            and   type = 'U')
   drop table dbo.vksUsersOther
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksUsersProfiles')
            and   type = 'U')
   drop table dbo.vksUsersProfiles
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksVendor')
            and   type = 'U')
   drop table dbo.vksVendor
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.vksVendorModel')
            and   type = 'U')
   drop table dbo.vksVendorModel
go

/*==============================================================*/
/* Table: APISMSSettings                                        */
/*==============================================================*/
create table dbo.APISMSSettings (
   idr                  int                  not null,
   host                 varchar(250)         null,
   port                 int                  null,
   login                varchar(128)         null,
   pswd                 varchar(128)         null,
   constraint PK_APISMSSETTINGS primary key (idr)
)
go

/*==============================================================*/
/* Table: AspNetRoles                                           */
/*==============================================================*/
create table dbo.AspNetRoles (
   Id                   int                  identity,
   ConcurrencyStamp     nvarchar(4000)       null,
   Name                 nvarchar(256)        null,
   NormalizedName       nvarchar(256)        null,
   ParentId             int                  null,
   ViewName             nvarchar(256)        null,
   constraint PK_ASPNETROLES primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: RoleNameIndex                                         */
/*==============================================================*/
create index RoleNameIndex on dbo.AspNetRoles (
NormalizedName ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: AspNetTreePages                                       */
/*==============================================================*/
create table dbo.AspNetTreePages (
   idr                  int                  identity,
   ViewName             varchar(300)         null,
   ParentId             int                  null,
   RoleId               int                  null,
   constraint PK_ASPNETTREEPAGES primary key (idr)
)
go

/*==============================================================*/
/* Table: AspNetUserRoles                                       */
/*==============================================================*/
create table dbo.AspNetUserRoles (
   UserId               int                  not null,
   RoleId               int                  not null,
   constraint PK_ASPNETUSERROLES primary key (UserId, RoleId)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_AspNetUserRoles_RoleId                             */
/*==============================================================*/
create index IX_AspNetUserRoles_RoleId on dbo.AspNetUserRoles (
RoleId ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IX_AspNetUserRoles_UserId                             */
/*==============================================================*/
create index IX_AspNetUserRoles_UserId on dbo.AspNetUserRoles (
UserId ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: AspNetUsers                                           */
/*==============================================================*/
create table dbo.AspNetUsers (
   Id                   int                  identity,
   AccessFailedCount    int                  not null,
   ConcurrencyStamp     nvarchar(4000)       null,
   Email                nvarchar(256)        null,
   EmailConfirmed       bit                  not null,
   LockoutEnabled       bit                  not null,
   LockoutEnd           datetimeoffset       null,
   NormalizedEmail      nvarchar(256)        null,
   NormalizedUserName   nvarchar(256)        null,
   PasswordHash         nvarchar(4000)       null,
   PhoneNumber          nvarchar(4000)       null,
   PhoneNumberConfirmed bit                  not null,
   SecurityStamp        nvarchar(4000)       null,
   TwoFactorEnabled     bit                  not null,
   UserName             nvarchar(256)        null,
   Post                 nvarchar(256)        null,
   UserFullName         nvarchar(100)        null,
   SID                  nvarchar(256)        null,
   Provider             nvarchar(50)         null,
   APIKey               varchar(255)         null,
   NeedRls              smallint             null,
   constraint PK_AspNetUsers primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: EmailIndex                                            */
/*==============================================================*/
create index EmailIndex on dbo.AspNetUsers (
NormalizedEmail ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: UserNameIndex                                         */
/*==============================================================*/
create unique index UserNameIndex on dbo.AspNetUsers (
NormalizedUserName ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: BalancerList                                          */
/*==============================================================*/
create table dbo.BalancerList (
   id                   int                  not null,
   privatename          varchar(128)         null,
   constraint PK_BALANCERLIST primary key (id)
)
go

/*==============================================================*/
/* Table: Booking                                               */
/*==============================================================*/
create table dbo.Booking (
   id                   int                  identity,
   name                 varchar(256)         null,
   description          varchar(256)         null,
   location             varchar(256)         null,
   ownerid              int                  null,
   datestart            datetime             null,
   timezone             int                  null,
   duration             int                  null,
   isusepin             bit                  null,
   schedule             varchar(128)         null,
   spaceid              int                  not null,
   connectiontypeid     int                  null,
   attemptscount        int                  null,
   delay                int                  null,
   issendnotification   bit                  null,
   issynctoexchange     bit                  null,
   openconferencebefore int                  null,
   isneverusepin        bit                  null,
   dateend              datetime             null,
   repeatcount          int                  null,
   pinpoliticsid        int                  null,
   pinschedule          varchar(128)         null,
   pindatestart         datetime             null,
   pincode              varchar(128)         null,
   typeid               int                  null,
   scheduletab          varchar(4)           null,
   pinscheduletab       varchar(4)           null,
   Ics                  text                 null,
   Uid                  varchar(256)         null,
   newpincode           varchar(128)         null,
   constraint PK_BOOKING primary key (id)
)
go

/*==============================================================*/
/* Table: BookingStatus                                         */
/*==============================================================*/
create table dbo.BookingStatus (
   BookingId            int                  not null,
   StatusId             tinyint              not null,
   StatusDate           datetime2            not null,
   Message              varchar(2000)        null,
   Params               varchar(2000)        null,
   ActiveStatus         varchar(16)          null,
   NextRun              datetime             null,
   Error                bit                  null,
   ProlongationCount    int                  null,
   LastStart            datetime             null,
   RepeatCount          int                  null,
   CallId               varchar(50)          null,
   DateEnd              datetime             null,
   constraint PK_BOOKINGSTATUS primary key (BookingId)
)
go

/*==============================================================*/
/* Index: IX_BookingStatus_CallId                               */
/*==============================================================*/
create index IX_BookingStatus_CallId on dbo.BookingStatus (
CallId ASC
)
go

/*==============================================================*/
/* Table: BookingStatusPin                                      */
/*==============================================================*/
create table dbo.BookingStatusPin (
   BookingId            int                  not null,
   NextChangePinCode    datetime             null,
   NewPinCode           varchar(128)         null,
   LastStart            datetime             null,
   constraint PK_BOOKINGSTATUSPIN primary key (BookingId)
)
go

/*==============================================================*/
/* Table: BookingTaskStatus                                     */
/*==============================================================*/
create table dbo.BookingTaskStatus (
   BookingId            int                  not null,
   StatusDate           datetime2            not null,
   LastStart            datetime2            null,
   NextProcessing       datetime2            null,
   constraint PK_BOOKINGTASKSTATUS primary key (BookingId)
)
go

/*==============================================================*/
/* Table: ConnectionType                                        */
/*==============================================================*/
create table dbo.ConnectionType (
   id                   int                  not null,
   privatename          varchar(32)          not null,
   name                 varchar(64)          not null,
   constraint PK_CONNECTIONTYPE primary key (id)
)
go

/*==============================================================*/
/* Table: FilterColumnsList                                     */
/*==============================================================*/
create table dbo.FilterColumnsList (
   idr                  int                  identity,
   TableId              int                  null,
   ColumnName           varchar(256)         null,
   FilterTypeId         int                  null,
   DataQuery            varchar(Max)         null,
   ConditionColumn      varchar(1024)        null,
   DisplayMember        varchar(1024)        null,
   ValueMember          varchar(1024)        null,
   IsTableColumn        bit                  not null default 1,
   FilterSql            varchar(4000)        null,
   WhereColumn          varchar(100)         null,
   Title                varchar(100)         null,
   constraint PK_FILTERCOLUMNSLIST primary key (idr)
)
go

/*==============================================================*/
/* Table: FilterForColumnTypeList                               */
/*==============================================================*/
create table dbo.FilterForColumnTypeList (
   idr                  int                  identity,
   TypeId               int                  not null,
   TypeName             varchar(256)         null,
   DataQuery            varchar(max)         null,
   constraint PK_FILTERFORCOLUMNTYPELIST primary key (TypeId)
)
go

/*==============================================================*/
/* Table: FilterOperationsList                                  */
/*==============================================================*/
create table dbo.FilterOperationsList (
   idr                  int                  identity,
   OperationName        varchar(1024)        null,
   Operand              varchar(1024)        null,
   ColumnTypeFilt       int                  null,
   constraint PK_FILTEROPERATIONSLIST primary key (idr)
)
go

/*==============================================================*/
/* Table: FilterTablesList                                      */
/*==============================================================*/
create table dbo.FilterTablesList (
   idr                  int                  identity,
   TableName            varchar(256)         null,
   DBTable              varchar(256)         null,
   AccessCondition      varchar(1024)        null,
   constraint PK_FILTERTABLESLIST primary key (idr)
)
go

/*==============================================================*/
/* Table: FilterValue                                           */
/*==============================================================*/
create table dbo.FilterValue (
   idr                  int                  identity,
   FilterId             int                  null,
   ColumnId             int                  null,
   OperationId          int                  null,
   FValue               varchar(1024)        null,
   constraint PK_FILTERVALUE primary key (idr)
)
go

/*==============================================================*/
/* Table: FiltersList                                           */
/*==============================================================*/
create table dbo.FiltersList (
   idr                  int                  identity,
   FilterName           varchar(256)         null,
   IsCommon             int                  null,
   constraint PK_FILTERSLIST primary key (idr)
)
go

/*==============================================================*/
/* Table: FiltersToUserLink                                     */
/*==============================================================*/
create table dbo.FiltersToUserLink (
   idr                  int                  identity,
   FilterId             int                  null,
   UserId               int                  null,
   IsActive             int                  null,
   constraint PK_FILTERSTOUSERLINK primary key (idr)
)
go

/*==============================================================*/
/* Table: LinkBookingToParticipant                              */
/*==============================================================*/
create table dbo.LinkBookingToParticipant (
   id                   int                  identity,
   bookingid            int                  null,
   vksparticipantid     int                  null,
   uri                  varchar(64)          null,
   callLegProfileGuid   varchar(36)          null,
   constraint PK_LINKBOOKINGTOPARTICIPANT primary key (id)
)
go

/*==============================================================*/
/* Index: ix_linkbt_bookingid_vksparticipantid                  */
/*==============================================================*/
create unique index ix_linkbt_bookingid_vksparticipantid on dbo.LinkBookingToParticipant (
bookingid ASC,
vksparticipantid ASC
)
go

/*==============================================================*/
/* Table: LinkBookingTovksUsersOther                            */
/*==============================================================*/
create table dbo.LinkBookingTovksUsersOther (
   idr                  int                  identity,
   bookingid            int                  null,
   vksusersotherid      int                  null,
   constraint PK_LINKBOOKINGTOVKSUSERSOTHER primary key (idr)
)
go

/*==============================================================*/
/* Table: LinkSpaceToParticipant                                */
/*==============================================================*/
create table dbo.LinkSpaceToParticipant (
   id                   int                  identity,
   spaceid              int                  null,
   vksuserid            int                  null,
   calllegprofileguid   varchar(36)          null,
   candestroy           bit                  null,
   canaddremovemember   bit                  null,
   canchangename        bit                  null,
   canchangenonmemberaccessallowed bit                  null,
   canchangeuri         bit                  null,
   canchangecallid      bit                  null,
   canchangepasscode    bit                  null,
   canremoveself        bit                  null,
   constraint PK_LINKSPACETOPARTICIPANT primary key (id)
)
go

/*==============================================================*/
/* Index: ix_linkst_spaceid_vksuserid                           */
/*==============================================================*/
create unique index ix_linkst_spaceid_vksuserid on dbo.LinkSpaceToParticipant (
spaceid ASC,
vksuserid ASC
)
go

/*==============================================================*/
/* Table: Logs                                                  */
/*==============================================================*/
create table dbo.Logs (
   idr                  int                  identity,
   ProductId            int                  null,
   TypeId               int                  not null,
   LevelId              tinyint              null,
   UserName             varchar(250)         null,
   DateRecord           datetime             null,
   Action               varchar(500)         null,
   Description          varchar(2000)        null,
   IP                   varchar(200)         null,
   ObjectId             int                  null,
   PropertyId           smallint             null,
   constraint PK_LOGS primary key (idr)
)
go

/*==============================================================*/
/* Table: LogsType                                              */
/*==============================================================*/
create table dbo.LogsType (
   idr                  int                  identity,
   name                 varchar(150)         null,
   PrivateName          varchar(128)         null,
   constraint PK_LOGSTYPE primary key (idr)
)
go

/*==============================================================*/
/* Table: NotifyTransportType                                   */
/*==============================================================*/
create table dbo.NotifyTransportType (
   Id                   int                  identity,
   Name                 varchar(128)         null,
   PrivateName          varchar(32)          null,
   constraint PK_NOTIFYTRANSPORTTYPE primary key (Id)
)
go

/*==============================================================*/
/* Table: OutlookBookingDefault                                 */
/*==============================================================*/
create table dbo.OutlookBookingDefault (
   id                   int                  not null,
   name                 varchar(256)         null,
   description          varchar(256)         null,
   location             varchar(256)         null,
   ownerid              varchar(256)         null,
   datestart            varchar(256)         null,
   timezone             varchar(256)         null,
   duration             int                  null,
   isusepin             bit                  null,
   schedule             varchar(128)         null,
   spaceid              varchar(256)         null,
   connectiontypeid     varchar(256)         null,
   attemptscount        int                  null,
   delay                int                  null,
   issendnotification   bit                  null,
   issynctoexchange     bit                  null,
   openconferencebefore int                  null,
   isneverusepin        bit                  null,
   dateend              datetime             null,
   repeatcount          int                  null,
   pinpoliticsid        int                  null,
   pinschedule          varchar(128)         null,
   pindatestart         datetime             null,
   pincode              varchar(128)         null,
   typeid               int                  null,
   scheduletab          varchar(4)           null,
   pinscheduletab       varchar(4)           null,
   laststart            datetime             null,
   pinlaststart         datetime             null,
   rtffiletemplate      text                 collate Cyrillic_General_CI_AS null,
   constraint PK_OUTLOOKBOOKINGDEFAULT primary key (id)
)
go

/*==============================================================*/
/* Table: Products                                              */
/*==============================================================*/
create table dbo.Products (
   idr                  int                  identity,
   name                 varchar(150)         null,
   constraint PK_PRODUCTS primary key (idr)
)
go

/*==============================================================*/
/* Table: RefreshLog                                            */
/*==============================================================*/
create table dbo.RefreshLog (
   idr                  int                  not null,
   Info                 varchar(2048)        null,
   UploadDate           datetime             null,
   ServicesId           int                  not null,
   SitesIds             varchar(1000)        null,
   Mode                 int                  not null default 0,
   constraint PK_REFRESHLOG primary key (idr)
)
go

/*==============================================================*/
/* Table: RefreshToken                                          */
/*==============================================================*/
create table dbo.RefreshToken (
   Id                   int                  identity(1, 1),
   AspNetUserId         int                  not null,
   CreateDt             datetime             not null,
   FingerPrint          varchar(500)         collate Cyrillic_General_CI_AS null,
   Ip                   varchar(100)         collate Cyrillic_General_CI_AS null,
   Token                varchar(5000)        collate Cyrillic_General_CI_AS not null,
   EndDate              datetime             null,
   constraint PK_REFRESHTOKEN primary key (Id)
)
go

/*==============================================================*/
/* Table: Role                                                  */
/*==============================================================*/
create table dbo.Role (
   Name                 nvarchar(255)        null,
   DisplayName          nvarchar(255)        null,
   Description          varchar(255)         null,
   Id                   int                  identity,
   constraint PK_ROLE primary key (Id)
)
go

/*==============================================================*/
/* Table: SMTPSettings                                          */
/*==============================================================*/
create table SMTPSettings (
   idr                  int                  not null,
   server               varchar(250)         null,
   port                 int                  null,
   login                varchar(128)         null,
   pswd                 varchar(128)         null,
   addressfrom          varchar(128)         null,
   securemode           varchar(512)         null,
   auth                 varchar(50)          null,
   domain               varchar(100)         null,
   ntlmdomain           varchar(100)         null,
   constraint PK_SMTPSETTINGS primary key (idr)
)
go

/*==============================================================*/
/* Table: ServersGroups                                         */
/*==============================================================*/
create table dbo.ServersGroups (
   id                   int                  identity,
   name                 varchar(128)         null,
   description          varchar(256)         null,
   isusebalancer        int                  null,
   balanceralgid        int                  null,
   timezoneid           int                  null,
   constraint PK_SERVERSGROUPS primary key (id)
)
go

/*==============================================================*/
/* Table: Services                                              */
/*==============================================================*/
create table dbo.Services (
   id                   int                  identity,
   name                 varchar(100)         not null,
   conffilename         varchar(100)         not null,
   pathexe              varchar(100)         null,
   description          varchar(250)         null,
   constraint PK_SERVICES primary key (id)
)
go

/*==============================================================*/
/* Table: Space                                                 */
/*==============================================================*/
create table dbo.Space (
   id                   int                  identity,
   name                 varchar(512)         null,
   uri                  varchar(128)         null,
   tagcdr               varchar(128)         null,
   guid                 varchar(36)          null,
   password             varchar(128)         null,
   urialt               varchar(128)         null,
   passwordguest        varchar(128)         null,
   urivideo             varchar(128)         null,
   isguestaccessible    bit                  null,
   isavailableforbooking bit                  null,
   spacegroupsid        int                  null,
   callid               varchar(32)          null,
   serversgroupsid      int                  null,
   calllegprofileguid   varchar(36)          null,
   callbrandingprofileguid varchar(36)          null,
   ownerid              int                  null,
   constraint PK_SPACE primary key (id)
)
go

/*==============================================================*/
/* Table: Spacegroups                                           */
/*==============================================================*/
create table dbo.Spacegroups (
   id                   int                  identity,
   name                 varchar(128)         null,
   description          varchar(256)         null,
   constraint PK_SPACEGROUPS primary key (id)
)
go

/*==============================================================*/
/* Table: TableColumnSettings                                   */
/*==============================================================*/
create table dbo.TableColumnSettings (
   Id                   int                  identity,
   TableName            nvarchar(255)        not null,
   ColumnName           nvarchar(255)        not null,
   Title                nvarchar(255)        null,
   Value                nvarchar(255)        not null,
   "order"              int                  not null,
   MinWidth             int                  not null default 0,
   Wrap                 nvarchar(255)        null,
   Template             nvarchar(255)        null,
   ClassName            varchar(255)         null,
   RoleId               int                  null,
   NoSortable           bit                  not null default 0,
   NoResizable          bit                  not null default 0,
   ActionButton         bit                  not null default 0,
   CellRenderer         varchar(200)         not null default 'booleanColumnsNames',
   CellsWithoutHint     bit                  not null default 1,
   VisibleCheckBox      bit                  not null default 0,
   constraint PK_TABLECOLUMNSETTINGS primary key (Id)
)
go

/*==============================================================*/
/* Index: is_tablecolumnsettings_tablename_order                */
/*==============================================================*/
create unique index is_tablecolumnsettings_tablename_order on dbo.TableColumnSettings (
TableName ASC,
"order" ASC
)
go

/*==============================================================*/
/* Table: UserRoles                                             */
/*==============================================================*/
create table dbo.UserRoles (
   Id                   int                  identity,
   UserId               Int                  not null,
   RoleId               int                  not null,
   constraint PK_USERROLES primary key (Id)
)
go

/*==============================================================*/
/* Table: UserTableColumns                                      */
/*==============================================================*/
create table dbo.UserTableColumns (
   Id                   int                  identity,
   UserId               int                  not null,
   TableColumnId        int                  not null,
   "order"              int                  not null,
   Width                int                  not null default 0,
   constraint PK_USERTABLECOLUMNS primary key (Id)
)
go

/*==============================================================*/
/* Table: Users                                                 */
/*==============================================================*/
create table dbo.Users (
   Id                   int                  identity,
   Email                nvarchar(256)        null,
   PasswordHash         nvarchar(4000)       null,
   SecurityStamp        nvarchar(4000)       null,
   UserName             nvarchar(256)        null,
   Post                 nvarchar(256)        null,
   UserFullName         nvarchar(100)        null,
   SID                  nvarchar(256)        null,
   Provider             nvarchar(50)         null,
   APIKey               varchar(150)         null,
   NeedRls              smallint             null,
   Login                nvarchar(255)        null,
   NormalizedLogin      nvarchar(255)        null,
   NormalizedEmail      varchar(255)         null,
   constraint PK_Users primary key (Id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: bookingtype                                           */
/*==============================================================*/
create table dbo.bookingtype (
   idr                  int                  not null,
   name                 varchar(64)          not null,
   privatename          varchar(64)          not null,
   constraint PK_BOOKINGTYPE primary key (idr)
)
go

/*==============================================================*/
/* Table: licensexml                                            */
/*==============================================================*/
create table dbo.licensexml (
   Products             varchar(Max)         null,
   SerialNumber         varchar(50)          null,
   DateStart            varchar(50)          null,
   DateEnd              varchar(50)          null,
   Hash                 varchar(512)         null,
   FileAll              varchar(Max)         null
)
go

/*==============================================================*/
/* Table: mmsSettings                                           */
/*==============================================================*/
create table dbo.mmsSettings (
   idr                  int                  identity,
   ParamName            varchar(150)         not null,
   Value                varchar(150)         null,
   constraint PK_MMSSETTINGS primary key (idr)
)
go

/*==============================================================*/
/* Table: nfsServers                                            */
/*==============================================================*/
create table dbo.nfsServers (
   idr                  int                  identity,
   ip                   varchar(32)          not null,
   mount                varchar(128)         not null,
   constraint PK_NFSSERVERS primary key (idr)
)
go

/*==============================================================*/
/* Table: ntfEvents                                             */
/*==============================================================*/
create table dbo.ntfEvents (
   idr                  int                  identity,
   UploadDate           datetime             not null,
   ServiceId            int                  null,
   ProcessingDate       datetime             null,
   SubscriptionId       int                  null,
   WebPageName          varchar(256)         null,
   OperationInfo        varchar(128)         null,
   Param1               varchar(256)         null,
   Param2               varchar(8000)        null,
   ProcedureName        varchar(50)          null,
   Param3               datetime             null,
   constraint PK_NTFEVENTS primary key (idr)
)
go

/*==============================================================*/
/* Table: ntfNotifyLog                                          */
/*==============================================================*/
create table dbo.ntfNotifyLog (
   idr                  int                  identity,
   DateRecord           datetime             not null,
   SubscriptionId       int                  not null,
   Info                 varchar(max)         not null,
   EmployeeId           int                  null,
   NotifyTransportTypeId int                  not null,
   ProcessingDate       datetime             null,
   ErrorMsg             varchar(500)         null,
   AttemptCount         int                  null,
   AttemptDate          datetime             null,
   Address              varchar(2048)        null,
   NotifyEmail          varchar(4000)        null,
   NotifyPhone          varchar(50)          null,
   ntfEventsId          int                  null,
   InfoSubject          varchar(256)         null,
   constraint PK_NTFNOTIFYLOG primary key (idr)
)
go

/*==============================================================*/
/* Table: ntfNotifyParam                                        */
/*==============================================================*/
create table dbo.ntfNotifyParam (
   idr                  int                  identity,
   TemplateId           int                  not null,
   Name                 varchar(64)          null,
   DefaultValue         varchar(512)         null,
   Info                 varchar(512)         null,
   ParamName            varchar(32)          null,
   constraint PK_NTFNOTIFYPARAM primary key (idr)
)
go

/*==============================================================*/
/* Table: ntfNotifyTemplate                                     */
/*==============================================================*/
create table dbo.ntfNotifyTemplate (
   idr                  int                  identity,
   Name                 varchar(100)         not null,
   PrivateName          varchar(50)          not null,
   Info                 varchar(500)         null,
   ServiceId            int                  not null,
   ProcName             varchar(128)         null,
   ProcedureName        varchar(50)          null,
   WebPageName          varchar(50)          null,
   TemplateHTML         varchar(64)          null,
   constraint PK_NTFNOTIFYTEMPLATE primary key (idr)
)
go

/*==============================================================*/
/* Table: ntfSubscription                                       */
/*==============================================================*/
create table dbo.ntfSubscription (
   idr                  int                  identity,
   TemplateId           int                  not null,
   Name                 varchar(50)          not null,
   IsActive             bit                  null,
   constraint PK_NTFSUBSCRIPTION primary key (idr)
)
go

/*==============================================================*/
/* Table: ntfSubscriptionParamValue                             */
/*==============================================================*/
create table dbo.ntfSubscriptionParamValue (
   idr                  int                  identity,
   SubscriptionId       int                  not null,
   NotifyParamId        int                  not null,
   Value                varchar(512)         null,
   constraint PK_NTFSUBSCRIPTIONPARAMVALUE primary key (idr)
)
go

/*==============================================================*/
/* Table: perActionList                                         */
/*==============================================================*/
create table dbo.perActionList (
   Idr                  int                  identity,
   ViewName             varchar(256)         null,
   PrivateName          varchar(256)         null,
   constraint PK_PERACTIONLIST primary key (Idr)
)
go

/*==============================================================*/
/* Table: perObjectList                                         */
/*==============================================================*/
create table dbo.perObjectList (
   Idr                  int                  identity,
   ViewName             varchar(256)         null,
   PrivateName          varchar(256)         null,
   constraint PK_PEROBJECTLIST primary key (Idr)
)
go

/*==============================================================*/
/* Table: perObjectToAction                                     */
/*==============================================================*/
create table dbo.perObjectToAction (
   ActionId             int                  not null,
   ObjectId             int                  not null,
   Mask                 int                  not null,
   constraint PK_PEROBJECTTOACTION primary key (ActionId, ObjectId)
)
go

/*==============================================================*/
/* Table: perRoleActions                                        */
/*==============================================================*/
create table dbo.perRoleActions (
   RoleMainId           int                  not null,
   ObjectId             int                  not null,
   ActionId             int                  null,
   constraint PK_PERROLEACTIONS primary key (RoleMainId, ObjectId)
)
go

/*==============================================================*/
/* Table: perRoleMainList                                       */
/*==============================================================*/
create table dbo.perRoleMainList (
   Idr                  int                  not null,
   Name                 varchar(128)         null,
   Description          varchar(1024)        null,
   constraint PK_PERROLEMAINLIST primary key (Idr)
)
go

/*==============================================================*/
/* Table: perUserToRole                                         */
/*==============================================================*/
create table dbo.perUserToRole (
   RoleMainId           int                  not null,
   AspNetUserId         int                  not null,
   constraint PK_PERUSERTOROLE primary key (RoleMainId, AspNetUserId)
)
go

/*==============================================================*/
/* Table: pinpolitics                                           */
/*==============================================================*/
create table dbo.pinpolitics (
   idr                  int                  identity,
   name                 varchar(32)          not null,
   privatename          varchar(32)          not null,
   constraint PK_PINPOLITICS primary key (idr)
)
go

/*==============================================================*/
/* Table: recording                                             */
/*==============================================================*/
create table dbo.recording (
   idr                  int                  identity,
   bookingid            int                  not null,
   url                  varchar(256)         not null,
   datestart            datetime             null,
   dateend              datetime             null,
   constraint PK_RECORDING primary key (idr)
)
go

/*==============================================================*/
/* Table: recordingvksusers                                     */
/*==============================================================*/
create table dbo.recordingvksusers (
   idr                  int                  identity,
   recordingid          int                  not null,
   userid               int                  not null,
   daterecord           datetime             not null,
   isplay               bit                  not null,
   isdownload           bit                  not null,
   description          varchar(512)         null,
   constraint PK_RECORDINGVKSUSERS primary key (idr)
)
go

/*==============================================================*/
/* Index: ix_recordingvksusers_recordingid_userid               */
/*==============================================================*/
create unique index ix_recordingvksusers_recordingid_userid on dbo.recordingvksusers (
recordingid ASC,
userid ASC
)
go

/*==============================================================*/
/* Table: rlsLinkUserToObject                                   */
/*==============================================================*/
create table dbo.rlsLinkUserToObject (
   Id                   int                  identity,
   UserId               int                  not null,
   SettingObjectId      int                  not null,
   constraint PK_RLSLINKUSERTOOBJECT primary key (Id)
)
go

/*==============================================================*/
/* Index: ix_rlsLinkUserToObject_SettingObjectId_userId         */
/*==============================================================*/
create index ix_rlsLinkUserToObject_SettingObjectId_userId on dbo.rlsLinkUserToObject (
UserId ASC,
SettingObjectId ASC
)
go

/*==============================================================*/
/* Table: rlsPermissionToSpace                                  */
/*==============================================================*/
create table dbo.rlsPermissionToSpace (
   Id                   int                  identity,
   LinkId               int                  not null,
   SpaceId              int                  not null,
   constraint PK_RLSPERMISSIONTOSPACE primary key (Id)
)
go

/*==============================================================*/
/* Table: rlsSettingList                                        */
/*==============================================================*/
create table dbo.rlsSettingList (
   Id                   int                  not null,
   Name                 varchar(64)          not null,
   PrivateName          varchar(32)          not null,
   Visible              bit                  null,
   constraint PK_RLSSETTINGLIST primary key (Id)
)
go

/*==============================================================*/
/* Table: rlsSettingObjects                                     */
/*==============================================================*/
create table dbo.rlsSettingObjects (
   Id                   int                  not null,
   Name                 varchar(128)         not null,
   PrivateName          varchar(32)          not null,
   SettingListId        int                  not null,
   Visible              bit                  not null default 1,
   constraint PK_RLSSETTINGOBJECTS primary key (Id)
)
go

/*==============================================================*/
/* Index: ix_rlsSettingObjects_SettingListId_PrivateName        */
/*==============================================================*/
create index ix_rlsSettingObjects_SettingListId_PrivateName on dbo.rlsSettingObjects (
PrivateName ASC,
SettingListId ASC
)
go

/*==============================================================*/
/* Table: rlsSpace                                              */
/*==============================================================*/
create table dbo.rlsSpace (
   UserId               int                  not null,
   SpaceId              int                  not null
)
go

/*==============================================================*/
/* Index: ix_rlsspace_spaceId_userId                            */
/*==============================================================*/
create index ix_rlsspace_spaceId_userId on dbo.rlsSpace (
UserId ASC,
SpaceId ASC
)
go

/*==============================================================*/
/* Table: timezone                                              */
/*==============================================================*/
create table dbo.timezone (
   idr                  int                  not null,
   name                 varchar(64)          not null,
   privatename          varchar(64)          not null,
   offsetminute         int                  not null,
   standartid           varchar(64)          not null,
   constraint PK_TIMEZONE primary key (idr)
)
go

/*==============================================================*/
/* Index: IX_standartid                                         */
/*==============================================================*/
create unique index IX_standartid on dbo.timezone (
standartid ASC
)
go

/*==============================================================*/
/* Table: vksCallCurrent                                        */
/*==============================================================*/
create table dbo.vksCallCurrent (
   idr                  int                  identity,
   GUID                 uniqueidentifier     not null,
   ConferenceID         int                  null,
   CallCorrelatorGUID   uniqueidentifier     null,
   CallType             varchar(50)          not null,
   Name                 varchar(100)         not null,
   NumCallLegs          smallint             null,
   MaxCallLegs          smallint             null,
   NumParticipantsLocal smallint             null,
   Locked               bit                  not null,
   Recording            bit                  not null,
   RecordingStatus      bit                  not null,
   Streaming            bit                  not null,
   StreamingStatus      bit                  not null,
   AllowAllMuteSelf     bit                  not null,
   AllowAllPresentationContribution bit                  not null,
   MessagePosition      varchar(50)          null,
   MessageDuration      varchar(50)          null,
   ActiveWhenEmpty      bit                  not null,
   EndpointRecording    bit                  not null,
   DurationSeconds      int                  not null,
   CreateTime           datetime             null,
   StartTime            datetime             null,
   EndTime              datetime             null,
   DateLastRecord       datetime             not null,
   constraint PK_VKSCALLCURRENT primary key (idr)
)
go

/*==============================================================*/
/* Table: vksCallHistory                                        */
/*==============================================================*/
create table dbo.vksCallHistory (
   idr                  int                  identity,
   GUID                 uniqueidentifier     null,
   ConferenceID         int                  null,
   CallCorrelatorGUID   uniqueidentifier     null,
   CallType             varchar(50)          null,
   Name                 varchar(100)         not null,
   NumCallLegs          smallint             null,
   MaxCallLegs          smallint             null,
   NumParticipantsLocal smallint             null,
   Locked               bit                  null,
   Recording            bit                  null,
   RecordingStatus      bit                  null,
   Streaming            bit                  null,
   StreamingStatus      bit                  null,
   AllowAllMuteSelf     bit                  null,
   AllowAllPresentationContribution bit                  null,
   MessagePosition      varchar(50)          null,
   MessageDuration      varchar(50)          null,
   ActiveWhenEmpty      bit                  null,
   EndpointRecording    bit                  null,
   DurationSeconds      int                  null,
   CreateTime           datetime             null,
   StartTime            datetime             null,
   EndTime              datetime             null,
   DateLastRecord       datetime             not null,
   constraint PK_VKSCALLHISTORY primary key (idr)
)
go

/*==============================================================*/
/* Table: vksCallInstances                                      */
/*==============================================================*/
create table dbo.vksCallInstances (
   idr                  int                  identity,
   GUID                 uniqueidentifier     not null,
   CallGUID             uniqueidentifier     not null,
   RemoteParty          varchar(100)         not null,
   OriginalRemoteParty  varchar(100)         not null,
   Name                 varchar(100)         null,
   LocalAddress         varchar(100)         null,
   Type                 varchar(10)          null,
   Direction            varchar(10)          null,
   CanMove              bit                  null,
   CreateTime           datetime             null,
   StartTime            datetime             null,
   EndTime              datetime             null,
   DateLastRecord       datetime             not null,
   constraint PK_VKSCALLINSTANCES primary key (idr)
)
go

/*==============================================================*/
/* Table: vksCallInstancesConfig                                */
/*==============================================================*/
create table dbo.vksCallInstancesConfig (
   idr                  int                  identity,
   CallInstanceID       int                  not null,
   Name                 varchar(100)         not null,
   DefaultLayout        varchar(20)          not null,
   ParticipantLabels    bit                  not null,
   PresentationContributionAllowed bit                  not null,
   PresentationViewingAllowed bit                  not null,
   MuteOthersAllowed    bit                  not null,
   VideoMuteOthersAllowed bit                  not null,
   MuteSelfAllowed      bit                  not null,
   VideoMuteSelfAllowed bit                  not null,
   DisconnectOthersAllowed bit                  not null,
   TelepresenceCallsAllowed bit                  not null,
   sipPresentationChannelEnabled bit                  not null,
   ChangeLayoutAllowed  bit                  not null,
   bfcpMode             varchar(20)          not null,
   AllowAllMuteSelfAllowed bit                  not null,
   AllowAllPresentationContributionAllowed bit                  not null,
   ChangeJoinAudioMuteOverrideAllowed bit                  not null,
   RecordingControlAllowed bit                  not null,
   StreamingControlAllowed bit                  not null,
   DateLastRecord       datetime             not null,
   constraint PK_VKSCALLINSTANCESCONFIG primary key (idr)
)
go

/*==============================================================*/
/* Table: vksCallInstancesStatus                                */
/*==============================================================*/
create table dbo.vksCallInstancesStatus (
   idr                  int                  identity,
   CallInstanceID       int                  not null,
   State                varchar(10)          not null,
   DurationSeconds      int                  null,
   Direction            varchar(10)          not null,
   sipCallID            uniqueidentifier     null,
   GroupID              uniqueidentifier     null,
   EncryptedMedia       varchar(5)           null,
   UnencryptedMedia     varchar(5)           null,
   Layout               varchar(20)          null,
   CameraControlAvailable varchar(5)           null,
   rxAudioCodec         varchar(10)          null,
   rxAudioCodecPacketLossPercentage decimal(5,2)         null,
   rxAudioJitter        numeric              null,
   rxAudioBitRate       numeric              null,
   rxAudioGainApplied   decimal(5,2)         null,
   txAudioCodec         varchar(10)          null,
   txAudioCodecPacketLossPercentage decimal(5,2)         null,
   txAudioJitter        numeric              null,
   txAudioBitRate       numeric              null,
   txAudioRoundTripTime numeric              null,
   txVideoRole          varchar(20)          null,
   txVideoCodec         varchar(20)          null,
   txVideoWidth         numeric              null,
   txVideoHeight        numeric              null,
   txVideoFrameRate     decimal(4,1)         null,
   txVideoBitRate       numeric              null,
   txVideoPacketLossPercentage decimal(5,2)         null,
   txVIdeoJitter        numeric              null,
   txVIdeoRoundTripTime numeric              null,
   DateLastRecord       datetime             not null,
   constraint PK_VKSCALLINSTANCESSTATUS primary key (idr)
)
go

/*==============================================================*/
/* Table: vksConferenceCurrent                                  */
/*==============================================================*/
create table dbo.vksConferenceCurrent (
   idr                  int                  identity,
   GUID                 uniqueidentifier     not null,
   OwnerID              int                  null,
   OwnerJID             varchar(100)         null,
   CallID               varchar(50)          null,
   URI                  varchar(100)         null,
   StreamURL            varchar(500)         null,
   SecondaryURI         varchar(100)         null,
   Name                 varchar(100)         null,
   Description          varchar(100)         null,
   DefaultLayout        varchar(100)         null,
   AutoGenerated        bit                  not null,
   NonMemberAccess      bit                  null,
   Secret               varchar(100)         null,
   CreateTime           datetime             null,
   StartTime            datetime             null,
   EndTime              datetime             null,
   DateLastRecord       datetime             not null,
   constraint PK_VKSCONFERENCECURRENT primary key (idr)
)
go

/*==============================================================*/
/* Table: vksConferenceHistory                                  */
/*==============================================================*/
create table dbo.vksConferenceHistory (
   idr                  int                  identity,
   GUID                 uniqueidentifier     not null,
   OwnerID              int                  null,
   OwnerJID             varchar(100)         null,
   CallID               varchar(50)          null,
   URI                  varchar(100)         null,
   StreamURL            varchar(500)         null,
   SecondaryURI         varchar(100)         null,
   Name                 varchar(100)         null,
   Description          varchar(100)         null,
   DefaultLayout        varchar(100)         null,
   AutoGenerated        bit                  not null,
   NonMemberAccess      bit                  null,
   Secret               varchar(100)         null,
   CreateTime           datetime             null,
   StartTime            datetime             null,
   EndTime              datetime             null,
   DateLastRecord       datetime             not null,
   constraint PK_VKSCONFERENCEHISTORY primary key (idr)
)
go

/*==============================================================*/
/* Table: vksLicensing                                          */
/*==============================================================*/
create table dbo.vksLicensing (
   idr                  int                  identity,
   PersonalLicenseLimit numeric              null,
   SharedLicenseLimit   numeric              null,
   CapacityUnitLimit    numeric              null,
   Users                numeric              null,
   PersonalLicenses     numeric              null,
   ParticipantsActive   numeric              null,
   CallsActive          numeric              null,
   WeightedCallsActive  numeric(6,3)         null,
   CallsWithoutPersonalLicense numeric              null,
   WeightedCallsWithoutPersonalLicense numeric(6,3)         null,
   CapacityUnitUsage    numeric(6,3)         null,
   CapacityUnitUsageWithoutPersonalLicense numeric(6,3)         null,
   DateLastRecord       datetime             not null,
   constraint PK_VKSLICENSING primary key (idr)
)
go

/*==============================================================*/
/* Table: vksListNode                                           */
/*==============================================================*/
create table dbo.vksListNode (
   Idr                  int                  identity,
   ServersID            int                  not null,
   DateRecord           datetime             not null,
   Name                 varchar(256)         not null,
   callBridgeId         uniqueidentifier     not null,
   Description          varchar(256)         null,
   ipv4                 varchar(50)          null,
   IsDeleted            bit                  not null,
   constraint PK_VKSLISTNODE primary key (Idr)
)
go

/*==============================================================*/
/* Table: vksParticipants                                       */
/*==============================================================*/
create table dbo.vksParticipants (
   idr                  int                  identity,
   GUID                 uniqueidentifier     not null,
   CallGUID             uniqueidentifier     null,
   CallBridgeGUID       uniqueidentifier     null,
   UserJid              varchar(100)         null,
   NameUser             varchar(100)         null,
   URI                  varchar(100)         null,
   OriginalURI          varchar(100)         null,
   NumCallLegs          smallint             null,
   isActivator          bit                  null,
   CanMove              bit                  null,
   DefaultLayout        varchar(20)          null,
   State                varchar(20)          null,
   CameraControlAvailable bit                  null,
   DateLastRecord       datetime             not null,
   constraint PK_VKSPARTICIPANTS primary key (idr)
)
go

/*==============================================================*/
/* Table: vksServers                                            */
/*==============================================================*/
create table dbo.vksServers (
   idr                  int                  identity(1, 1),
   Name                 varchar(100)         not null,
   RemoteIPAddress      varchar(64)          null,
   Port                 int                  null,
   Login                varchar(100)         null,
   Password             varchar(100)         null,
   Mode                 varchar(6)           null,
   APIVersion           varchar(6)           null,
   ResultsSiteName      varchar(1)           null,
   Version              varchar(256)         null,
   VersionSoftware      varchar(256)         null,
   VendorID             int                  null,
   ModelID              int                  null,
   Enable               int                  null,
   serversgroupsid      int                  null,
   constraint PK__vksservers__DC501A7E59E2A0BF primary key (idr)
)
go

/*==============================================================*/
/* Index: ix_vksservers_name                                    */
/*==============================================================*/
create unique index ix_vksservers_name on dbo.vksServers (
Name ASC
)
go

/*==============================================================*/
/* Table: vksServersCommands                                    */
/*==============================================================*/
create table dbo.vksServersCommands (
   idr                  int                  identity(1, 1),
   serversid            int                  not null,
   enable               bit                  not null default 1,
   idc                  int                  not null,
   serviceid            int                  null,
   namec                varchar(100)         not null,
   command              varchar(100)         not null,
   starttime            time                 null,
   collectionfrequency  varchar(50)          null,
   fileextension        varchar(100)         not null,
   folderdata           varchar(100)         not null,
   daterecord           datetime             not null,
   lastdurms            int                  null,
   lastcnt              int                  null,
   errorcount           int                  null,
   nodeid               int                  null,
   constraint PK__vksservcomm__DC501A7E991C45BE primary key (idr)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: ix_vksservcomm_id3                                    */
/*==============================================================*/
create unique index ix_vksservcomm_id3 on dbo.vksServersCommands (
serversid ASC,
idc ASC,
serviceid ASC,
nodeid ASC
)
go

/*==============================================================*/
/* Table: vksUsers                                              */
/*==============================================================*/
create table dbo.vksUsers (
   idr                  int                  identity,
   GUID                 varchar(36)          null,
   JID                  varchar(100)         not null,
   Name                 varchar(100)         null,
   Phone                varchar(22)          null,
   Org                  varchar(100)         null,
   UserFunction         varchar(100)         null,
   Email                varchar(100)         null,
   UserActive           bit                  null,
   IsDeleted            bit                  null,
   DateLastRecord       datetime             null,
   ProfileGUID          uniqueidentifier     null,
   hasLicense           bit                  null,
   constraint PK_VKSUSERS primary key (idr)
)
go

/*==============================================================*/
/* Index: IX_jid                                                */
/*==============================================================*/
create unique index IX_jid on dbo.vksUsers (
JID ASC
)
go

/*==============================================================*/
/* Table: vksUsersConference                                    */
/*==============================================================*/
create table dbo.vksUsersConference (
   idr                  int                  identity,
   UserID               int                  not null,
   ConferenceID         int                  null,
   constraint PK_VKSUSERSCONFERENCE primary key (idr)
)
go

/*==============================================================*/
/* Table: vksUsersOther                                         */
/*==============================================================*/
create table dbo.vksUsersOther (
   idr                  int                  identity,
   Name                 varchar(128)         null,
   uri                  varchar(64)          not null,
   email                varchar(128)         null,
   constraint PK_VKSUSERSOTHER primary key (idr)
)
go

/*==============================================================*/
/* Table: vksUsersProfiles                                      */
/*==============================================================*/
create table dbo.vksUsersProfiles (
   idr                  int                  identity,
   GUID                 varchar(36)          null,
   CanCreateCoSpaces    bit                  null,
   CanCreateCalls       bit                  null,
   CanUseExternalDevices bit                  null,
   CanMakePhoneCalls    bit                  null,
   CanReceiveCalls      bit                  null,
   UserToUserMessagingAllowed bit                  null,
   HasLicense           bit                  null,
   DateLastRecord       datetime             null,
   name                 varchar(128)         null,
   description          varchar(256)         null,
   serversgroupsid      int                  null,
   constraint PK_VKSUSERSPROFILES primary key (idr)
)
go

/*==============================================================*/
/* Table: vksVendor                                             */
/*==============================================================*/
create table dbo.vksVendor (
   Idr                  int                  identity,
   Name                 varchar(150)         not null,
   Description          varchar(512)         null,
   constraint PK_VKSVENDOR primary key (Idr)
)
go

/*==============================================================*/
/* Table: vksVendorModel                                        */
/*==============================================================*/
create table dbo.vksVendorModel (
   Idr                  int                  identity,
   VendorId             int                  not null,
   Name                 varchar(150)         not null,
   Description          varchar(512)         null,
   constraint PK_VKSVENDORMODEL primary key (Idr)
)
go

/*==============================================================*/
/* View: BookingView                                            */
/*==============================================================*/
create view dbo.BookingView as
select b.id, 
       b.name, 
       b.description,
       vu.name + ' (' + vu.jid + ')'as owner,
       s.name as spacename,
       s.uri  as spaceuri,
       s.id   as spaceid,
       (CASE 
        WHEN b.schedule is null THEN 'нет'
        WHEN b.schedule = '' THEN 'нет'
        ELSE 'да' END) as schedule,
       b2.name as type,
       bs.ActiveStatus currentstatus,
       bs.NextRun nextrun,
       cast(null as datetime) counter,
       bs.dateend as dateend
       from dbo.Booking b 
left join dbo.bookingtype b2 ON b.typeid = b2.idr 
join dbo.Space s ON b.spaceid = s.id
left join dbo.vksUsers vu ON b.ownerid = vu.idr
left join dbo.BookingStatus bs ON b.id = bs.BookingId
go

/*==============================================================*/
/* View: RecordingVksUsersView                                  */
/*==============================================================*/
create view dbo.RecordingVksUsersView as
select
  r.idr id, 
  rv.idr recordingvksusersid,
  a.UserFullName + ' (' + a.Email + ')'as [user],
  coalesce(rv.isplay, 'false') as isplay,
  coalesce(rv.isdownload, 'false') as isdownload,
  rv.daterecord,
  a.id userid
From dbo.recording r
  join dbo.recordingvksusers rv on r.idr = rv.recordingid
  join dbo.AspNetUsers a on rv.userid = a.Id
go

/*==============================================================*/
/* View: RecordingsView                                         */
/*==============================================================*/
create view dbo.RecordingsView as
select 
  r.idr id, 
  b.name,
  s.uri as spaceuri,
  vu.name + ' (' + vu.jid + ')'as owner,
  r.datestart as datestart, 
  r.dateend as dateend, 
  datediff(SECOND, r.datestart, r.dateend) as duration,
  sg.name as serversgroupsname,
  case when s.uri is null then cast(rv.isplay as bit)
    else cast('true' as bit)
  end as isplay,
  case when s.uri is null then cast(rv.isdownload as bit)
    else cast('true' as bit)
  end as isdownload,
  case when s.uri is null then cast(rv.isshare as bit)
    else cast('true' as bit)
  end as isshare,
  case when s.uri is null then cast(rv.isdelete as bit)
    else cast('true' as bit)
  end as isdelete
From dbo.recording r
  join dbo.Booking b on r.bookingid = b.id 
  left join dbo.Space s  ON b.spaceid = s.id 
  left join dbo.ServersGroups sg ON sg.id = s.serversgroupsid
  left join dbo.vksUsers vu ON b.ownerid = vu.idr
  cross apply (select top 1 ord, isshare, isdelete, isplay, isdownload from 
    (
        select top 1 1 ord, 'false' as isshare, 'false' as isdelete, rvu.isplay, rvu.isdownload
            from dbo.recordingvksusers rvu join dbo.AspNetUsers anu on rvu.userid = anu.Id where rvu.recordingid = r.idr
        union all
        select top 1 2 ord, 'true' as isshare, 'true' as isdelete, 'true' isplay, 'true' isdownload
            from dbo.AspNetUsers anu where anu.NormalizedUserName = /*substring(vu.jid, 1, PATINDEX('%@%', */jid/* )-1)*/
        ) tmp1 order by ord
    ) rv
go

/*==============================================================*/
/* View: ServersGroupView                                       */
/*==============================================================*/
create view dbo.ServersGroupView as
select id,name,description from dbo.ServersGroups sg
go

/*==============================================================*/
/* View: SpaceUserRightsView                                    */
/*==============================================================*/
create view dbo.SpaceUserRightsView as
select 
    s.id
    , u.name + ' (' + u.jid + ')' as vksuser
    , ls.calllegprofileguid
    , ls.candestroy
    , ls.canchangename
    , ls.canchangecallId
    , ls.canaddremovemember
    , ls.canchangeuri
    , ls.canchangepasscode
    , ls.canremoveself 
    , ls.canchangenonmemberaccessallowed 
         cancangenonMemberAccessAllowed
from dbo.space s 
    join dbo.LinkSpaceToParticipant ls on s.id = ls.spaceid 
    join dbo.vksUsers u on ls.vksuserid = u.idr
go

/*==============================================================*/
/* View: SpacesView                                             */
/*==============================================================*/
create view dbo.SpacesView as
select s.id, s3.name as serversgroupsname, s.uri , s.name
from dbo.[Space] s 
  left join dbo.ServersGroups s3 ON s.serversgroupsid = s3.id
go

/*==============================================================*/
/* View: VksServersView                                         */
/*==============================================================*/
create view dbo.VksServersView as
select vs.idr as id , name, '' as basicPath, serversgroupsid from dbo.vksServers vs
go

/*==============================================================*/
/* View: VksUserProfilesView                                    */
/*==============================================================*/
create view dbo.VksUserProfilesView as
select vup.idr as id, vup.name, vup.description , sg.name as serversgroupsname 
from dbo.vksUsersProfiles vup 
  left join ServersGroups sg ON vup.serversgroupsid = sg.id
go

/*==============================================================*/
/* View: vAPIGetConferenceList                                  */
/*==============================================================*/
create view dbo.vAPIGetConferenceList as
SELECT        
     CASE WHEN callc.idr IS NOT NULL THEN 1 ELSE 0 END AS conferenceActive
    ,confc.idr AS conferenceID
    ,confc.GUID AS conferenceGUID
    ,confc.Name AS conferenceName
    ,confc.OwnerJID AS conferenceOwner
    ,confc.CallID AS conferenceNumber
    ,callc.DurationSeconds AS durationSecond
    ,callc.NumCallLegs AS memberCount
FROM dbo.vksConferenceCurrent AS confc 
LEFT JOIN dbo.vksCallCurrent AS callc ON callc.ConferenceID = confc.idr
go

/*==============================================================*/
/* View: vAPIGetConferenceListActive                            */
/*==============================================================*/
create view dbo.vAPIGetConferenceListActive as
SELECT        
     CASE WHEN callc.idr IS NOT NULL THEN 1 ELSE 0 END AS conferenceActive
    ,confc.idr AS conferenceID
    ,confc.GUID AS conferenceGUID
    ,confc.Name AS conferenceName
    ,confc.OwnerJID AS conferenceOwner
    ,confc.CallID AS conferenceNumber
    ,callc.DurationSeconds AS durationSecond
    ,callc.NumCallLegs AS memberCount
FROM dbo.vksConferenceCurrent AS confc 
LEFT OUTER JOIN dbo.vksCallCurrent AS callc ON callc.ConferenceID = confc.idr
WHERE (callc.idr IS NOT NULL)
go

/*==============================================================*/
/* View: vAPIGetConferenceListInactive                          */
/*==============================================================*/
create view dbo.vAPIGetConferenceListInactive as
SELECT        
     CASE WHEN callc.idr IS NOT NULL THEN 1 ELSE 0 END AS conferenceActive
    ,confc.idr AS conferenceID
    ,confc.GUID AS conferenceGUID
    ,confc.Name AS conferenceName
    ,confc.OwnerJID AS conferenceOwner
    ,confc.CallID AS conferenceNumber
    ,callc.DurationSeconds AS durationSecond
    ,callc.NumCallLegs AS memberCount
FROM dbo.vksConferenceCurrent AS confc 
LEFT OUTER JOIN dbo.vksCallCurrent AS callc ON callc.ConferenceID = confc.idr
WHERE (callc.idr IS NULL)
go

/*==============================================================*/
/* View: vAPIGetConferenceUsersList                             */
/*==============================================================*/
create view dbo.vAPIGetConferenceUsersList as
SELECT DISTINCT 
     CASE WHEN p.idr IS NOT NULL THEN 1 ELSE 0 END AS userActive
    ,usr.idr AS userId
    ,usr.GUID AS userGUID
    ,usr.JID AS userJID
    ,usr.Name AS userName
    ,usr.Org AS userOrg
    ,usr.UserFunction
    ,CASE WHEN confc.idr IN (SELECT DISTINCT ConferenceID FROM vksCallCurrent) THEN 1 ELSE 0 END AS conferenceActive
    ,confc.idr AS conferenceId
    ,confc.GUID AS conferenceGUID
    ,confc.Name AS conferenceName
    ,usr.Name AS conferenceOwner
FROM  dbo.vksUsers AS usr 
INNER JOIN dbo.vksUsersConference AS uc ON usr.idr = uc.UserID 
INNER JOIN dbo.vksConferenceCurrent AS confc ON confc.idr = uc.ConferenceID OR confc.OwnerID = usr.idr 
LEFT JOIN dbo.vksParticipants AS p ON usr.JID = p.UserJid AND p.CallGUID IS NOT NULL 
LEFT JOIN dbo.vksCallCurrent AS c ON p.CallGUID = c.GUID
go

alter table dbo.AspNetTreePages
   add constraint FK_ASPNETTR_FK_ASPNET_ASPNETTR foreign key (ParentId)
      references dbo.AspNetTreePages (idr)
go

alter table dbo.AspNetTreePages
   add constraint FK_aspnettreepages_aspnetroles foreign key (RoleId)
      references dbo.AspNetRoles (Id)
         on delete cascade
go

alter table dbo.AspNetUserRoles
   add constraint FK_AspNetUserRoles_AspNetRoles_RoleId foreign key (RoleId)
      references dbo.AspNetRoles (Id)
         on delete cascade
go

alter table dbo.AspNetUserRoles
   add constraint FK_AspNetUserRoles_AspNetUsers_UserId foreign key (UserId)
      references dbo.AspNetUsers (Id)
         on delete cascade
go

alter table dbo.Booking
   add constraint FK_BOOKING_FK_BOOKIN_SPACE foreign key (spaceid)
      references dbo.Space (id)
         on delete cascade
go

alter table dbo.Booking
   add constraint FK_BOOKING_FK_BOOKIN_BOOKINGT foreign key (typeid)
      references dbo.bookingtype (idr)
go

alter table dbo.Booking
   add constraint FK_BOOKING_FK_BOOKIN_CONNECTI foreign key (connectiontypeid)
      references dbo.ConnectionType (id)
go

alter table dbo.Booking
   add constraint FK_BOOKING_FK_BOOKIN_TIMEZONE foreign key (timezone)
      references dbo.timezone (idr)
go

alter table dbo.Booking
   add constraint FK_Booking_Vksusers foreign key (ownerid)
      references dbo.vksUsers (idr)
go

alter table dbo.Booking
   add constraint FK_pinpolitics_booking foreign key (pinpoliticsid)
      references dbo.pinpolitics (idr)
go

alter table dbo.BookingStatus
   add constraint FK_BookingStatus_Booking foreign key (BookingId)
      references dbo.Booking (id)
         on delete cascade
go

alter table dbo.BookingStatusPin
   add constraint FK_BookingStatusPin_Booking foreign key (BookingId)
      references dbo.Booking (id)
         on delete cascade
go

alter table dbo.BookingTaskStatus
   add constraint FK_BookingTaskStatus_Booking foreign key (BookingId)
      references dbo.Booking (id)
         on delete cascade
go

alter table dbo.LinkBookingToParticipant
   add constraint FK_LINKBOOK_FK_LINKBO_BOOKING foreign key (bookingid)
      references dbo.Booking (id)
         on delete cascade
go

alter table dbo.LinkBookingToParticipant
   add constraint FK_LINKBOOK_FK_LINKBO_VKSUSERS foreign key (vksparticipantid)
      references dbo.vksUsers (idr)
go

alter table dbo.LinkBookingTovksUsersOther
   add constraint FK_LINKBOOKINGTOVKSUSERSOTHER_BOOKING foreign key (bookingid)
      references dbo.Booking (id)
         on delete cascade
go

alter table dbo.LinkBookingTovksUsersOther
   add constraint FK_LINKBOOKINGTOVKSUSERSOTHER_VKSUSERSOTHER foreign key (vksusersotherid)
      references dbo.vksUsersOther (idr)
go

alter table dbo.LinkSpaceToParticipant
   add constraint FK_LINKSPAC_FK_LINKSP_SPACE foreign key (spaceid)
      references dbo.Space (id)
         on delete cascade
go

alter table dbo.LinkSpaceToParticipant
   add constraint FK_LINKSPAC_FK_LINKSP_VKSUSERS foreign key (vksuserid)
      references dbo.vksUsers (idr)
go

alter table dbo.ServersGroups
   add constraint FK_SERVERSG_FK_SERVER_BALANCER foreign key (balanceralgid)
      references dbo.BalancerList (id)
go

alter table dbo.ServersGroups
   add constraint FK_serversgroups_timezone foreign key (timezoneid)
      references dbo.ServersGroups (id)
go

alter table dbo.Space
   add constraint FK_SPACE_FK_SPACE__VKSUSERS foreign key (ownerid)
      references dbo.vksUsers (idr)
go

alter table dbo.Space
   add constraint FK_Space_Spacegroups foreign key (spacegroupsid)
      references dbo.Spacegroups (id)
go

alter table dbo.Space
   add constraint FK_space_serversgroups foreign key (serversgroupsid)
      references dbo.ServersGroups (id)
         on delete cascade
go

alter table dbo.UserRoles
   add constraint FK_UserRoles_Role foreign key (RoleId)
      references dbo.Role (Id)
go

alter table dbo.UserRoles
   add constraint FK_UserRoles_Users foreign key (UserId)
      references dbo.Users (Id)
go

alter table dbo.ntfEvents
   add constraint FK_ntfevents_services foreign key (ServiceId)
      references dbo.Services (id)
go

alter table dbo.ntfNotifyLog
   add constraint FK_NTFNOTIFYLOG_NTFEVENTS foreign key (ntfEventsId)
      references dbo.ntfEvents (idr)
go

alter table dbo.ntfNotifyParam
   add constraint FK_ntfrnotifyparam_ntfnotifytemplate foreign key (TemplateId)
      references dbo.ntfNotifyTemplate (idr)
         on delete cascade
go

alter table dbo.ntfSubscription
   add constraint FK_ntfsubscription_ntfnotifytemplate foreign key (TemplateId)
      references dbo.ntfNotifyTemplate (idr)
         on delete cascade
go

alter table dbo.ntfSubscriptionParamValue
   add constraint FK_ntfsubscriptionparamvalue_ntfnotifyparam foreign key (NotifyParamId)
      references dbo.ntfNotifyParam (idr)
go

alter table dbo.ntfSubscriptionParamValue
   add constraint FK_ntfsubscriptionparamvalue_ntfsubscription foreign key (SubscriptionId)
      references dbo.ntfSubscription (idr)
         on delete cascade
go

alter table dbo.perObjectToAction
   add constraint FK_PEROBJEC_REF_PERACTIO2 foreign key (ActionId)
      references dbo.perActionList (Idr)
go

alter table dbo.perObjectToAction
   add constraint FK_PEROBJEC_REF_PEROBJEC2 foreign key (ObjectId)
      references dbo.perObjectList (Idr)
go

alter table dbo.perRoleActions
   add constraint FK_PERROLEA_REF_PEROBJEC3 foreign key (ActionId, ObjectId)
      references dbo.perObjectToAction (ActionId, ObjectId)
go

alter table dbo.perRoleActions
   add constraint FK_ROLEACTION_REF_ROLEMAIN3 foreign key (RoleMainId)
      references dbo.perRoleMainList (Idr)
         on delete cascade
go

alter table dbo.perUserToRole
   add constraint FK_PERUSERT_REF_ASPNETUS foreign key (AspNetUserId)
      references dbo.AspNetUsers (Id)
         on delete cascade
go

alter table dbo.perUserToRole
   add constraint FK_PERUSERT_REF_PERROLEM foreign key (RoleMainId)
      references dbo.perRoleMainList (Idr)
         on delete cascade
go

alter table dbo.recording
   add constraint FK_recording_booking foreign key (bookingid)
      references dbo.Booking (id)
go

alter table dbo.recordingvksusers
   add constraint FK_recordingvksuser_aspnetusers foreign key (userid)
      references dbo.AspNetUsers (Id)
go

alter table dbo.recordingvksusers
   add constraint FK_recordingvksuser_recording foreign key (recordingid)
      references dbo.recording (idr)
go

alter table dbo.rlsLinkUserToObject
   add constraint FK_RLSLINKU_FK_RLSLIN_ASPNETUS foreign key (UserId)
      references dbo.AspNetUsers (Id)
         on delete cascade
go

alter table dbo.rlsLinkUserToObject
   add constraint FK_RLSLINKU_FK_RLSLIN_RLSSETTI foreign key (SettingObjectId)
      references dbo.rlsSettingObjects (Id)
         on delete cascade
go

alter table dbo.rlsPermissionToSpace
   add constraint FK_rlspermissiontospace_rlslinkusertoobject foreign key (LinkId)
      references dbo.rlsLinkUserToObject (Id)
         on delete cascade
go

alter table dbo.rlsPermissionToSpace
   add constraint FK_rlspermissiontospace_space foreign key (SpaceId)
      references dbo.Space (id)
go

alter table dbo.rlsSettingObjects
   add constraint FK_RLSSETTI_FK_RLSSET_RLSSETTI foreign key (SettingListId)
      references dbo.rlsSettingList (Id)
         on delete cascade
go

alter table dbo.vksCallCurrent
   add constraint FK_vksCallCurrent_vksConferenceCurrent foreign key (ConferenceID)
      references dbo.vksConferenceCurrent (idr)
go

alter table dbo.vksCallHistory
   add constraint FK_vksCallHistory_vksConferenceHistory foreign key (ConferenceID)
      references dbo.vksConferenceHistory (idr)
go

alter table dbo.vksConferenceCurrent
   add constraint FK_vksConferenceCurrent_vksUsers foreign key (OwnerID)
      references dbo.vksUsers (idr)
go

alter table dbo.vksConferenceHistory
   add constraint FK_vksConferenceHistory_vksUsers foreign key (OwnerID)
      references dbo.vksUsers (idr)
go

alter table dbo.vksListNode
   add constraint FK_vksListNode_vksServers foreign key (ServersID)
      references dbo.vksServers (idr)
go

alter table dbo.vksServers
   add constraint FK_VKSSERVE_FK_VKSSER_SERVERSG foreign key (serversgroupsid)
      references dbo.ServersGroups (id)
go

alter table dbo.vksServersCommands
   add constraint FK_vksServersCommands_Services foreign key (serviceid)
      references dbo.Services (id)
go

alter table dbo.vksServersCommands
   add constraint FK_vksServersCommands_vksListNode foreign key (nodeid)
      references dbo.vksListNode (Idr)
go

alter table dbo.vksServersCommands
   add constraint FK_vksServersCommands_vksServer foreign key (serversid)
      references dbo.vksServers (idr)
go

alter table dbo.vksUsersConference
   add constraint FK_vksUsersConference_vksConferenceCurrent foreign key (ConferenceID)
      references dbo.vksConferenceCurrent (idr)
go

alter table dbo.vksUsersConference
   add constraint FK_vksUsersConference_vksUsers foreign key (UserID)
      references dbo.vksUsers (idr)
go

alter table dbo.vksUsersProfiles
   add constraint FK_vksUsersProfiles_Serversgroups foreign key (serversgroupsid)
      references dbo.ServersGroups (id)
go

alter table dbo.vksVendorModel
   add constraint FK_vksVendorModel_vksVendor foreign key (VendorId)
      references dbo.vksVendor (Idr)
go


create procedure dbo.SetDefaultRLS (@UserId int) as
begin
    insert into dbo.rlsLinkUserToObject (UserId, SettingObjectId)
    select @UserId, Id from dbo.rlsSettingObjects  where PrivateName = 'ONLYUSERDATA' and settingListId in (select id from dbo.rlsSettingList where  PrivateName = 'SPACES_LIST')

    insert into dbo.rlsLinkUserToObject (UserId, SettingObjectId)
    select @UserId, Id from dbo.rlsSettingObjects  where PrivateName = 'ONLYUSERANDACCESSDATA' and settingListId in (select id from dbo.rlsSettingList where  PrivateName = 'RECORDING_LIST')

    /*add for. required for user role*/
    delete from dbo.AspNetUserRoles where UserId = @UserId 

    insert into dbo.AspNetUserRoles (UserId, RoleId)
    select @UserId, 1
    union all
    select @UserId, 10
    union all
    select @UserId, 103
    union all
    select @UserId, 102
    /*add for. required for user role*/
end
go


create procedure dbo.SetRLS (@UserId int) as
/*
update dbo.AspNetUsers set NeedRls = 1 where id = 144;
exec dbo.SetRLS @UserId = 144
select * from rlsEmployee where userid=144
*/
/*
#18165, 2021-06-28 - доработки по "Особому доступу"
#19323, 2021-09-01 - ограничение прав в соответствии с Типом SIM-карт и Лицевому счету
*/
begin
set nocount on;
-- проверяем нужно ли рассчитывать, только если NeedRls == 1 
declare @NeedRls smallint;
select @NeedRls = coalesce(NeedRls,0) from dbo.AspNetUsers where id = @UserId;
if @NeedRls != 1 
    return;

-- ================================ --
begin try

declare @is_error int = 0;
create table #rlsSpace_new(UserId int, SpaceId int);

declare @SettingList_org int = 1;

select top 1 @SettingList_org = id from dbo.rlsSettingList where PrivateName= 'SPACES_LIST';

--rlsSpace
insert into #rlsSpace_new (UserId, SpaceId)
select @UserId, SpaceId from (
select SpaceId from dbo.rlsLinkUserToObject l
    join dbo.rlsPermissionToSpace d on l.Id = d.LinkId
where l.UserId  = @UserId
) e

--ONLYUSERDATA
--if exists (SELECT top 1 1 FROM dbo.rlsLinkUserToObject u2o join dbo.rlsSettingObjects so on u2o.SettingObjectId = so.Id and so.SettingListId = 1 and so.PrivateName = 'ONLYUSERDATA' where u2o.UserId  = @UserId)
--insert into #rlsSpace_new (UserId, SpaceId)
--select @UserId, SpaceId from (
--select s.id SpaceId from dbo.AspNetUsers a
--    join dbo.vksUsers u on a.NormalizedUserName = substring(jid, 1, PATINDEX('%@%', jid )-1)
--    join dbo.Space s on u.idr = s.ownerid
--where a.Id  = @UserId
--) e

--sync dbo.rlsSpace
begin
    delete from dbo.rlsSpace where UserId = @UserId 
        and SpaceId in (
            select r.SpaceId from dbo.rlsSpace r
            left join #rlsSpace_new e on e.UserId = @UserId and e.UserId = r.UserId and e.SpaceId = r.SpaceId
            where r.UserId = @UserId
        and e.UserId is null);

    insert into dbo.rlsSpace (UserId, SpaceId)
    select distinct n.UserId, n.SpaceId from #rlsSpace_new n
    where not exists(select 1 from dbo.rlsSpace r where r.SpaceId = n.SpaceId and r.UserId = n.UserId);
end;

end try
begin catch
    set @is_error = 1;
    throw;
end catch;

-- При успешном выполнении обновлять поле dbo.AspNetUsers.NeedRls=0
if @is_error = 0 
    update dbo.AspNetUsers set NeedRls = 0 where id = @UserId and NeedRls != 0;
end
go


create procedure dbo.ntfGetNotifyBookingChange (@subscriptionId int=null, @Serviceid int = null) as
/*
--#19967 - постановка: отслеживает записи в dbo.ntfEvents, где OperatioInfo in ('BOOKING_ADD', 'BOOKING_EDIT', 'BOOKING_DELETE') и ProcessingDate is null.
                       формируются записи в dbo.ntfNotifyLog, только если есть хотя бы одна запись с условием IsActive = 1 
					   и TemplateId = (select idr from dbo.ntfNotifyTemplate where PrivateName = 'BookingChange')
*/
/*
exec dbo.ntfGetNotifyBookingChange
select * from dbo.ntfNotifyLog order by idr desc
--delete from dbo.ntfNotifyLog
*/
begin
    set nocount on;
    declare
         @NotifyTransportTypeId int = (select top 1 id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id)
        ,@def_serviceId int = (select top 1 id from dbo.services where name = 'mentolbooking');

    set @Serviceid = coalesce(@Serviceid, @def_serviceId)

    if @subscriptionId is null 
        set @SubscriptionId = (select top 1 idr from dbo.ntfSubscription where IsActive = 1 and TemplateId in (select idr from dbo.ntfNotifyTemplate where PrivateName = 'BookingChange') order by idr);
    /* если подписок нет - завершаем работу */
    if @SubscriptionId is null
        return;
    /* если передали подписку то проверяем что она активна */
    if not exists(select 1 from dbo.ntfSubscription where idr = @SubscriptionId and IsActive = 1 and TemplateId in (select idr from dbo.ntfNotifyTemplate where PrivateName = 'BookingChange')) 
        return;

    create table #params(
         ntfEventsId  int
        ,bookingId    int
        ,booking_name varchar(512) collate database_default
        ,email_json   varchar(8000) collate database_default
    )
    insert into #params(ntfEventsId,bookingId,booking_name,email_json)
    select 
         idr as ntfEventsId
        ,ltrim(rtrim(Param1)) as bookingId
        ,JSON_VALUE(param2, '$.booking_name') as booking_name
        ,ltrim(rtrim(Param2)) as email_json
    from dbo.ntfEvents ntf 
    where
        ntf.OperationInfo in ('BOOKING_ADD', 'BOOKING_EDIT', 'BOOKING_DELETE') 
    and ntf.ProcessingDate is null
    and    ServiceId = @serviceId;

    insert into dbo.ntfNotifyLog(
         DateRecord
        ,SubscriptionId
        ,Info
        ,EmployeeId
        ,NotifyEmail
        ,ntfEventsId
        ,InfoSubject
        ,NotifyTransportTypeId
        ,AttemptCount
    )
    select 
         getdate()              as DateRecord
        ,@SubscriptionId        as SubscriptionId
        ,''                     as Info
        ,0                      as EmployeeId
        ,el.email               as NotifyEmail
        ,p.ntfEventsId          as ntfEventsId
        ,p.booking_name         as InfoSubject
        ,@NotifyTransportTypeId as NotifyTransportTypeId
        ,0                        as AttemptCount
    from #params p
    /*
    outer apply(
        SELECT distinct vksusers_emails as email
        FROM OPENJSON(p.email_json, N'$.vksusers_emails')
        WITH (
            vksusers_emails      VARCHAR(50)    N'$'
        ) as e
    ) as el*/
    --склеивать email через запятую
    outer apply (
        SELECT
          COALESCE(STUFF
          (
            (
              SELECT 
                    ';' + ltrim(rtrim(t.email))
                from (
                SELECT distinct vksusers_emails as email
                FROM OPENJSON(p.email_json, N'$.vksusers_emails')
                WITH (
                    vksusers_emails      VARCHAR(4000)    N'$'
                ) as e
                ) as t
                ORDER BY t.email
                FOR XML PATH('') 
            ), 1, 1, N''
          ), N'') as email
    ) as el

    /*
    SELECT distinct vksusers_emails
    FROM OPENJSON(@param2, N'$.vksusers_emails')
    WITH (
        vksusers_emails      VARCHAR(200)    N'$'
    )
    select 
     JSON_VALUE(@param2, '$.booking_name') as booking_name
    ,JSON_QUERY(@param2, '$.vksusers_emails') as vksusers_emails

    declare @param2 as varchar(256) ='{ "Text_User": "","booking_name": "Еженедельное собрание", "vksusers_emails": ["krav@inlinepro.ru","nsolodkov@inlinepro.ru"]}'
    insert into ntfEvents(UploadDate, OperationInfo, param1, param2)
    select getdate(), 'BOOKING_ADD', '1', @param2

    */
end
go


create procedure dbo.ntfGetNotifyBookingChangePincodeNotification  (@subscriptionId int=null, @Serviceid int = null) 
as
--#20353 - постановка
/*
exec dbo.ntfGetNotifyBookingChangePincodeNotification
select * from dbo.ntfNotifyLog
*/
begin
declare
    @NotifyTransportTypeId int = (select top 1 id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id),
    @TemplateId int  = (select top 1 idr from dbo.ntfNotifyTemplate where PrivateName = 'BookingChangePincodeNotification' order by idr asc)
    --@SubscriptionId int = null;

    set nocount on;

    set @SubscriptionId = coalesce((select top 1 idr from dbo.ntfSubscription where IsActive = 1 and TemplateId = @TemplateId order by idr asc),@subscriptionId);

    -- если подписок нет - выходим, ничего не делаем
    if @SubscriptionId is null 
        return;

    create table #events(ntfEventsId int, Param1 varchar(256) collate database_default, Param2 varchar(8000) collate database_default);

    insert into #events(ntfEventsId, Param1, Param2)
    select e.idr as ntfEventsId, Param1, Param2 from dbo.ntfEvents e where OperationInfo in ('BOOKING_CHANGE_PINCODE_NOTIFICATION') and ProcessingDate is null;
    if exists(select 1 from #events)
    begin
        insert into dbo.ntfNotifyLog(DateRecord, SubscriptionId, Info, EmployeeId, NotifyEmail, ntfEventsId, InfoSubject, AttemptCount, NotifyTransportTypeId)
        select 
             getdate()       as DateRecord
            ,@SubscriptionId as SubscriptionId
            ,''              as Info
            ,0               as EmployeeId
            ,rtrim(ltrim(JSON_VALUE(param2, '$.vksUser_email'))) as NotifyEmail
            ,e.ntfEventsId   as ntfEventsId
            ,coalesce(JSON_VALUE(param2, '$.booking_name'),'') + ' Предстоящая смена ПИН-кода' as InfoSubject
            ,0               as AttemptCount
            ,@NotifyTransportTypeId       as NotifyTransportTypeId
        from #events e;
        --print 'rows inserted: '+cast(@@rowcount as varchar);
    end;
end;
go


create procedure dbo.ntfGetNotifyListForSend (@serviceId int, @subscriptionId int, @ProcedureName varchar(max)=null, @WebPageName varchar(max)=null) 
-- @serviceId - определяет от какого сервиса пришел сигнал
-- @subscriptionId - определяет для какой подписки необходимо получить данные
-- Один из параметров должен быть null, т.к. это разные сигналы
AS
/*
 exec [dbo].[ntfGetNotifyListForSend] null, null, null, 'B2B'
 */
 BEGIN
 -- определим, какой тип сигнала пришел: от сервисов или по конкретной подписке
 -- если от сервиса, то определяем подписки и выполняем процедуры
 -- если конкретная подписка, то определяем ее процедуру и выполняем
 declare @subscriptionIdTable table (Id int) -- массив в котором будут Id подписок,необходимых для выполнения

if (coalesce(@serviceId,-1) > 0 )
begin
    insert into @subscriptionIdTable (Id)
    select distinct s.idr from 
    dbo.ntfSubscription s
    join dbo.ntfNotifyTemplate templ on templ.idr = s.TemplateId
    where s.IsActive = 1 and templ.ServiceId = @serviceId
end

if len(@ProcedureName) > 0
begin
    insert into @subscriptionIdTable (Id)
    select distinct s.idr from 
    dbo.ntfSubscription s
    join dbo.ntfNotifyTemplate templ on templ.idr = s.TemplateId
    where s.IsActive = 1 and templ.ProcedureName in (select Data from dbo.Split(@ProcedureName,','))
end

if len(@WebPageName) > 0
begin
    insert into @subscriptionIdTable (Id)
    select distinct s.idr from 
    dbo.ntfSubscription s
    join dbo.ntfNotifyTemplate templ on templ.idr = s.TemplateId
    where s.IsActive = 1 and @WebPageName in (select Data from dbo.Split(templ.WebPageName,','))
end

if (coalesce(@subscriptionId,-1) > 0 )
begin
    insert into @subscriptionIdTable (Id) select @subscriptionId
end

declare @procExecSQL varchar(256)

DECLARE subscribtion_cursor CURSOR FOR   
    select distinct ' dbo.' + templ.ProcName + ' '+cast (s.idr as varchar(10))  as procExecSQL from 
    dbo.ntfSubscription s
        join @subscriptionIdTable st on st.Id = s.idr
        join dbo.ntfNotifyTemplate templ on templ.idr = s.TemplateId
    where Len(coalesce(templ.ProcName,'')) > 0 

OPEN subscribtion_cursor  

FETCH NEXT FROM subscribtion_cursor  INTO @procExecSQL  
WHILE @@FETCH_STATUS = 0  
BEGIN    
    --print @procExecSQL
    exec (@procExecSQL)       
    FETCH NEXT FROM subscribtion_cursor  INTO @procExecSQL
END   
CLOSE subscribtion_cursor
DEALLOCATE subscribtion_cursor

end
go


create procedure dbo.ntfGetNotifyRecordingAddDelete (@SubscriptionId int = null, @Serviceid int = null)
as
/*
Процедура отслеживает записи в dbo.ntfEvents, где OperatioInfo in ('RECORDING_DELETE') и ProcessingDate is null.
формируются записи в dbo.ntfNotifyLog, только если есть хотя бы одна запись с условием IsActive = 1 и TemplateId = (select idr from dbo.ntfNotifyTemplate where PrivateName = 'RecordingDelete')
*/
/*
--#20205 - постановка
--#20310 - переименовать в ntfGetNotifyRecordingAddDelete, значения в OperationInfo 'RECORDING_ADD' или 'RECORDING_DELETE'
			изменился шаблон (select top 1 idr from dbo.ntfNotifyTemplate where PrivateName = 'RecordingAddDelete' order by idr asc)
			в InfoSubject заполнять в зависимости от OperationInfo, 'RECORDING_ADD' - Добавлена или 'RECORDING_DELETE' - Удалена
*/
/*
exec dbo.ntfGetNotifyRecordingAddDelete
select * from dbo.ntfNotifyLog
*/
begin
declare
    @NotifyTransportTypeId int = (select top 1 id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id),
    @TemplateId int  = (select top 1 idr from dbo.ntfNotifyTemplate where PrivateName = 'RecordingAddDelete' order by idr asc)
    --@SubscriptionId int = null;

    set nocount on;

    set @SubscriptionId = (select top 1 idr from dbo.ntfSubscription where IsActive = 1 and TemplateId = @TemplateId order by idr asc);

    -- если подписок нет - выходим, ничего не делаем
    if @SubscriptionId is null 
        return;

    create table #events(ntfEventsId int, Param1 varchar(256) collate database_default, Param2 varchar(8000) collate database_default, OperationInfo varchar(256) collate database_default);

    insert into #events(ntfEventsId, Param1, Param2, OperationInfo)
    select e.idr as ntfEventsId, Param1, Param2, OperationInfo from dbo.ntfEvents e where OperationInfo in ('RECORDING_DELETE', 'RECORDING_ADD') and ProcessingDate is null;

    insert into dbo.ntfNotifyLog(DateRecord, SubscriptionId, Info, EmployeeId, NotifyEmail, ntfEventsId, InfoSubject, AttemptCount, NotifyTransportTypeId)
    select 
         getdate()       as DateRecord
        ,@SubscriptionId as SubscriptionId
        ,''              as Info
        ,0               as EmployeeId
        ,JSON_VALUE(param2, '$.vksUsersEmail') as NotifyEmail
        ,e.ntfEventsId   as ntfEventsId
        ,coalesce(JSON_VALUE(param2, '$.bookingName'),'') + case when OperationInfo = 'RECORDING_DELETE' then ' Удалена запись ВКС' else ' Добавлена запись ВКС' end as InfoSubject
        ,0               as AttemptCount
        ,@NotifyTransportTypeId       as NotifyTransportTypeId
    from #events e;
    --print 'rows inserted: '+cast(@@rowcount as varchar);

    /*json test
    declare @s varchar(max) ='
    {
       "recordingUrl": "\\\\server\\space_id\\video.mp4",
       "bookingName": "Еженедельное собрание",
       "vksUsersEmail": "krav@inlinepro.ru" 
    }    '
    select param2 
            ,JSON_VALUE(param2, '$.vksUsersEmail') as NotifyEmail
            ,JSON_VALUE(param2, '$.bookingName') as InfoSubject
            ,JSON_VALUE(param2, '$.recordingUrl') as recordingUrl
    from (select @s as param2) as t
    */
end;
go


create procedure dbo.ntfGetNotifyRecordingNotification  (@subscriptionId int=null, @Serviceid int = null) 
as
--#20327 - постановка
/*
exec dbo.ntfGetNotifyRecordingNotification
select * from dbo.ntfNotifyLog
*/
begin
set nocount on;
declare
    @NotifyTransportTypeId int = (select top 1 id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id),
    @TemplateId int  = (select top 1 idr from dbo.ntfNotifyTemplate where PrivateName = 'RecordingNotification' order by idr asc);

    set @SubscriptionId = coalesce(@subscriptionId, (select top 1 idr from dbo.ntfSubscription where IsActive = 1 and TemplateId = @TemplateId order by idr asc));

    -- если подписок нет - выходим, ничего не делаем
    if @SubscriptionId is null 
        return;

    create table #events(ntfEventsId int, Param1 varchar(256) collate database_default, Param2 varchar(8000) collate database_default);

    insert into #events(ntfEventsId, Param1, Param2)
    select e.idr as ntfEventsId, Param1, Param2 from dbo.ntfEvents e where OperationInfo in ('RECORDING_DELETE_NOTIFICATION') and ProcessingDate is null;
    if exists(select 1 from #events)
    begin
        insert into dbo.ntfNotifyLog(DateRecord, SubscriptionId, Info, EmployeeId, NotifyEmail, ntfEventsId, InfoSubject, AttemptCount, NotifyTransportTypeId)
        select 
             getdate()       as DateRecord
            ,@SubscriptionId as SubscriptionId
            ,''              as Info
            ,0               as EmployeeId
            ,rtrim(ltrim(JSON_VALUE(param2, '$.vksUsersEmail'))) as NotifyEmail
            ,e.ntfEventsId   as ntfEventsId
            ,coalesce(JSON_VALUE(param2, '$.bookingName'),'') + ' Предстоящее удаление записи ВКС' as InfoSubject
            ,0               as AttemptCount
            ,@NotifyTransportTypeId       as NotifyTransportTypeId
        from #events e;
        --print 'rows inserted: '+cast(@@rowcount as varchar);
    end;
    /*json test
    declare @s varchar(max) ='
    {
	   "recordingUrl": "\\\\server\\space_id\\video.mp4",
	   "bookingName": "Еженедельное собрание",
	   "vksUsersEmail": "krav@inlinepro.ru",
	   "daysBeforeDeletion": "30",
	   "deletionDate": "15.12.2021" 
   }'
    select param2 
            ,JSON_VALUE(param2, '$.vksUsersEmail') as NotifyEmail
            ,coalesce(JSON_VALUE(param2, '$.bookingName'),'') as bookingName
    from (select @s as param2) as t
    */
end;
go


create procedure dbo.ntfGetNotifyRecordingVksUsersChange (@subscriptionId int=null, @Serviceid int = null) 
as
--#20207 - постановка
/*
exec dbo.ntfGetNotifyRecordingVksUsersChange
select * from dbo.ntfNotifyLog
*/
begin
declare
    @NotifyTransportTypeId int = (select top 1 id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id),
    @TemplateId int  = (select top 1 idr from dbo.ntfNotifyTemplate where PrivateName = 'RecordingVksUsersChange' order by idr asc)
    --@SubscriptionId int = null;

    set nocount on;

    set @SubscriptionId = (select top 1 idr from dbo.ntfSubscription where IsActive = 1 and TemplateId = @TemplateId order by idr asc);

    -- если подписок нет - выходим, ничего не делаем
    if @SubscriptionId is null 
        return;

    create table #events(ntfEventsId int, Param1 varchar(256) collate database_default, Param2 varchar(8000) collate database_default);

    insert into #events(ntfEventsId, Param1, Param2)
    select e.idr as ntfEventsId, Param1, Param2 from dbo.ntfEvents e where OperationInfo in ('RECORDINGVKSUSERS_ADD', 'RECORDINGVKSUSERS_EDIT', 'RECORDINGVKSUSERS_DELETE') and ProcessingDate is null;
    if exists(select 1 from #events)
    begin
        insert into dbo.ntfNotifyLog(DateRecord, SubscriptionId, Info, EmployeeId, NotifyEmail, ntfEventsId, InfoSubject, AttemptCount, NotifyTransportTypeId)
        select 
             getdate()       as DateRecord
            ,@SubscriptionId as SubscriptionId
            ,''              as Info
            ,0               as EmployeeId
            ,rtrim(ltrim(JSON_VALUE(param2, '$.userEmail'))) as NotifyEmail
            ,e.ntfEventsId   as ntfEventsId
            ,coalesce(JSON_VALUE(param2, '$.bookingName'),'') + ' Права на запись ВКС' as InfoSubject
            ,0               as AttemptCount
            ,@NotifyTransportTypeId       as NotifyTransportTypeId
        from #events e;
        --print 'rows inserted: '+cast(@@rowcount as varchar);
    end;
    /*json test
    declare @s varchar(max) ='
    {
       "userName": "Kravchenko Nikolay",
       "userEmail": "krav@inlinepro.ru",
       "bookingName": "Еженедельное собрание",
       "recordingId": 25,
       "recordingUrl": "\\\\192.168.80.195\\space_id\\Еженедельное собрание.mp4",
       "recordingVksUsersIsPlay": true,
       "recordingVksUsersIsDownload": false,
       "recordingVksUsersDescription": "Решение вопросов по проекту"     
    }'
    select param2 
            ,JSON_VALUE(param2, '$.userEmail') as NotifyEmail
            ,coalesce(JSON_VALUE(param2, '$.bookingName'),'') as bookingName
    from (select @s as param2) as t
    */
end;
go


create procedure dbo.periodicSetRLS as
/*
exec dbo.periodicSetRLS;
*/
begin
    declare 
          @userId int = null
         ,@msg varchar(1024)=''
         ,@errorCnt int =0;
    if OBJECT_ID('tempdb..#users') is not null
        drop table #users;
    create table #users(userid int primary key)
    insert into #users(userid)
    select id from dbo.AspNetUsers order by id

    select top 1 @userId = userId from #users order by userid asc;
    while @userId is not null 
    begin
        print '@userId = '+cast(@userId as varchar);
        begin try

            update dbo.AspNetUsers set NeedRls = 1 where id = @UserId and (NeedRls != 1 or NeedRls is NULL);
            exec [dbo].[SetRLS] @UserId = @userId; 

        end try
        begin catch
            select @msg = ERROR_MESSAGE()
            set @errorCnt = @errorCnt + 1;
            print 'ERROR: @userId = '+cast(@userId as varchar)+',  '+isnull(@msg,'');
        end catch;

        -- get next userId
        select top 1 @userId = userId from #users where userId>@userId order by userid asc;
        if @@ROWCOUNT = 0 set @userId = null;
    end;

    if @errorCnt > 0 
    begin
        set @msg = 'Общее число ошибок '+cast(@errorCnt as varchar) +'.  ' + @msg;
        RAISERROR (@msg,16,1);
    end;

end
go

create proc dbo.ntfGetNotifyConferenceEnded (@subscriptionId int=null, @Serviceid int = null) as
--#20481 - постановка
/*
exec dbo.ntfGetNotifyConferenceEnded
select * from dbo.ntfNotifyLog
*/
begin
declare
    @NotifyTransportTypeId int = (select top 1 id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id),
    @TemplateId int  = (select top 1 idr from dbo.ntfNotifyTemplate where PrivateName = 'ConferenceEnded' order by idr asc)
    --@SubscriptionId int = null;

    set nocount on;

    set @SubscriptionId = coalesce((select top 1 idr from dbo.ntfSubscription where IsActive = 1 and TemplateId = @TemplateId order by idr asc),@subscriptionId);

    -- если подписок нет - выходим, ничего не делаем
    if @SubscriptionId is null 
        return;

    create table #events(ntfEventsId int, Param1 varchar(256) collate database_default, Param2 varchar(8000) collate database_default);

    insert into #events(ntfEventsId, Param1, Param2)
    select e.idr as ntfEventsId, Param1, Param2 from dbo.ntfEvents e where OperationInfo = 'CONFERENCE_ENDED' and ProcessingDate is null;
    if exists(select 1 from #events)
    begin
        insert into dbo.ntfNotifyLog(DateRecord, SubscriptionId, Info, EmployeeId, NotifyEmail, ntfEventsId, InfoSubject, AttemptCount, NotifyTransportTypeId)
        select 
             getdate()       as DateRecord
            ,@SubscriptionId as SubscriptionId
            ,''              as Info
            ,0               as EmployeeId
            ,rtrim(ltrim(JSON_VALUE(param2, '$.vksUsersEmail'))) as NotifyEmail
            ,e.ntfEventsId   as ntfEventsId
            ,coalesce(JSON_VALUE(param2, '$.bookingName'),'') + ' Встреча завершена' as InfoSubject
            ,0               as AttemptCount
            ,@NotifyTransportTypeId       as NotifyTransportTypeId
        from #events e;
        --print 'rows inserted: '+cast(@@rowcount as varchar);
    end;
end;
go