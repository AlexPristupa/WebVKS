/*==============================================================*/
/* View: vAPIGetConferenceList                                  */
/*==============================================================*/
create or replace view dbo.vAPIGetConferenceList as
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
LEFT JOIN dbo.vksCallCurrent AS callc ON callc.ConferenceID = confc.idr;

-- set view ownership
alter table  dbo.vAPIGetConferenceList owner to dbo;

/*==============================================================*/
/* View: vAPIGetConferenceListActive                            */
/*==============================================================*/
create or replace view dbo.vAPIGetConferenceListActive as
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
WHERE (callc.idr IS NOT NULL);

-- set view ownership
alter table  dbo.vAPIGetConferenceListActive owner to dbo;

/*==============================================================*/
/* View: vAPIGetConferenceListInactive                          */
/*==============================================================*/
create or replace view dbo.vAPIGetConferenceListInactive as
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
WHERE (callc.idr IS NULL);

-- set view ownership
alter table  dbo.vAPIGetConferenceListInactive owner to dbo;

/*==============================================================*/
/* View: vAPIGetConferenceUsersList                             */
/*==============================================================*/
create or replace view dbo.vAPIGetConferenceUsersList as
SELECT DISTINCT 
     CASE WHEN p.idr IS NOT NULL THEN 1 ELSE 0 END AS userActive
    ,usr.idr AS userId
    ,usr.GUID AS userGUID
    ,usr.JID AS userJID
    ,usr.Name AS userName
    ,usr.Org AS userOrg
    ,usr.UserFunction
    ,CASE WHEN confc.idr IN (SELECT DISTINCT ConferenceID FROM dbo.vksCallCurrent) THEN 1 ELSE 0 END AS conferenceActive
    ,confc.idr AS conferenceId
    ,confc.GUID AS conferenceGUID
    ,confc.Name AS conferenceName
    ,usr.Name AS conferenceOwner
FROM  dbo.vksUsers AS usr 
INNER JOIN dbo.vksUsersConference AS uc ON usr.idr = uc.UserID 
INNER JOIN dbo.vksConferenceCurrent AS confc ON confc.idr = uc.ConferenceID OR confc.OwnerID = usr.idr 
LEFT JOIN dbo.vksParticipants AS p ON usr.JID = p.UserJid AND p.CallGUID IS NOT NULL 
LEFT JOIN dbo.vksCallCurrent AS c ON p.CallGUID = c.GUID;

-- set view ownership
alter table  dbo.vAPIGetConferenceUsersList owner to dbo;

/*==============================================================*/
/* View: BookingView                                            */
/*==============================================================*/
create or replace view dbo.BookingView AS
select b.id, 
       b.name, 
       b.description,
       vu.name || ' (' || vu.jid || ')'as owner,
       s.name     as spacename,
       s.uri      as spaceuri,
	   s.id       as spaceid,
       cast((CASE 
        WHEN b.schedule is null THEN 'нет'
        WHEN b.schedule = '' THEN 'нет'
        ELSE 'да' END) as varchar(128)) as schedule,
       b2.name    as type,
       bs.ActiveStatus currentstatus,
       bs.NextRun nextrun,
       cast(null as timestamp) counter,
       bs.dateend as dateend
from dbo.Booking b 
  left join dbo.bookingtype b2 ON b.typeid = b2.idr 
  join dbo.Space s ON b.spaceid = s.id
  left join dbo.vksUsers vu ON b.ownerid = vu.idr
  left join dbo.BookingStatus bs ON b.id = bs.BookingId;
  
-- set view ownership
alter table  dbo.BookingView owner to dbo;

/*==============================================================*/
/* View: SpacesView                                             */
/*==============================================================*/
create or replace view dbo.SpacesView AS
select s.id, s3.name as serversgroupsname, s.uri , s.name
from dbo.Space s 
  left join dbo.ServersGroups s3 ON s.serversgroupsid = s3.id;

-- set view ownership
alter table  dbo.SpacesView owner to dbo;

/*==============================================================*/
/* View: RecordingsView                                         */
/*==============================================================*/
create or replace view dbo.RecordingsView AS
select 
  r.idr id, 
  b.name,
  s.uri as spaceuri,
  vu.name || ' (' || vu.jid || ')'as owner,
  r.datestart as datestart, 
  r.dateend as dateend, 
  datediff('second', r.datestart, r.dateend)as duration,
  sg.name as serversgroupsname,
  case when s.uri is null then cast(rv.isplay as bool)
    else true
  end as isplay,
  case when s.uri is null then cast(rv.isdownload as bool)
    else true
  end as isdownload,
  case when s.uri is null then cast(rv.isshare as bool)
    else true
  end as isshare,
  case when s.uri is null then cast(rv.isdelete as bool)
    else true
  end as isdelete
From dbo.recording r
  join dbo.Booking b on r.bookingid = b.id 
  left join dbo.Space s  ON b.spaceid = s.id 
  left join dbo.ServersGroups sg ON sg.id = s.serversgroupsid
  left join dbo.vksUsers vu ON b.ownerid = vu.idr
  inner join lateral  (select ord, isshare, isdelete, isplay, isdownload from 
    (
        (select 1 ord, false as isshare, false as isdelete, rvu.isplay, rvu.isdownload
            from dbo.recordingvksusers rvu join dbo.AspNetUsers anu on rvu.userid = anu.Id where rvu.recordingid = r.idr limit 1)
        union all
        (select 2 ord, true as isshare, true as isdelete, true isplay, true isdownload
            from dbo.AspNetUsers anu where anu.NormalizedUserName = /*substring(vu.jid, 1, PATINDEX('%@%', */jid/* )-1)*/ limit 1)
        ) tmp1 order by ord limit 1
    ) rv on true;
  
-- set view ownership
alter table dbo.RecordingsView owner to dbo;

/*==============================================================*/
/* View: VksUserProfilesView                                    */
/*==============================================================*/
create or replace view dbo.VksUserProfilesView AS
select vup.idr as id, vup.name, vup.description , sg.name as serversgroupsname 
from dbo.vksUsersProfiles vup 
  left join dbo.ServersGroups sg ON vup.serversgroupsid = sg.id;

-- set view ownership
alter table dbo.VksUserProfilesView owner to dbo;

/*==============================================================*/
/* View: VksServersView                                         */
/*==============================================================*/
CREATE OR REPLACE VIEW dbo.vksserversview
 AS
 SELECT vs.idr AS id,
    vs.name,
    ' '::varchar AS basicpath,
    vs.serversgroupsid
   FROM dbo.vksservers vs;

-- set view ownership
alter table dbo.vksserversview owner to dbo;

/*==============================================================*/
/* View: ServersGroupView                                       */
/*==============================================================*/
create or replace view dbo.ServersGroupView AS
select id, name, description from dbo.ServersGroups sg;

-- set view ownership
alter table dbo.ServersGroupView owner to dbo;

/*==============================================================*/
/* View: SpaceUserRightsView                                    */
/*==============================================================*/
create view dbo.SpaceUserRightsView as
select 
    s.id
    , u.name || ' (' || u.jid || ')' as vksuser
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
    join dbo.vksUsers u on ls.vksuserid = u.idr;
	
-- set view ownership
alter table dbo.SpaceUserRightsView owner to dbo;

/*==============================================================*/
/* View: RecordingVksUsersView                                  */
/*==============================================================*/
create view dbo.RecordingVksUsersView as
select
  r.idr id, 
  rv.idr recordingvksusersid,
  a.UserFullName || ' (' || a.Email || ')'as user,
  coalesce(rv.isplay, false) as isplay,
  coalesce(rv.isdownload, false) as isdownload,
  rv.daterecord,
  a.id userid
From dbo.recording r
  join dbo.recordingvksusers rv on r.idr = rv.recordingid
  join dbo.AspNetUsers a on rv.userid = a.Id;
	
-- set view ownership
alter table dbo.RecordingVksUsersView owner to dbo;
