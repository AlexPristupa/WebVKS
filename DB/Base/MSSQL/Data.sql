SET QUOTED_IDENTIFIER ON
go
SET NOCOUNT ON
go

CREATE SCHEMA Security  
GO  

/*-----------------Begin spaceFilter-----------------------*/
drop SECURITY POLICY if exists Security.spaceFilter   
go

drop FUNCTION IF EXISTS Security.fn_spaceSecuritypredicate
go
create   FUNCTION Security.fn_spaceSecuritypredicate(@IdentityColumnId int)  
    RETURNS TABLE  
    WITH SCHEMABINDING  
AS  
    return select 1 as fn_securitypredicate_result from (
	  select 
	    case 
		    when (SESSION_CONTEXT(N'UserId') is null ) then 1
            when (exists (select top 1 1 from dbo.rlsSpace where UserId = CAST(SESSION_CONTEXT(N'UserId') AS int) and @IdentityColumnId = SpaceId  ) ) then 1 
            when (exists (SELECT top 1 1 FROM dbo.rlsLinkUserToObject u2o join dbo.rlsSettingObjects so on u2o.SettingObjectId = so.Id and so.SettingListId = 1 and so.PrivateName = 'ONLYUSERDATA' where u2o.UserId  = CAST(SESSION_CONTEXT(N'UserId') AS int)) 
			and 
			(exists (select top 1 1 from dbo.AspNetUsers a join dbo.vksUsers u on a.NormalizedUserName = /*substring(jid, 1, PATINDEX('%@%', */jid /*)-1)*/ join dbo.Space s on u.idr = s.ownerid where a.Id  = CAST(SESSION_CONTEXT(N'UserId') AS int) and @IdentityColumnId = s.Id)
			) ) then 1
            when (exists (SELECT top 1 1 FROM dbo.rlsLinkUserToObject u2o join dbo.rlsSettingObjects so on u2o.SettingObjectId = so.Id and so.SettingListId = 1 and so.PrivateName = 'FULLACCESS' where u2o.UserId  = CAST(SESSION_CONTEXT(N'UserId') AS int))) then 1
		end as res
		) as t where res = 1;
GO  

CREATE SECURITY POLICY Security.spaceFilter  
    ADD FILTER PREDICATE Security.fn_spaceSecuritypredicate(id)   
        ON dbo.space  
    WITH (STATE = ON); 
go

/*-----------------Begin recordingFilter-----------------------*/
drop SECURITY POLICY if exists Security.recordingFilter   
go

drop FUNCTION IF EXISTS Security.fn_recordingSecuritypredicate
go
create   FUNCTION Security.fn_recordingSecuritypredicate(@IdentityColumnId int)  
    RETURNS TABLE  
    WITH SCHEMABINDING  
AS  
    return select 1 as fn_securitypredicate_result from (
      select 
        case 
            when (SESSION_CONTEXT(N'UserId') is null ) then 1
            --when (exists (select top 1 1 from dbo.rlsrecording where UserId = CAST(SESSION_CONTEXT(N'UserId') AS int) and @IdentityColumnId = recordingId  ) ) then 1 
            when (exists (SELECT top 1 1 FROM dbo.rlsLinkUserToObject u2o join dbo.rlsSettingObjects so on u2o.SettingObjectId = so.Id and so.SettingListId = 2 and so.PrivateName = 'ONLYUSERANDACCESSDATA' where u2o.UserId  = CAST(SESSION_CONTEXT(N'UserId') AS int)) 
            and 
            (exists (select top 1 1 from dbo.AspNetUsers a join dbo.vksUsers u on a.NormalizedUserName = /*substring(jid, 1, PATINDEX('%@%',*/ jid/* )-1)*/ join dbo.booking s on u.idr = s.ownerid join dbo.recording r on s.id = r.bookingid where a.Id  = CAST(SESSION_CONTEXT(N'UserId') AS int) and @IdentityColumnId = r.idr)
            ) ) then 1
            when (exists (select top 1 1 from dbo.recordingvksusers a where a.userid  = CAST(SESSION_CONTEXT(N'UserId') AS int) and @IdentityColumnId = a.recordingid)
            )  then 1
            when (exists (SELECT top 1 1 FROM dbo.rlsLinkUserToObject u2o join dbo.rlsSettingObjects so on u2o.SettingObjectId = so.Id and so.SettingListId = 2 and so.PrivateName = 'FULLACCESS' where u2o.UserId  = CAST(SESSION_CONTEXT(N'UserId') AS int))) then 1
        end as res
        ) as t where res = 1;
GO  

CREATE SECURITY POLICY Security.recordingFilter  
    ADD FILTER PREDICATE Security.fn_recordingSecuritypredicate(idr)   
        ON dbo.recording  
    WITH (STATE = ON); 
go

/*-----------------Begin recordingvksusersFilter-----------------------*/
drop SECURITY POLICY if exists Security.recordingvksuserFilter   
go

drop FUNCTION IF EXISTS Security.fn_recordingvksusersSecuritypredicate
go
create   FUNCTION Security.fn_recordingvksusersSecuritypredicate(@IdentityColumnId int)  
    RETURNS TABLE  
    WITH SCHEMABINDING  
AS  
    return select 1 as fn_securitypredicate_result from (
      select 
        case 
            when (SESSION_CONTEXT(N'UserId') is null ) then 1
            --when (exists (select top 1 1 from dbo.rlsrecordingvksuser where UserId = CAST(SESSION_CONTEXT(N'UserId') AS int) and @IdentityColumnId = recordingvksuserId  ) ) then 1 
            when (exists (SELECT top 1 1 FROM dbo.rlsLinkUserToObject u2o join dbo.rlsSettingObjects so on u2o.SettingObjectId = so.Id and so.SettingListId = 2 and so.PrivateName = 'ONLYUSERANDACCESSDATA' where u2o.UserId  = CAST(SESSION_CONTEXT(N'UserId') AS int)) 
            and
            (exists (select top 1 1 from dbo.recordingvksusers a where a.userid  = CAST(SESSION_CONTEXT(N'UserId') AS int) and @IdentityColumnId = a.idr)
            ) ) then 1
            when (exists (SELECT top 1 1 FROM dbo.rlsLinkUserToObject u2o join dbo.rlsSettingObjects so on u2o.SettingObjectId = so.Id and so.SettingListId = 2 and so.PrivateName = 'ONLYUSERANDACCESSDATA' where u2o.UserId  = CAST(SESSION_CONTEXT(N'UserId') AS int)) 
            and 
            (exists (select top 1 1 from dbo.AspNetUsers a join dbo.vksUsers u on a.NormalizedUserName = /*substring(jid, 1, PATINDEX('%@%',*/ jid/* )-1)*/ join dbo.booking s on u.idr = s.ownerid join dbo.recording r on s.id = r.bookingid join dbo.recordingvksusers rv on r.idr = rv.recordingid where a.Id  = CAST(SESSION_CONTEXT(N'UserId') AS int) and @IdentityColumnId = rv.idr)
            ) ) then 1
            when (exists (SELECT top 1 1 FROM dbo.rlsLinkUserToObject u2o join dbo.rlsSettingObjects so on u2o.SettingObjectId = so.Id and so.SettingListId = 2 and so.PrivateName = 'FULLACCESS' where u2o.UserId  = CAST(SESSION_CONTEXT(N'UserId') AS int))) then 1
        end as res
        ) as t where res = 1;
GO 

CREATE SECURITY POLICY Security.recordingvksusersFilter  
    ADD FILTER PREDICATE Security.fn_recordingvksusersSecuritypredicate(idr)   
        ON dbo.recordingvksusers  
    WITH (STATE = ON); 
go

--services
if not exists (select 1 from dbo.services where name = 'mentolciscovks')
insert into dbo.services (name, conffilename, pathexe, description)
values('mentolciscovks', 'mentolciscovks.exe.conf', NULL, NULL)
go

if not exists (select 1 from dbo.services where name = 'mentolbooking')
INSERT INTO dbo.Services (name,    conffilename,    pathexe,    description)
VALUES ('mentolbooking',    'mentolbooking.exe.conf',    NULL,    NULL)
go

if not exists (select 1 from dbo.services where name = 'mentolnotifierenterprise')
insert into dbo.services (name, conffilename, pathexe, description)
values('mentolnotifierenterprise', 'mentolnotifierenterprise.exe.conf', NULL, NULL)
go

if not exists (select 1 from dbo.services where name = 'mentolcmscollector')
insert into dbo.services (name, conffilename, pathexe, description)
values('mentolcmscollector', 'mentolcmscollector.exe.conf', NULL, NULL)
go

set IDENTITY_INSERT dbo.AspNetRoles on 
go

if not exists (select 1 from dbo.AspNetRoles where Id = 1)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (1, 'MMS', 'MMS', NULL, 'Система управления конференциями')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 10)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (10, 'MMS_BOOKING', 'MMS_BOOKING', 1, 'Бронирование')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 11)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (11, 'MMS_SETTINGS', 'MMS_SETTINGS', 1, 'Настройки')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 12)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (12, 'MMS_CMS', 'MMS_CMS', 1, 'CMS')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 13)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (13, 'MMS_REPORTS', 'MMS_REPORTS', 1, 'Отчеты')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 14)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (14, 'MMS_USERS', 'MMS_USERS', 1, 'Пользователи')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 100)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (100, 'MMS_BOOKING_BOOKING', 'MMS_BOOKING_BOOKING', 10, 'Бронирования')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 101)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (101, 'MMS_BOOKING_ROOMS', 'MMS_BOOKING_ROOMS', 10, 'Комнаты')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 102)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (102, 'MMS_BOOKING_RECORDS', 'MMS_BOOKING_RECORDS', 10, 'Записи')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 103)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (103, 'MMS_BOOKING_BOOKING_PERSON', 'MMS_BOOKING_BOOKING_PERSON', 10, 'Бронирования')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 200)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (200, 'MMS_SETTINGS_USERPROFILES', 'MMS_SETTINGS_USERPROFILES', 11, 'Профили пользователей')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 201)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (201, 'MMS_SETTINGS_GROUPSOFROOMS', 'MMS_SETTINGS_GROUPSOFROOMS', 11, 'Группы комнат')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 202)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (202, 'MMS_SETTINGS_RECORDSTORES', 'MMS_SETTINGS_RECORDSTORES', 11, 'Хранилища записей')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 203)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (203, 'MMS_SETTINGS_EXCHANGE', 'MMS_SETTINGS_EXCHANGE', 11, 'Exchange')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 300)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (300, 'MMS_CMS_SERVERS', 'MMS_CMS_SERVERS', 12, 'Сервера')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 301)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (301, 'MMS_CMS_GROUPS', 'MMS_CMS_GROUPS', 12, 'Группы')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 400)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (400, 'MMS_REPORTS_REPORTS', 'MMS_REPORTS_REPORTS', 13, 'Отчеты')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 401)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (401, 'MMS_REPORTS_DISTRIBUTIONOFREPORTS', 'MMS_REPORTS_DISTRIBUTIONOFREPORTS', 13, 'Рассылка отчетов')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 500)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (500, 'MMS_USERS_USERS', 'MMS_USERS_USERS', 14, 'Пользователи')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 501)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (501, 'MMS_USERS_ROLES', 'MMS_USERS_ROLES', 14, 'Роли')
go
if not exists (select 1 from dbo.AspNetRoles where Id = 502)
insert into dbo.AspNetRoles (Id, Name, NormalizedName, ParentId, ViewName) values (502, 'MMS_USERS_LOGS', 'MMS_USERS_LOGS', 14, 'Журнал')
go

set IDENTITY_INSERT dbo.AspNetRoles off
go

set IDENTITY_INSERT dbo.AspNetTreePages ON
go

-- Level 1
if not exists (select 1 from dbo.AspNetTreePages where idr = 1)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (1, 'Система управления конференциями', NULL, 1)
go
-- Level 2 Система управления конференциями
if not exists (select 1 from dbo.AspNetTreePages where idr = 10)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (10, 'Бронирование', 1, 10)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 11)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (11, 'Настройки', 1, 11)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 12)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (12, 'CMS', 1, 12)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 13)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (13, 'Отчеты', 1, 13)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 14)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (14, 'Пользователи', 1, 14)
go
-- Level 3 Бронирование
if not exists (select 1 from dbo.AspNetTreePages where idr = 100)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (100, 'Бронирования', 10, 100)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 101)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (101, 'Комнаты', 10, 101)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 102)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (102, 'Записи', 10, 102)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 103)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (103, 'Бронирования', 10, 103)
go
-- Level 3 Настройки
if not exists (select 1 from dbo.AspNetTreePages where idr = 200)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (200, 'Профили пользователей', 11, 200)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 201)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (201, 'Группы комнат', 11, 201)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 202)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (202, 'Хранилища записей', 11, 202)
go
-- Level 3 CMS
if not exists (select 1 from dbo.AspNetTreePages where idr = 300)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (300, 'Сервера', 12, 300)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 301)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (301, 'Группы', 12, 301)
go
-- Level 3 Отчеты
if not exists (select 1 from dbo.AspNetTreePages where idr = 400)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (400, 'Отчеты', 13, 400)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 401)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (401, 'Рассылка отчетов', 13, 401)
go
-- Level 3 Пользователи
if not exists (select 1 from dbo.AspNetTreePages where idr = 500)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (500, 'Пользователи', 14, 500)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 501)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (501, 'Роли', 14, 501)
go
if not exists (select 1 from dbo.AspNetTreePages where idr = 502)
insert into dbo.AspNetTreePages(idr, ViewName, ParentId, RoleId) values (502, 'Журнал', 14, 502)
go

set IDENTITY_INSERT dbo.AspNetTreePages OFF
go

--dbo.rlsSettingList
if not exists (select 1 from  dbo.rlsSettingList where  PrivateName = 'SPACES_LIST')
insert into dbo.rlsSettingList (Id, Name, PrivateName, Visible) values (1, 'Список комнат пользователя','SPACES_LIST',1)
go
if not exists (select 1 from  dbo.rlsSettingList where  PrivateName = 'RECORDING_LIST')
insert into dbo.rlsSettingList (Id, Name, PrivateName, Visible) values (2, 'Список записей пользователя','RECORDING_LIST',1)
go

--dbo.rlsSettingObjects
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Id = so.SettingListId and sl.PrivateName = 'SPACES_LIST' where  so.PrivateName = 'FULLACCESS')
insert into dbo.rlsSettingObjects (Id, Name, PrivateName, SettingListId) select 1, 'Все','FULLACCESS', Id from  dbo.rlsSettingList where  PrivateName = 'SPACES_LIST'
go
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Id = so.SettingListId and sl.PrivateName = 'SPACES_LIST' where  so.PrivateName = 'ONLYUSERDATA')
insert into dbo.rlsSettingObjects (Id, Name, PrivateName, SettingListId) select 2, 'Только данные пользователя','ONLYUSERDATA', Id from  dbo.rlsSettingList where  PrivateName = 'SPACES_LIST'
go
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Id = so.SettingListId and sl.PrivateName = 'SPACES_LIST' where  so.PrivateName = 'LIST_ACCESS')
insert into dbo.rlsSettingObjects (Id, Name, PrivateName, SettingListId) select 3, 'Перечень значений','LIST_ACCESS', Id from  dbo.rlsSettingList where  PrivateName = 'SPACES_LIST'
go
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Id = so.SettingListId and sl.PrivateName = 'RECORDING_LIST' where  so.PrivateName = 'FULLACCESS')
insert into dbo.rlsSettingObjects (Id, Name, PrivateName, SettingListId) select 4, 'Все','FULLACCESS', Id from  dbo.rlsSettingList where  PrivateName = 'RECORDING_LIST'
go
if not exists (select 1 from  dbo.rlsSettingObjects so inner join dbo.rlsSettingList sl on sl.Id = so.SettingListId and sl.PrivateName = 'RECORDING_LIST' where  so.PrivateName = 'ONLYUSERANDACCESSDATA')
insert into dbo.rlsSettingObjects (Id, Name, PrivateName, SettingListId) select 5, 'Только записи пользователя и доступные записи','ONLYUSERANDACCESSDATA', Id from  dbo.rlsSettingList where  PrivateName = 'RECORDING_LIST'
go

if not exists(select 1 from dbo.AspNetUsers where UserName = 'secadmin')
begin
set IDENTITY_INSERT dbo.AspNetUsers ON
INSERT INTO dbo.AspNetUsers (Id, AccessFailedCount, ConcurrencyStamp, Email, EmailConfirmed, LockoutEnabled, LockoutEnd, NormalizedEmail, NormalizedUserName, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName, UserFullName, Post)
VALUES
  (1, 0, N'c542cd3d-73ca-4de1-8416-8a015c65b754', NULL, 0, 1, NULL, NULL, N'SECADMIN',
   N'AQAAAAEAACcQAAAAEDC5xk9bH9OdA2my6dt83d04CS/JZS/k7AOE/WwZmq4/J0hC/VU58hmZnajy+d+tMg==', 
   NULL, 0, N'753bd2a8-4948-4462-a12f-d28821c46ee9', 0, 
   N'secadmin', 'Администратор','Администратор Безопасности')
set IDENTITY_INSERT dbo.AspNetUsers OFF
--insert into [dbo].[AspNetUserRoles] values (1, 3)
--insert into [dbo].[AspNetUserRoles] values (1, 31)
--insert into [dbo].[AspNetUserRoles] values (1, 302)
--insert into [dbo].[AspNetUserRoles] values (1, 303)
end
go

--default user admin/admin
if not exists(select 1 from dbo.AspNetUsers where UserName = 'admin')
begin
declare @userid int

INSERT INTO dbo.AspNetUsers (AccessFailedCount, ConcurrencyStamp, Email, EmailConfirmed, LockoutEnabled, LockoutEnd, NormalizedEmail, NormalizedUserName, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName, UserFullName, Post)
VALUES
  (0, N'aa2d62ef-bb37-4ebe-a0a8-1d4f88a66e47', NULL, 0, 1, NULL, NULL, N'ADMIN', N'AQAAAAEAACcQAAAAENPwtvUba62zpWdZhzUnZ7USUKUWQ3mS1CvntdYFEeeaYQGhVQC6ObiRYaCnWPYXWA==', NULL, 0, N'904a1cf0-813f-4304-a6a2-ff1c94039733', 0, N'admin', 'admin','Администратор системы')

set @userid = @@IDENTITY

insert into dbo.AspNetUserRoles (UserId,RoleId)
select @userid as UserId, ar.id as RoleId from dbo.AspNetRoles ar
left join dbo.AspNetUserRoles ur on ur.UserId = @userid and ur.RoleId = ar.id
where ar.id is not null and ur.UserId is null
group by ar.id;

insert into dbo.rlsLinkUserToObject(UserId, SettingObjectId)
select @userid as UserId, o.id as SettingObjectId from dbo.rlsSettingObjects o
inner join dbo.RlsSettingList l on o.SettingListId=l.Id 
left join dbo.rlsLinkUserToObject rlu on rlu.UserId = @userid and rlu.SettingObjectId = o.id
where o.PrivateName='FULLACCESS'
and rlu.Id is null;
end;
go

--FilterOperationsList
if not exists (select 1 from dbo.FilterOperationsList where Operand = '>')
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt)  values('>','больше',0)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = '<')
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('<','меньше',0)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = '>=')
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('>=','больше или равно',0)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = '<=')
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('<=','меньше или равно',0)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = '=' and ColumnTypeFilt=3) 
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('=','равно',3)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = '!=' and ColumnTypeFilt=3) 
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('!=','не равно',3)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = '~') 
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('~','содержит точно',1)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = '!~') 
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('!~','не содержит точно',1)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = '~*' and ColumnTypeFilt=1) 
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('~*','содержит слева',1)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = '*~' and ColumnTypeFilt=1) 
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('*~','содержит справа',1)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = '*~*') 
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('*~*','содержит',1)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = '!*~*') 
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('!*~*','не содержит',1)
go
if not exists(select 1 from dbo.FilterOperationsList where OperationName='равно' and Operand='=' and ColumnTypeFilt=8)
insert into dbo.FilterOperationsList (OperationName,Operand,ColumnTypeFilt) values ('равно','=',8)
go
if not exists(select 1 from dbo.FilterOperationsList where OperationName='не равно' and Operand='!=' and ColumnTypeFilt=8)
insert into dbo.FilterOperationsList (OperationName,Operand,ColumnTypeFilt) values ('не равно','!=',8)
go
if not exists(select 1 from dbo.FilterOperationsList where OperationName='содержит слева' and Operand='~*' and ColumnTypeFilt=8)
insert into dbo.FilterOperationsList (OperationName,Operand,ColumnTypeFilt) values ('содержит слева','~*',8)
go
if not exists(select 1 from dbo.FilterOperationsList where OperationName='содержит справа' and Operand='*~' and ColumnTypeFilt=8)
insert into dbo.FilterOperationsList (OperationName,Operand,ColumnTypeFilt) values ('содержит справа','*~',8)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = 'null' and ColumnTypeFilt = 1) 
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('null','не указано',1)
go
if not exists (select 1 from dbo.FilterOperationsList where Operand = 'null' and ColumnTypeFilt = 2) 
insert into dbo.FilterOperationsList(Operand, OperationName,ColumnTypeFilt) values('null','не указано',2)
go
if not exists(select 1 from dbo.FilterOperationsList where OperationName='содержит слева' and Operand='~*' and ColumnTypeFilt=8)
insert into dbo.FilterOperationsList (OperationName,Operand,ColumnTypeFilt) values ('не содержит слева','!~*',8)
go
if not exists(select 1 from dbo.FilterOperationsList where OperationName='содержит справа' and Operand='*~' and ColumnTypeFilt=8)
insert into dbo.FilterOperationsList (OperationName,Operand,ColumnTypeFilt) values ('не содержит справа','!*~',8)
go

--FilterForColumnTypeList
If not exists (select 1 from dbo.FilterForColumnTypeList where TypeId = 1)
INSERT INTO FilterForColumnTypeList (TypeId, TypeName, DataQuery) VALUES(1, 'выбор', 'select vc.id, vc.value,   
 (case when FValue IS NULL then '''' else ''check'' end) as state  
from (select ROW_NUMBER() OVER(ORDER BY {0} ASC) as id, {0} as value  from {1}  group by {0}) vc  
left join (select fv.FValue from dbo.FiltersList fl  
inner join dbo.FilterValue fv on fv.FilterId = fl.idr  
inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId
where fl.idr = {2} and fcl.ColumnName = ''{0}'') as fcl on fcl.FValue = vc.value')
go
If not exists (select 1 from dbo.FilterForColumnTypeList where TypeId = 2)
INSERT INTO FilterForColumnTypeList (TypeId, TypeName, DataQuery) VALUES(2, 'временной', 'select ROW_NUMBER() OVER(ORDER BY fv.FValue ASC) as id, fv.FValue as value, fol.Operand as operand, 
CASE 
WHEN (fv.FValue is null or fv.FValue = '''') 
THEN 1 
ELSE 0
END as isEmptyDate
from dbo.FiltersList fl 
inner join dbo.FilterValue fv on fv.FilterId = fl.idr 
inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId 
inner join dbo.FilterOperationsList fol on fol.idr = fv.OperationId and fol.ColumnTypeFilt=0 
where fl.idr = {0} and fcl.ColumnName = ''{1}'' and NOT fv.OperationId IS NULL')
go
If not exists (select 1 from dbo.FilterForColumnTypeList where TypeId = 3)
INSERT INTO FilterForColumnTypeList (TypeId, TypeName, DataQuery) VALUES(3, 'числовой', 'select ROW_NUMBER() OVER(ORDER BY fv.FValue ASC) as id, fv.FValue as value, fol.Operand as operand, 
CASE 
WHEN (fv.FValue is null or fv.FValue = '''') 
THEN 1 
ELSE 0
END as isEmptyDate
from dbo.FiltersList fl 
inner join dbo.FilterValue fv on fv.FilterId = fl.idr 
inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId 
inner join dbo.FilterOperationsList fol on fol.idr = fv.OperationId and fol.ColumnTypeFilt=0
where fl.idr = {0} and fcl.ColumnName = ''{1}'' and NOT fv.OperationId IS NULL')
go
If not exists (select 1 from dbo.FilterForColumnTypeList where TypeId = 4)
INSERT INTO FilterForColumnTypeList (TypeId, TypeName, DataQuery) VALUES(4, 'строковый', 'select ROW_NUMBER() OVER (ORDER BY fv.FValue ASC) as id, fv.FValue as value, fol.Operand as operand, 
    CASE 
    WHEN (fv.FValue is null or fv.FValue = '''') 
    THEN 1 
    ELSE 0 
    END as isEmptyDate ,
    ''check'' state
    from dbo.FiltersList fl 
    inner join dbo.FilterValue fv on fv.FilterId = fl.idr 
    inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId
    inner join dbo.FilterOperationsList fol on fol.idr = fv.OperationId 
    where fl.idr = {0} and fcl.ColumnName like ''%{1}%'' and NOT fv.OperationId IS NULL')
go
If not exists (select 1 from dbo.FilterForColumnTypeList where TypeId = 5)
INSERT INTO FilterForColumnTypeList (TypeId, TypeName, DataQuery) VALUES(5, 'время', 'select ROW_NUMBER() OVER(ORDER BY fv.FValue ASC) as id, fv.FValue as value, fol.Operand as operand,   
    CASE   WHEN (fv.FValue is null or fv.FValue = '''')   THEN 1   ELSE 0  END as isEmptyDate  
    from dbo.FiltersList fl   
    inner join dbo.FilterValue fv on fv.FilterId = fl.idr   
    inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId   
    inner join dbo.FilterOperationsList fol on fol.idr = fv.OperationId   
    where fl.idr = {0} and fcl.ColumnName = ''{1}'' and NOT fv.OperationId IS NULL')
;
If not exists (select 1 from dbo.FilterForColumnTypeList where TypeId = 6)
INSERT INTO FilterForColumnTypeList (TypeId, TypeName, DataQuery) VALUES(6, 'структура', 'select ROW_NUMBER() OVER(ORDER BY fv.FValue ASC) as id,fv.FValue as value,fol.Operand as operand,CASE WHEN (fv.FValue is null or fv.FValue = '''') THEN 1 ELSE 0 END as isEmptyDate from dbo.FiltersList fl inner join dbo.FilterValue fv on fv.FilterId = fl.idr inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId inner join dbo.FilterOperationsList fol on fol.idr = fv.OperationId where fl.idr = {0} and fcl.ColumnName = ''{1}'' and NOT fv.OperationId IS NULL')
go
If not exists (select 1 from dbo.FilterForColumnTypeList where TypeId = 7)
INSERT INTO FilterForColumnTypeList (TypeId, TypeName, DataQuery) VALUES(7, 'выбор fts', 'select vc.id, vc.value,   
 (case when FValue IS NULL then '''' else ''check'' end) as state  
from (select ROW_NUMBER() OVER(ORDER BY {0} ASC) as id, {0} as value  from {1}  group by {0}) vc  
left join (select fv.FValue from dbo.FiltersList fl  
inner join dbo.FilterValue fv on fv.FilterId = fl.idr  
inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId
where fl.idr = {2} and fcl.ColumnName = ''{0}'') as fcl on fcl.FValue = vc.value')
go
If not exists (select 1 from dbo.FilterForColumnTypeList where TypeId = 8)
INSERT INTO FilterForColumnTypeList (TypeId, TypeName, DataQuery) VALUES(8, 'строковый fts', 'select ROW_NUMBER() OVER (ORDER BY fv.FValue ASC) as id, fv.FValue as value, fol.Operand, 
CASE WHEN (fv.FValue is null or fv.FValue = '''') THEN 1 ELSE 0 END as isEmptyDate 
from dbo.FiltersList fl 
inner join dbo.FilterValue fv on fv.FilterId = fl.idr 
inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId 
inner join dbo.FilterOperationsList fol on fv.OperationId = fol.idr
where fl.idr = {0} and fcl.ColumnName like ''%{1}%''')
go
If not exists (select 1 from dbo.FilterForColumnTypeList where TypeId = 9)
INSERT INTO FilterForColumnTypeList (TypeId, TypeName, DataQuery) VALUES(9, 'структура fts', 'select ROW_NUMBER() OVER(ORDER BY fv.FValue ASC) as id,fv.FValue as value,fol.Operand as operand,CASE WHEN (fv.FValue is null or fv.FValue = '''') THEN 1 ELSE 0 END as isEmptyDate 
    from dbo.FiltersList fl inner join dbo.FilterValue fv on fv.FilterId = fl.idr i
    nner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId 
    inner join dbo.FilterOperationsList fol on fol.idr = fv.OperationId 
    where fl.idr = {0} and fcl.ColumnName = ''{1}'' and NOT fv.OperationId IS NULL')
go
If not exists (select 1 from dbo.FilterForColumnTypeList where TypeId = 10)
INSERT INTO FilterForColumnTypeList (TypeId, TypeName, DataQuery) VALUES(10, 'Логический', '')
go
If not exists (select 1 from dbo.FilterForColumnTypeList where TypeId = 11)
INSERT INTO FilterForColumnTypeList (TypeId, TypeName, DataQuery) VALUES(11, 'Выбор по правилам', NULL)
go

--FilterTablesList
if not exists (select 1 from dbo.[FilterTablesList] where TableName = 'Vks_Booking')
insert into dbo.[FilterTablesList]([TableName],[DBTable]) 
Values('Vks_Booking', 'BookingView')
go
if not exists (select 1 from dbo.[FilterTablesList] where TableName = 'Vks_Spaces')
insert into dbo.[FilterTablesList]([TableName],[DBTable]) 
Values('Vks_Spaces', 'SpacesView')
go
if not exists (select 1 from dbo.[FilterTablesList] where TableName = 'Vks_Recordings')
insert into dbo.[FilterTablesList]([TableName],[DBTable]) 
Values('Vks_Recordings', 'RecordingsView')
go
if not exists (select 1 from dbo.[FilterTablesList] where TableName = 'Vks_VksUserProfiles')
insert into dbo.[FilterTablesList]([TableName],[DBTable]) 
Values('Vks_VksUserProfiles', 'VksUserProfilesView')
go
if not exists (select 1 from dbo.[FilterTablesList] where TableName = 'Vks_VksServers')
insert into dbo.[FilterTablesList]([TableName],[DBTable]) 
Values('Vks_VksServers', 'VksServersView')
go
if not exists (select 1 from dbo.[FilterTablesList] where TableName = 'Vks_ServersGroup')
insert into dbo.[FilterTablesList]([TableName],[DBTable]) 
Values('Vks_ServersGroup', 'ServersGroupView')
go
if not exists (select 1 from dbo.[FilterTablesList] where TableName = 'Vks_SpaceGroups')
insert into dbo.[FilterTablesList]([TableName],[DBTable]) 
Values('Vks_SpaceGroups', 'SpaceGroups')
go
if not exists (select 1 from dbo.[FilterTablesList] where TableName = 'Vks_BookingDialog')
insert into dbo.[FilterTablesList]([TableName],[DBTable]) 
Values('Vks_BookingDialog', 'BookingView')
go
if not exists (select 1 from dbo.[FilterTablesList] where TableName = 'Vks_SpaceUserRightsDialog')
insert into dbo.[FilterTablesList]([TableName],[DBTable]) 
Values('Vks_SpaceUserRightsDialog', 'SpaceUserRightsView')
go
if not exists (select 1 from dbo.[FilterTablesList] where TableName = 'Vks_RecordingVksUsersDialog')
insert into dbo.[FilterTablesList]([TableName],[DBTable]) 
Values('Vks_RecordingVksUsersDialog', 'RecordingVksUsersView')
go

--FilterColumnsList
--TableName = Vks_Booking
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking') and ColumnName = 'name')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking'), 'name', 4, NULL, 'name', NULL, NULL, 1, NULL, NULL, NULL)
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking') and ColumnName = 'description')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking'), 'description', 4, NULL, 'description', NULL, NULL, 1, NULL, NULL, NULL)
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking') and ColumnName = 'owner')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking'), 'owner', 1, 'select 
    vc.id, vc.value, (case when FValue IS NULL then '''' else ''check'' end) as state  
        from (select vu.idr as id, name+'' (''+JID+'')'' as value from dbo.vksUsers vu ) vc 
        left join (select fv.FValue from dbo.FiltersList fl 
                   inner join dbo.FilterValue fv on fv.FilterId = fl.idr 
                   inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId 
                   where fl.idr = -1 and fcl.ConditionColumn = ''owner'') 
    as fcl on fcl.FValue = vc.value', 'owner', NULL, NULL, 1, NULL, NULL, NULL)
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking') and ColumnName = 'spaceName')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking'), 'spaceName', 4, NULL, 'spaceName', NULL, NULL, 1, NULL, NULL, NULL)
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking') and ColumnName = 'spaceUri')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking'), 'spaceUri', 4, NULL, 'spaceUri', NULL, NULL, 1, NULL, NULL, NULL)
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking') and ColumnName = 'schedule')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking'), 'schedule', 1, NULL, 'schedule', NULL, NULL, 1, NULL, NULL, NULL)
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking') and ColumnName = 'type')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking'), 'type', 1, 'select 
    vc.id, vc.value, (case when FValue IS NULL then '''' else ''check'' end) as state  
        from (select idr as id, name as value from dbo.bookingtype b ) vc 
        left join (select fv.FValue from dbo.FiltersList fl 
                   inner join dbo.FilterValue fv on fv.FilterId = fl.idr 
                   inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId 
                   where fl.idr = -1 and fcl.ConditionColumn = ''type'') 
    as fcl on fcl.FValue = vc.value', 'type', NULL, NULL, 1, NULL, NULL, NULL)
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking') and ColumnName = 'currentStatus')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking'), 'currentStatus', 1, 'select 
    vc.id, vc.value, (case when FValue IS NULL then '''' else ''check'' end) as state  
        from (select cast(0 as bigint) as id, ''START'' as value 
        union select cast(1 as bigint) as id, ''STOP'' as value) vc 
        left join (select fv.FValue from dbo.FiltersList fl 
                   inner join dbo.FilterValue fv on fv.FilterId = fl.idr 
                   inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId 
                   where fl.idr = -1 and fcl.ConditionColumn = ''currentStatus'') 
    as fcl on fcl.FValue = vc.value', 'currentStatus', NULL, NULL, 1, NULL, NULL, NULL)
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking') and ColumnName = 'nextRun')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Booking'), 'nextRun', 2, NULL, 'nextRun', NULL, NULL, 1, NULL, NULL, NULL)
go

--TableName = Vks_Spaces
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Spaces') and ColumnName = 'ServersGroupsName')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Spaces'), 'ServersGroupsName', 1, 'select 
    vc.id, vc.value, (case when FValue IS NULL then '''' else ''check'' end) as state  
        from (select vu.id as id, name as value from dbo.ServersGroups vu ) vc 
        left join (select fv.FValue from dbo.FiltersList fl 
                   inner join dbo.FilterValue fv on fv.FilterId = fl.idr 
                   inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId 
                   where fl.idr = -1 and fcl.ConditionColumn = ''owner'') 
    as fcl on fcl.FValue = vc.value', 'ServersGroupsName', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Spaces') and ColumnName = 'uri')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Spaces'), 'uri', 4, NULL, 'uri', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Spaces') and ColumnName = 'name')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Spaces'), 'name', 4, NULL, 'name', NULL, NULL, 1, NULL, NULL, NULL);
go
--TableName = Vks_Recordings
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings') and ColumnName = 'name')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings'), 'name', 4, NULL, 'name', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings') and ColumnName = 'spaceUri')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings'), 'spaceUri', 4, NULL, 'spaceUri', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings') and ColumnName = 'owner')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings'), 'owner', 1, 'select 
    vc.id, vc.value, (case when FValue IS NULL then '''' else ''check'' end) as state  
        from (select vu.idr as id, name+'' (''+JID+'')'' as value from dbo.vksUsers vu ) vc 
        left join (select fv.FValue from dbo.FiltersList fl 
                   inner join dbo.FilterValue fv on fv.FilterId = fl.idr 
                   inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId 
                   where fl.idr = -1 and fcl.ConditionColumn = ''owner'') 
    as fcl on fcl.FValue = vc.value', 'owner', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings') and ColumnName = 'dateStart')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings'), 'dateStart', 2, NULL, 'dateStart', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings') and ColumnName = 'dateEnd')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings'), 'dateEnd', 2, NULL, 'dateEnd', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings') and ColumnName = 'serversGroupsName')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_Recordings'), 'serversGroupsName', 1, 'select 
    vc.id, vc.value, (case when FValue IS NULL then '''' else ''check'' end) as state  
        from (select vu.id as id, name as value from dbo.ServersGroups vu ) vc 
        left join (select fv.FValue from dbo.FiltersList fl 
                   inner join dbo.FilterValue fv on fv.FilterId = fl.idr 
                   inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId 
                   where fl.idr = -1 and fcl.ConditionColumn = ''owner'') 
    as fcl on fcl.FValue = vc.value', 'serversGroupsName', NULL, NULL, 1, NULL, NULL, NULL)
go
--TableName = Vks_VksUserProfiles
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksUserProfiles') and ColumnName = 'name')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksUserProfiles'), 'name', 4, NULL, 'name', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksUserProfiles') and ColumnName = 'description')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksUserProfiles'), 'description', 4, NULL, 'description', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksUserProfiles') and ColumnName = 'serversGroupsName')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksUserProfiles'), 'serversGroupsName', 4, NULL, 'serversGroupsName', NULL, NULL, 1, NULL, NULL, NULL);
go
--TableName = Vks_VksServers
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksServers') and ColumnName = 'name')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksServers'), 'name', 4, NULL, 'name', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksServers') and ColumnName = 'basicPath')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksServers'), 'basicPath', 4, NULL, 'basicPath', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksServers') and ColumnName = 'serversGroupsId')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_VksServers'), 'serversGroupsId', 3, NULL, 'serversGroupsId', NULL, NULL, 1, NULL, NULL, NULL);
--TableName = Vks_ServersGroup
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_ServersGroup') and ColumnName = 'name')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_ServersGroup'), 'name', 1, NULL, 'name', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_ServersGroup') and ColumnName = 'description')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_ServersGroup'), 'description', 2, NULL, 'description', NULL, NULL, 1, NULL, NULL, NULL);
go
--TableName = Vks_SpaceGroups
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_SpaceGroups') and ColumnName = 'name')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_SpaceGroups'), 'name', 4, NULL, 'name', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_SpaceGroups') and ColumnName = 'description')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_SpaceGroups'), 'description', 4, NULL, 'description', NULL, NULL, 1, NULL, NULL, NULL);
go
--TableName = Vks_BookingDialog
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_BookingDialog') and ColumnName = 'spaceId')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_BookingDialog'), 'spaceId', 3, 'spaceId', '', '', '', 1, '', '', '');
go
--TableName = Vks_RecordingVksUsersDialog
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_RecordingVksUsersDialog') and ColumnName = 'id')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_RecordingVksUsersDialog'), 'id', 3, NULL, 'id', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_RecordingVksUsersDialog') and ColumnName = 'user')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_RecordingVksUsersDialog'), 'user', 1, 
'select vc.id, vc.value, (case when FValue IS NULL then '''' else ''check'' end) as state from (select a.id, (a.UserFullName + '' ('' + coalesce(a.Email, '''') + '')'') as value from dbo.AspNetUsers a ) vc left join (select fv.FValue from dbo.FiltersList fl inner join dbo.FilterValue fv on fv.FilterId = fl.idr inner join dbo.FilterColumnsList fcl on fcl.idr = fv.ColumnId where fl.idr = -1 and fcl.ConditionColumn = ''user'') as fcl on fcl.FValue = vc.value'
, 'user', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_RecordingVksUsersDialog') and ColumnName = 'isPlay')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_RecordingVksUsersDialog'), 'isPlay', 10, NULL, 'isPlay', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_RecordingVksUsersDialog') and ColumnName = 'isDownload')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_RecordingVksUsersDialog'), 'isDownload', 10, NULL, 'isDownload', NULL, NULL, 1, NULL, NULL, NULL);
go
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_RecordingVksUsersDialog') and ColumnName = 'dateRecord')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_RecordingVksUsersDialog'), 'dateRecord', 2, NULL, 'dateRecord', NULL, NULL, 1, NULL, NULL, NULL);
go
--TableName = Vks_SpaceUserRightsDialog
if not exists (select 1 from dbo.FilterColumnsList where TableId = (select idr from dbo.FilterTablesList ftl where TableName = 'Vks_SpaceUserRightsDialog') and ColumnName = 'id')
INSERT INTO FilterColumnsList (TableId, ColumnName, FilterTypeId, DataQuery, ConditionColumn, DisplayMember, ValueMember, IsTableColumn, FilterSql, WhereColumn, Title) 
VALUES((select idr from dbo.FilterTablesList ftl where TableName = 'Vks_SpaceUserRightsDialog'), 'id', 3, NULL, 'id', NULL, NULL, 1, NULL, NULL, NULL);
go

--TableColumnSettings
--TableName = Vks_Booking
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'name')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'name', 'Наименование', 'name', 1, 170, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0)
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'description')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'description', 'Описание', 'description', 2, 145, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0)
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'owner')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'owner', 'Владелец', 'owner', 3, 164, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0)
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'spacename')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'spaceName', 'Комната', 'spacename', 4, 145, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'spaceUri')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'spaceUri', 'URI', 'spaceuri', 5, 102, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'schedule')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'schedule', 'Периодическая', 'schedule', 6, 151, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'type')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'type', 'Тип конференции', 'type', 7, 162, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'currentStatus')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'currentStatus', 'Текущий статус', 'currentStatus', 8, 155, NULL, NULL, NULL, NULL, 0, 0, 0, '', 0, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'nextRun')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'nextRun', 'Следующий запуск', 'nextrun', 9, 178, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'actions')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'actions', 'Действия', 'actions', 10, 150, NULL, NULL, NULL, 252, 1, 1, 1, 'actionButton', 0, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'id')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'id', '№', 'id', 11, 50, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0)
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Booking' and ColumnName = 'counter')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Booking', 'counter', 'Счетчик', 'counter', 12, 131, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0)
go

--TableName = Vks_Spaces
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Spaces' and ColumnName = 'ServersGroupsName')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Spaces', 'ServersGroupsName', 'Группа серверов', 'ServersGroupsName', 1, 210, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Spaces' and ColumnName = 'uri')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Spaces', 'uri', 'URI', 'uri', 2, 150, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Spaces' and ColumnName = 'name')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Spaces', 'name', 'Наименование', 'name', 3, 266, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Spaces' and ColumnName = 'actions')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Spaces', 'actions', 'Действия', 'actions', 4, 90, NULL, NULL, NULL, NULL, 1, 0, 0, 'actionButton', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Spaces' and ColumnName = 'id')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Spaces', 'id', '№', 'id', 5, 50, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go

--TableName = Vks_Recordings
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Recordings' and ColumnName = 'name')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Recordings', 'name', 'Наименование', 'name', 1, 183, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Recordings' and ColumnName = 'spaceUri')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Recordings', 'spaceUri', 'URI', 'spaceUri', 2, 136, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Recordings' and ColumnName = 'owner')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Recordings', 'owner', 'Владелец', 'owner', 3, 154, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Recordings' and ColumnName = 'dateStart')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Recordings', 'dateStart', 'Дата начала', 'dateStart', 4, 165, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Recordings' and ColumnName = 'dateEnd')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Recordings', 'dateEnd', 'Дата завершения', 'dateEnd', 5, 174, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Recordings' and ColumnName = 'duration')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Recordings', 'duration', 'Длительность', 'duration', 6, 142, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Recordings' and ColumnName = 'serversGroupsName')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Recordings', 'serversGroupsName', 'Группа серверов', 'serversGroupsName', 7, 200, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_Recordings' and ColumnName = 'actions')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_Recordings', 'actions', 'Действия', 'actions', 8, 110, NULL, NULL, NULL, NULL, 1, 1, 1, 'actionButton', 1, 0);
go

--TableName = Vks_VksUserProfiles
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_VksUserProfiles' and ColumnName = 'name')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_VksUserProfiles', 'name', 'Наименование', 'name', 1, 153, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_VksUserProfiles' and ColumnName = 'description')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_VksUserProfiles', 'description', 'Описание', 'description', 2, 129, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_VksUserProfiles' and ColumnName = 'serversGroupsName')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_VksUserProfiles', 'serversGroupsName', 'Группа серверов', 'serversGroupsName', 3, 166, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_VksUserProfiles' and ColumnName = 'actions')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_VksUserProfiles', 'actions', 'Действия', 'actions', 4, 80, NULL, NULL, NULL, NULL, 1, 0, 0, 'actionButton', 1, 0);
go

--TableName = Vks_VksServers
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_VksServers' and ColumnName = 'name')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_VksServers', 'name', 'Наименование', 'name', 1, 180, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_VksServers' and ColumnName = 'basicPath')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_VksServers', 'basicPath', 'Базовый путь', 'basicPath', 2, 149, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_VksServers' and ColumnName = 'actions')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_VksServers', 'actions', 'Действия', 'actions', 3, 80, NULL, NULL, NULL, NULL, 1, 0, 0, 'actionButton', 1, 0);
go

--TableName = Vks_ServersGroup
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_ServersGroup' and ColumnName = 'name')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_ServersGroup', 'name', 'Наименование', 'name', 1, 161, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_ServersGroup' and ColumnName = 'description')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_ServersGroup', 'description', 'Описание', 'description', 2, 134, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_ServersGroup' and ColumnName = 'actions')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_ServersGroup', 'actions', 'Действия', 'actions', 3, 80, NULL, NULL, NULL, NULL, 1, 0, 0, 'actionButton', 1, 0);
go
--TableName = Vks_SpaceGroups
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_SpaceGroups' and ColumnName = 'name')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_SpaceGroups', 'name', 'Наименование', 'name', 1, 161, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_SpaceGroups' and ColumnName = 'description')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_SpaceGroups', 'description', 'Описание', 'description', 2, 134, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_SpaceGroups' and ColumnName = 'actions')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_SpaceGroups', 'actions', 'Действия', 'actions', 3, 80, NULL, NULL, NULL, NULL, 1, 0, 0, 'actionButton', 1, 0);
go
--TableName = Vks_BookingDialog
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_BookingDialog' and ColumnName = 'name')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_BookingDialog', 'name', 'Наименование', 'name', 1, 161, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_BookingDialog' and ColumnName = 'description')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_BookingDialog', 'description', 'Описание', 'description', 2, 147, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_BookingDialog' and ColumnName = 'currentStatus')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_BookingDialog', 'currentStatus', 'Текущий статус', 'currentStatus', 3, 124, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_BookingDialog' and ColumnName = 'nextRun')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_BookingDialog', 'nextRun', 'Следующий запуск', 'nextRun', 4, 181, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
--TableName = Vks_SpaceUserRightsDialog
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_SpaceUserRightsDialog' and ColumnName = 'vksUser')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_SpaceUserRightsDialog', 'vksUser', 'Участник', 'vksUser', 1, 161, NULL, NULL, NULL, NULL, 1, 0, 0, '', 0, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_SpaceUserRightsDialog' and ColumnName = 'callLegProfileGuid')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_SpaceUserRightsDialog', 'callLegProfileGuid', 'Профайл настроек вызова', 'callLegProfileGuid', 2, 180, NULL, NULL, NULL, NULL, 1, 0, 0, '', 0, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_SpaceUserRightsDialog' and ColumnName = 'rights')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_SpaceUserRightsDialog', 'rights', 'Права', 'rights', 3, 720, NULL, NULL, NULL, NULL, 1, 0, 0, '', 0, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_SpaceUserRightsDialog' and ColumnName = 'actions')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_SpaceUserRightsDialog', 'actions', 'Действия', 'actions', 4, 80, NULL, NULL, NULL, NULL, 1, 0, 0, 'actionButton', 0, 0);
go
--TableName = Vks_RecordingVksUsersDialog
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_RecordingVksUsersDialog' and ColumnName = 'user')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_RecordingVksUsersDialog', 'user', 'Пользователь', 'user', 1, 440, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_RecordingVksUsersDialog' and ColumnName = 'isPlay')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_RecordingVksUsersDialog', 'isPlay', 'Просмотр', 'isPlay', 2, 100, NULL, NULL, NULL, NULL, 0, 0, 0, '', 0, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_RecordingVksUsersDialog' and ColumnName = 'isDownload')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_RecordingVksUsersDialog', 'isDownload', 'Загрузка', 'isDownload', 3, 100, NULL, NULL, NULL, NULL, 0, 0, 0, '', 0, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_RecordingVksUsersDialog' and ColumnName = 'dateRecord')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_RecordingVksUsersDialog', 'dateRecord', 'Дата выдачи прав', 'dateRecord', 4, 194, NULL, NULL, NULL, NULL, 0, 0, 0, '', 1, 0);
go
if not exists(select 1 from dbo.TableColumnSettings where TableName = 'Vks_RecordingVksUsersDialog' and ColumnName = 'actions')
INSERT INTO TableColumnSettings (TableName, ColumnName, Title, Value, [order], MinWidth, Wrap, Template, ClassName, RoleId, NoSortable, NoResizable, ActionButton, CellRenderer, CellsWithoutHint, VisibleCheckBox) 
VALUES('Vks_RecordingVksUsersDialog', 'actions', 'Действия', 'actions', 5, 80, NULL, NULL, NULL, NULL, 1, 0, 0, 'actionButton', 1, 0);
go

--ConnectionType
if not exists(select 1 from dbo.ConnectionType where privatename = 'AUTO_CONNECTION')
INSERT INTO dbo.ConnectionType (id, name, privatename) 
VALUES(1, 'Автоподключение', 'AUTO_CONNECTION');
go
if not exists(select 1 from dbo.ConnectionType where privatename = 'MANUAL_CONNECTION')
INSERT INTO dbo.ConnectionType (id, name, privatename) 
VALUES(2, 'Ручное подключение', 'MANUAL_CONNECTION');
go

--Products
set identity_insert dbo.Products on 
go
if not exists (select 1 from dbo.Products where idr = 1)
insert into dbo.Products (idr, name) values (1, 'MMS')
go
set identity_insert dbo.Products off 
go

--LogsType
if not exists (select 1 from dbo.LogsType where PrivateName = 'SYSTEM')
insert into dbo.LogsType (name, PrivateName) values ('Система', 'SYSTEM')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'LOG')
insert into dbo.LogsType (name, PrivateName) values ('Журнал', 'LOG')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'USERS')
insert into dbo.LogsType (name, PrivateName) values ('Пользователи', 'USERS')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'ROLES')
insert into dbo.LogsType (name, PrivateName) values ('Роли', 'ROLES')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'BOOKING')
insert into dbo.LogsType (name, PrivateName) values ('Бронирования', 'BOOKING')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'ROOMS')
insert into dbo.LogsType (name, PrivateName) values ('Комнаты', 'ROOMS')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'RECORDS')
insert into dbo.LogsType (name, PrivateName) values ('Записи', 'RECORDS')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'USERPROFILES')
insert into dbo.LogsType (name, PrivateName) values ('Профили пользователей', 'USERPROFILES')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'GROUPSOFROOMS')
insert into dbo.LogsType (name, PrivateName) values ('Группы комнат', 'GROUPSOFROOMS')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'RECORDSTORES')
insert into dbo.LogsType (name, PrivateName) values ('Хранилища записей', 'RECORDSTORES')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'EXCHANGE')
insert into dbo.LogsType (name, PrivateName) values ('Exchange', 'EXCHANGE')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'SERVERS')
insert into dbo.LogsType (name, PrivateName) values ('Сервера', 'SERVERS')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'GROUPS')
insert into dbo.LogsType (name, PrivateName) values ('Группы', 'GROUPS')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'REPORTS')
insert into dbo.LogsType (name, PrivateName) values ('Отчеты', 'REPORTS')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'DISTRIBUTIONOFREPORTS')
insert into dbo.LogsType (name, PrivateName) values ('Рассылка отчетов', 'DISTRIBUTIONOFREPORTS')
go
if not exists (select 1 from dbo.LogsType where PrivateName = 'SERVICES')
insert into dbo.LogsType (name, PrivateName) values ('Сервисы', 'SERVICES')
go

--pinpolitics
set identity_insert dbo.pinpolitics on 
go
if not exists (select 1 from dbo.pinpolitics where privatename = 'DONOTCHANGE')
insert into dbo.pinpolitics (idr, name, privatename) values (1, 'Не менять', 'DONOTCHANGE')
go
if not exists (select 1 from dbo.pinpolitics where privatename = 'ENDOFSERIES')
insert into dbo.pinpolitics (idr, name, privatename) values (2, 'По завершению серии', 'ENDOFSERIES')
go
if not exists (select 1 from dbo.pinpolitics where privatename = 'SCHEDULED')
insert into dbo.pinpolitics (idr, name, privatename) values (3, 'По расписанию', 'SCHEDULED')
go
set identity_insert dbo.pinpolitics off 
go

--bookingtype
if not exists (select 1 from dbo.bookingtype where privatename = 'ONETIME')
insert into dbo.bookingtype (idr, name, privatename) values (1, 'Разовая', 'ONETIME')
go
if not exists (select 1 from dbo.bookingtype where privatename = 'PERIODIC')
insert into dbo.bookingtype (idr, name, privatename) values (2, 'Периодическая', 'PERIODIC')
go
if not exists (select 1 from dbo.bookingtype where privatename = 'CONSTANT')
insert into dbo.bookingtype (idr, name, privatename) values (3, 'Постоянная', 'CONSTANT')
go

--timezone
if not exists(select 1 from dbo.timezone where privatename = '(UTC-12:00)InternationalDateLineWest')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (1, '(UTC-12:00) International Date Line West', '(UTC-12:00)InternationalDateLineWest', -720, 'Dateline Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-11:00)CoordinatedUniversalTime-11')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (2, '(UTC-11:00) Coordinated Universal Time-11', '(UTC-11:00)CoordinatedUniversalTime-11', -660, 'UTC-11')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-10:00)AleutianIslands')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (3, '(UTC-10:00) Aleutian Islands', '(UTC-10:00)AleutianIslands', -600, 'Aleutian Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-10:00)Hawaii')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (4, '(UTC-10:00) Hawaii', '(UTC-10:00)Hawaii', -600, 'Hawaiian Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-09:30)MarquesasIslands')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (5, '(UTC-09:30) Marquesas Islands', '(UTC-09:30)MarquesasIslands', -570, 'Marquesas Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-09:00)Alaska')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (6, '(UTC-09:00) Alaska', '(UTC-09:00)Alaska', -540, 'Alaskan Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-09:00)CoordinatedUniversalTime-09')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (7, '(UTC-09:00) Coordinated Universal Time-09', '(UTC-09:00)CoordinatedUniversalTime-09', -540, 'UTC-09')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-08:00)BajaCalifornia')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (8, '(UTC-08:00) Baja California', '(UTC-08:00)BajaCalifornia', -480, 'Pacific Standard Time (Mexico)')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-08:00)CoordinatedUniversalTime-08')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (9, '(UTC-08:00) Coordinated Universal Time-08', '(UTC-08:00)CoordinatedUniversalTime-08', -480, 'UTC-08')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-08:00)PacificTime(US&Canada)')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (10, '(UTC-08:00) Pacific Time (US & Canada)', '(UTC-08:00)PacificTime(US&Canada)', -480, 'Pacific Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-07:00)Arizona')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (11, '(UTC-07:00) Arizona', '(UTC-07:00)Arizona', -420, 'US Mountain Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-07:00)Chihuahua,LaPaz,Mazatlan')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (12, '(UTC-07:00) Chihuahua, La Paz, Mazatlan', '(UTC-07:00)Chihuahua,LaPaz,Mazatlan', -420, 'Mountain Standard Time (Mexico)')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-07:00)MountainTime(US&Canada)')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (13, '(UTC-07:00) Mountain Time (US & Canada)', '(UTC-07:00)MountainTime(US&Canada)', -420, 'Mountain Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-07:00)Yukon')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (14, '(UTC-07:00) Yukon', '(UTC-07:00)Yukon', -420, 'Yukon Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-06:00)CentralAmerica')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (15, '(UTC-06:00) Central America', '(UTC-06:00)CentralAmerica', -360, 'Central America Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-06:00)CentralTime(US&Canada)')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (16, '(UTC-06:00) Central Time (US & Canada)', '(UTC-06:00)CentralTime(US&Canada)', -360, 'Central Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-06:00)EasterIsland')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (17, '(UTC-06:00) Easter Island', '(UTC-06:00)EasterIsland', -360, 'Easter Island Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-06:00)Guadalajara,MexicoCity,Monterrey')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (18, '(UTC-06:00) Guadalajara, Mexico City, Monterrey', '(UTC-06:00)Guadalajara,MexicoCity,Monterrey', -360, 'Central Standard Time (Mexico)')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-06:00)Saskatchewan')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (19, '(UTC-06:00) Saskatchewan', '(UTC-06:00)Saskatchewan', -360, 'Canada Central Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-05:00)Bogota,Lima,Quito,RioBranco')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (20, '(UTC-05:00) Bogota, Lima, Quito, Rio Branco', '(UTC-05:00)Bogota,Lima,Quito,RioBranco', -300, 'SA Pacific Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-05:00)Chetumal')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (21, '(UTC-05:00) Chetumal', '(UTC-05:00)Chetumal', -300, 'Eastern Standard Time (Mexico)')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-05:00)EasternTime(US&Canada)')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (22, '(UTC-05:00) Eastern Time (US & Canada)', '(UTC-05:00)EasternTime(US&Canada)', -300, 'Eastern Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-05:00)Haiti')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (23, '(UTC-05:00) Haiti', '(UTC-05:00)Haiti', -300, 'Haiti Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-05:00)Havana')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (24, '(UTC-05:00) Havana', '(UTC-05:00)Havana', -300, 'Cuba Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-05:00)Indiana(East)')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (25, '(UTC-05:00) Indiana (East)', '(UTC-05:00)Indiana(East)', -300, 'US Eastern Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-05:00)TurksandCaicos')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (26, '(UTC-05:00) Turks and Caicos', '(UTC-05:00)TurksandCaicos', -300, 'Turks And Caicos Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-04:00)Asuncion')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (27, '(UTC-04:00) Asuncion', '(UTC-04:00)Asuncion', -240, 'Paraguay Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-04:00)AtlanticTime(Canada)')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (28, '(UTC-04:00) Atlantic Time (Canada)', '(UTC-04:00)AtlanticTime(Canada)', -240, 'Atlantic Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-04:00)Caracas')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (29, '(UTC-04:00) Caracas', '(UTC-04:00)Caracas', -240, 'Venezuela Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-04:00)Cuiaba')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (30, '(UTC-04:00) Cuiaba', '(UTC-04:00)Cuiaba', -240, 'Central Brazilian Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-04:00)Georgetown,LaPaz,Manaus,SanJuan')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (31, '(UTC-04:00) Georgetown, La Paz, Manaus, San Juan', '(UTC-04:00)Georgetown,LaPaz,Manaus,SanJuan', -240, 'SA Western Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-04:00)Santiago')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (32, '(UTC-04:00) Santiago', '(UTC-04:00)Santiago', -240, 'Pacific SA Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-03:30)Newfoundland')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (33, '(UTC-03:30) Newfoundland', '(UTC-03:30)Newfoundland', -210, 'Newfoundland Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-03:00)Araguaina')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (34, '(UTC-03:00) Araguaina', '(UTC-03:00)Araguaina', -180, 'Tocantins Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-03:00)Brasilia')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (35, '(UTC-03:00) Brasilia', '(UTC-03:00)Brasilia', -180, 'E. South America Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-03:00)Cayenne,Fortaleza')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (36, '(UTC-03:00) Cayenne, Fortaleza', '(UTC-03:00)Cayenne,Fortaleza', -180, 'SA Eastern Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-03:00)CityofBuenosAires')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (37, '(UTC-03:00) City of Buenos Aires', '(UTC-03:00)CityofBuenosAires', -180, 'Argentina Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-03:00)Greenland')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (38, '(UTC-03:00) Greenland', '(UTC-03:00)Greenland', -180, 'Greenland Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-03:00)Montevideo')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (39, '(UTC-03:00) Montevideo', '(UTC-03:00)Montevideo', -180, 'Montevideo Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-03:00)PuntaArenas')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (40, '(UTC-03:00) Punta Arenas', '(UTC-03:00)PuntaArenas', -180, 'Magallanes Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-03:00)SaintPierreandMiquelon')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (41, '(UTC-03:00) Saint Pierre and Miquelon', '(UTC-03:00)SaintPierreandMiquelon', -180, 'Saint Pierre Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-03:00)Salvador')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (42, '(UTC-03:00) Salvador', '(UTC-03:00)Salvador', -180, 'Bahia Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-02:00)CoordinatedUniversalTime-02')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (43, '(UTC-02:00) Coordinated Universal Time-02', '(UTC-02:00)CoordinatedUniversalTime-02', -120, 'UTC-02')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-01:00)Azores')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (44, '(UTC-01:00) Azores', '(UTC-01:00)Azores', -60, 'Azores Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC-01:00)CaboVerdeIs.')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (45, '(UTC-01:00) Cabo Verde Is.', '(UTC-01:00)CaboVerdeIs.', -60, 'Cape Verde Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+00:00)Dublin,Edinburgh,Lisbon,London')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (47, '(UTC+00:00) Dublin, Edinburgh, Lisbon, London', '(UTC+00:00)Dublin,Edinburgh,Lisbon,London', 0, 'GMT Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+00:00)Monrovia,Reykjavik')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (48, '(UTC+00:00) Monrovia, Reykjavik', '(UTC+00:00)Monrovia,Reykjavik', 0, 'Greenwich Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+00:00)SaoTome')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (49, '(UTC+00:00) Sao Tome', '(UTC+00:00)SaoTome', 0, 'Sao Tome Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+01:00)Casablanca')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (50, '(UTC+01:00) Casablanca', '(UTC+01:00)Casablanca', 60, 'Morocco Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+01:00)Amsterdam,Berlin,Bern,Rome,Stockholm,Vienna')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (51, '(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna', '(UTC+01:00)Amsterdam,Berlin,Bern,Rome,Stockholm,Vienna', 60, 'W. Europe Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+01:00)Belgrade,Bratislava,Budapest,Ljubljana,Prague')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (52, '(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague', '(UTC+01:00)Belgrade,Bratislava,Budapest,Ljubljana,Prague', 60, 'Central Europe Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+01:00)Brussels,Copenhagen,Madrid,Paris')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (53, '(UTC+01:00) Brussels, Copenhagen, Madrid, Paris', '(UTC+01:00)Brussels,Copenhagen,Madrid,Paris', 60, 'Romance Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+01:00)Sarajevo,Skopje,Warsaw,Zagreb')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (54, '(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb', '(UTC+01:00)Sarajevo,Skopje,Warsaw,Zagreb', 60, 'Central European Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+01:00)WestCentralAfrica')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (55, '(UTC+01:00) West Central Africa', '(UTC+01:00)WestCentralAfrica', 60, 'W. Central Africa Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Amman')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (56, '(UTC+02:00) Amman', '(UTC+02:00)Amman', 120, 'Jordan Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Athens,Bucharest')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (57, '(UTC+02:00) Athens, Bucharest', '(UTC+02:00)Athens,Bucharest', 120, 'GTB Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Beirut')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (58, '(UTC+02:00) Beirut', '(UTC+02:00)Beirut', 120, 'Middle East Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Cairo')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (59, '(UTC+02:00) Cairo', '(UTC+02:00)Cairo', 120, 'Egypt Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Chisinau')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (60, '(UTC+02:00) Chisinau', '(UTC+02:00)Chisinau', 120, 'E. Europe Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Damascus')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (61, '(UTC+02:00) Damascus', '(UTC+02:00)Damascus', 120, 'Syria Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Gaza,Hebron')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (62, '(UTC+02:00) Gaza, Hebron', '(UTC+02:00)Gaza,Hebron', 120, 'West Bank Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Harare,Pretoria')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (63, '(UTC+02:00) Harare, Pretoria', '(UTC+02:00)Harare,Pretoria', 120, 'South Africa Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Helsinki,Kyiv,Riga,Sofia,Tallinn,Vilnius')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (64, '(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius', '(UTC+02:00)Helsinki,Kyiv,Riga,Sofia,Tallinn,Vilnius', 120, 'FLE Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Jerusalem')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (65, '(UTC+02:00) Jerusalem', '(UTC+02:00)Jerusalem', 120, 'Israel Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Juba')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (66, '(UTC+02:00) Juba', '(UTC+02:00)Juba', 120, 'South Sudan Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Kaliningrad')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (67, '(UTC+02:00) Kaliningrad', '(UTC+02:00)Kaliningrad', 120, 'Kaliningrad Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Khartoum')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (68, '(UTC+02:00) Khartoum', '(UTC+02:00)Khartoum', 120, 'Sudan Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Tripoli')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (69, '(UTC+02:00) Tripoli', '(UTC+02:00)Tripoli', 120, 'Libya Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+02:00)Windhoek')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (70, '(UTC+02:00) Windhoek', '(UTC+02:00)Windhoek', 120, 'Namibia Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+03:00)Baghdad')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (71, '(UTC+03:00) Baghdad', '(UTC+03:00)Baghdad', 180, 'Arabic Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+03:00)Istanbul')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (72, '(UTC+03:00) Istanbul', '(UTC+03:00)Istanbul', 180, 'Turkey Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+03:00)Kuwait,Riyadh')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (73, '(UTC+03:00) Kuwait, Riyadh', '(UTC+03:00)Kuwait,Riyadh', 180, 'Arab Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+03:00)Minsk')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (74, '(UTC+03:00) Minsk', '(UTC+03:00)Minsk', 180, 'Belarus Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+03:00)Moscow,St.Petersburg')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (75, '(UTC+03:00) Moscow, St. Petersburg', '(UTC+03:00)Moscow,St.Petersburg', 180, 'Russian Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+03:00)Nairobi')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (76, '(UTC+03:00) Nairobi', '(UTC+03:00)Nairobi', 180, 'E. Africa Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+03:00)Volgograd')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (77, '(UTC+03:00) Volgograd', '(UTC+03:00)Volgograd', 180, 'Volgograd Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+03:30)Tehran')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (78, '(UTC+03:30) Tehran', '(UTC+03:30)Tehran', 210, 'Iran Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+04:00)AbuDhabi,Muscat')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (79, '(UTC+04:00) Abu Dhabi, Muscat', '(UTC+04:00)AbuDhabi,Muscat', 240, 'Arabian Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+04:00)Astrakhan,Ulyanovsk')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (80, '(UTC+04:00) Astrakhan, Ulyanovsk', '(UTC+04:00)Astrakhan,Ulyanovsk', 240, 'Astrakhan Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+04:00)Baku')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (81, '(UTC+04:00) Baku', '(UTC+04:00)Baku', 240, 'Azerbaijan Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+04:00)Izhevsk,Samara')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (82, '(UTC+04:00) Izhevsk, Samara', '(UTC+04:00)Izhevsk,Samara', 240, 'Russia Time Zone 3')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+04:00)PortLouis')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (83, '(UTC+04:00) Port Louis', '(UTC+04:00)PortLouis', 240, 'Mauritius Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+04:00)Saratov')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (84, '(UTC+04:00) Saratov', '(UTC+04:00)Saratov', 240, 'Saratov Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+04:00)Tbilisi')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (85, '(UTC+04:00) Tbilisi', '(UTC+04:00)Tbilisi', 240, 'Georgian Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+04:00)Yerevan')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (86, '(UTC+04:00) Yerevan', '(UTC+04:00)Yerevan', 240, 'Caucasus Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+04:30)Kabul')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (87, '(UTC+04:30) Kabul', '(UTC+04:30)Kabul', 270, 'Afghanistan Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+05:00)Ashgabat,Tashkent')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (88, '(UTC+05:00) Ashgabat, Tashkent', '(UTC+05:00)Ashgabat,Tashkent', 300, 'West Asia Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+05:00)Ekaterinburg')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (89, '(UTC+05:00) Ekaterinburg', '(UTC+05:00)Ekaterinburg', 300, 'Ekaterinburg Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+05:00)Islamabad,Karachi')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (90, '(UTC+05:00) Islamabad, Karachi', '(UTC+05:00)Islamabad,Karachi', 300, 'Pakistan Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+05:00)Qyzylorda')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (91, '(UTC+05:00) Qyzylorda', '(UTC+05:00)Qyzylorda', 300, 'Qyzylorda Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+05:30)Chennai,Kolkata,Mumbai,NewDelhi')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (92, '(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi', '(UTC+05:30)Chennai,Kolkata,Mumbai,NewDelhi', 330, 'India Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+05:30)SriJayawardenepura')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (93, '(UTC+05:30) Sri Jayawardenepura', '(UTC+05:30)SriJayawardenepura', 330, 'Sri Lanka Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+05:45)Kathmandu')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (94, '(UTC+05:45) Kathmandu', '(UTC+05:45)Kathmandu', 300, 'Nepal Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+06:00)Astana')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (95, '(UTC+06:00) Astana', '(UTC+06:00)Astana', 360, 'Central Asia Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+06:00)Dhaka')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (96, '(UTC+06:00) Dhaka', '(UTC+06:00)Dhaka', 360, 'Bangladesh Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+06:00)Omsk')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (97, '(UTC+06:00) Omsk', '(UTC+06:00)Omsk', 360, 'Omsk Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+06:30)Yangon(Rangoon)')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (98, '(UTC+06:30) Yangon (Rangoon)', '(UTC+06:30)Yan-on(Rangoon)', 390, 'Myanmar Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+07:00)Bangkok,Hanoi,Jakarta')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (99, '(UTC+07:00) Bangkok, Hanoi, Jakarta', '(UTC+07:00)Bangkok,Hanoi,Jakarta', 420, 'SE Asia Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+07:00)Barnaul,gorno-Altaysk')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (100, '(UTC+07:00) Barnaul, gorno-Altaysk', '(UTC+07:00)Barnaul,gorno-Altaysk', 420, 'Altai Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+07:00)Hovd')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (101, '(UTC+07:00) Hovd', '(UTC+07:00)Hovd', 420, 'W. Mongolia Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+07:00)Krasnoyarsk')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (102, '(UTC+07:00) Krasnoyarsk', '(UTC+07:00)Krasnoyarsk', 420, 'North Asia Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+07:00)Novosibirsk')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (103, '(UTC+07:00) Novosibirsk', '(UTC+07:00)Novosibirsk', 420, 'N. Central Asia Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+07:00)Tomsk')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (104, '(UTC+07:00) Tomsk', '(UTC+07:00)Tomsk', 420, 'Tomsk Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+08:00)Beijing,Chongqing,HongKong,Urumqi')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (105, '(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi', '(UTC+08:00)Beijing,Chongqing,HongKong,Urumqi', 480, 'China Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+08:00)Irkutsk')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (106, '(UTC+08:00) Irkutsk', '(UTC+08:00)Irkutsk', 480, 'North Asia East Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+08:00)KualaLumpur,Singapore')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (107, '(UTC+08:00) Kuala Lumpur, Singapore', '(UTC+08:00)KualaLumpur,Singapore', 480, 'Singapore Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+08:00)Perth')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (108, '(UTC+08:00) Perth', '(UTC+08:00)Perth', 480, 'W. Australia Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+08:00)Taipei')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (109, '(UTC+08:00) Taipei', '(UTC+08:00)Taipei', 480, 'Taipei Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+08:00)Ulaanbaatar')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (110, '(UTC+08:00) Ulaanbaatar', '(UTC+08:00)Ulaanbaatar', 480, 'Ulaanbaatar Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+08:45)Eucla')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (111, '(UTC+08:45) Eucla', '(UTC+08:45)Eucla', 480, 'Aus Central W. Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+09:00)Chita')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (112, '(UTC+09:00) Chita', '(UTC+09:00)Chita', 540, 'Transbaikal Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+09:00)Osaka,Sapporo,Tokyo')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (113, '(UTC+09:00) Osaka, Sapporo, Tokyo', '(UTC+09:00)Osaka,Sapporo,Tokyo', 540, 'Tokyo Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+09:00)Pyongyang')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (114, '(UTC+09:00) Pyongyang', '(UTC+09:00)Pyongyang', 540, 'North Korea Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+09:00)Seoul')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (115, '(UTC+09:00) Seoul', '(UTC+09:00)Seoul', 540, 'Korea Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+09:00)Yakutsk')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (116, '(UTC+09:00) Yakutsk', '(UTC+09:00)Yakutsk', 540, 'Yakutsk Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+09:30)Adelaide')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (117, '(UTC+09:30) Adelaide', '(UTC+09:30)Adelaide', 570, 'Cen. Australia Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+09:30)Darwin')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (118, '(UTC+09:30) Darwin', '(UTC+09:30)Darwin', 570, 'AUS Central Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+10:00)Brisbane')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (119, '(UTC+10:00) Brisbane', '(UTC+10:00)Brisbane', 600, 'E. Australia Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+10:00)Canberra,Melbourne,Sydney')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (120, '(UTC+10:00) Canberra, Melbourne, Sydney', '(UTC+10:00)Canberra,Melbourne,Sydney', 600, 'AUS Eastern Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+10:00)Guam,PortMoresby')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (121, '(UTC+10:00) Guam, Port Moresby', '(UTC+10:00)Guam,PortMoresby', 600, 'West Pacific Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+10:00)Hobart')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (122, '(UTC+10:00) Hobart', '(UTC+10:00)Hobart', 600, 'Tasmania Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+10:00)Vladivostok')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (123, '(UTC+10:00) Vladivostok', '(UTC+10:00)Vladivostok', 600, 'Vladivostok Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+10:30)LordHoweIsland')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (124, '(UTC+10:30) Lord Howe Island', '(UTC+10:30)LordHoweIsland', 630, 'Lord Howe Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+11:00)BougainvilleIsland')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (125, '(UTC+11:00) Bougainville Island', '(UTC+11:00)BougainvilleIsland', 660, 'Bougainville Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+11:00)Chokurdakh')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (126, '(UTC+11:00) Chokurdakh', '(UTC+11:00)Chokurdakh', 660, 'Russia Time Zone 10')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+11:00)Magadan')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (127, '(UTC+11:00) Magadan', '(UTC+11:00)Magadan', 660, 'Magadan Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+11:00)NorfolkIsland')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (128, '(UTC+11:00) Norfolk Island', '(UTC+11:00)NorfolkIsland', 660, 'Norfolk Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+11:00)Sakhalin')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (129, '(UTC+11:00) Sakhalin', '(UTC+11:00)Sakhalin', 660, 'Sakhalin Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+11:00)SolomonIs.,NewCaledonia')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (130, '(UTC+11:00) Solomon Is., New Caledonia', '(UTC+11:00)SolomonIs.,NewCaledonia', 660, 'Central Pacific Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+12:00)Anadyr,Petropavlovsk-Kamchatsky')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (131, '(UTC+12:00) Anadyr, Petropavlovsk-Kamchatsky', '(UTC+12:00)Anadyr,Petropavlovsk-Kamchatsky', 720, 'Russia Time Zone 11')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+12:00)Auckland,Wellington')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (132, '(UTC+12:00) Auckland, Wellington', '(UTC+12:00)Auckland,Wellington', 720, 'New Zealand Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+12:00)CoordinatedUniversalTime+12')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (133, '(UTC+12:00) Coordinated Universal Time+12', '(UTC+12:00)CoordinatedUniversalTime+12', 720, 'UTC+12')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+12:00)Fiji')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (134, '(UTC+12:00) Fiji', '(UTC+12:00)Fiji', 720, 'Fiji Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+12:45)ChathamIslands')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (135, '(UTC+12:45) Chatham Islands', '(UTC+12:45)ChathamIslands', 720, 'Chatham Islands Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+13:00)CoordinatedUniversalTime+13')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (136, '(UTC+13:00) Coordinated Universal Time+13', '(UTC+13:00)CoordinatedUniversalTime+13', 780, 'UTC+13')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+13:00)Nuku''alofa')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (137, '(UTC+13:00) Nuku''alofa', '(UTC+13:00)Nuku''alofa', 780, 'Tonga Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+13:00)Samoa')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (138, '(UTC+13:00) Samoa', '(UTC+13:00)Samoa', 780, 'Samoa Standard Time')
go
if not exists(select 1 from dbo.timezone where privatename = '(UTC+14:00)KiritimatiIsland')
insert into dbo.timezone (idr, name, privatename, offsetminute, standartid) values (139, '(UTC+14:00) Kiritimati Island', '(UTC+14:00)KiritimatiIsland', 840, 'Line Islands Standard Time')
go

--dbo.NotifyTransportType
set identity_insert dbo.NotifyTransportType on 
go
if not exists (select 1 from  dbo.NotifyTransportType where Id =1 )
insert into dbo.NotifyTransportType (Id, Name, PrivateName) Values (1, 'email', 'EMAIL')
go
if not exists (select 1 from  dbo.NotifyTransportType where Id =2 )
insert into dbo.NotifyTransportType (Id, Name, PrivateName) Values (2, 'sms', 'SMS')
go
if not exists (select 1 from  dbo.NotifyTransportType where Id =3 )
insert into dbo.NotifyTransportType (Id, Name, PrivateName) Values (3, 'sms и email', 'SMS_EMAIL')
go
if not exists (select 1 from  dbo.NotifyTransportType where Id =4 )
insert into dbo.NotifyTransportType (Id, Name, PrivateName) Values (4, 'Не оповещать', 'NO_NOTIFY')
go
set identity_insert dbo.NotifyTransportType off 
go

--dbo.ntfNotifyTemplate
SET IDENTITY_INSERT dbo.ntfNotifyTemplate ON
GO
if not exists (select 1 from dbo.ntfNotifyTemplate where PrivateName = 'BookingChange')
insert into dbo.ntfNotifyTemplate (idr, Name, PrivateName, Info, ServiceId, ProcName, TemplateHTML)
select 1, 'Оповещение при изменении бронирования', 'BookingChange', 'В случае, если были изменения по бронированию, будет отправлено оповещение получателям.', id, 'ntfGetNotifyBookingChange', 'template_notify_booking.html' from dbo.Services where name = 'mentolbooking'
go
if not exists (select 1 from dbo.ntfNotifyTemplate where PrivateName = 'RecordingAddDelete')
insert into dbo.ntfNotifyTemplate (idr, Name, PrivateName, Info, ServiceId, ProcName, TemplateHTML)
select 2, 'Оповещение при добавлении\удалении записи ВКС', 'RecordingAddDelete', 'В случае, добавления\удаления записи ВКС, будет отправлено оповещение владельцу.', id, 'ntfGetNotifyRecordingAddDelete', 'template_notify_recording.html' from dbo.Services where name = 'mentolbooking'
go
if not exists (select 1 from dbo.ntfNotifyTemplate where PrivateName = 'RecordingVksUsersChange')
insert into dbo.ntfNotifyTemplate (idr, Name, PrivateName, Info, ServiceId, ProcName, TemplateHTML)
select 3, 'Оповещение при назначении\удалении прав на запись ВКС', 'RecordingVksUsersChange', 'В случае, назначения\удаления прав на запись ВКС, будет отправлено оповещение пользователю.', id, 'ntfGetNotifyRecordingVksUsersChange', 'template_notify_recordingvksusers.html' from dbo.Services where name = 'mentolbooking'
go
if not exists (select 1 from dbo.ntfNotifyTemplate where PrivateName = 'RecordingNotification')
insert into dbo.ntfNotifyTemplate (idr, Name, PrivateName, Info, ServiceId, ProcName, TemplateHTML)
select 4, 'Оповещение о предстоящем удалении записи ВКС', 'RecordingNotification', 'В случае, если запись ВКС будет удалена в указанный срок (согласно настройкам хранения), будет отправлено оповещение пользователю.', id, 'ntfGetNotifyRecordingNotification', 'template_notify_recording_notification.html' from dbo.Services where name = 'mentolbooking'
go
if not exists (select 1 from dbo.ntfNotifyTemplate where PrivateName = 'BookingChangePincodeNotification')
insert into dbo.ntfNotifyTemplate (idr, Name, PrivateName, Info, ServiceId, ProcName, TemplateHTML)
select 5, 'Оповещение о предстоящей смене ПИН-кода', 'BookingChangePincodeNotification', 'В случае, если через указанные сроки будет произведена смена ПИН-кода, будет отправлено оповещение пользователю.', id, 'ntfGetNotifyBookingChangePincodeNotification', 'template_notify_booking_change_pincode_notification.html' from dbo.Services where name = 'mentolbooking'
go
if not exists (select 1 from dbo.ntfNotifyTemplate where PrivateName = 'ConferenceEnded')
insert into dbo.ntfNotifyTemplate (idr, Name, PrivateName, Info, ServiceId, ProcName, TemplateHTML)
select 6, 'Оповещение о завершенной встрече', 'ConferenceEnded', 'В случае, если встреча завершилась, будет отправлено оповещение пользователю.', id, 'ntfGetNotifyConferenceEnded', 'template_notify_conference_ended.html' from dbo.Services where name = 'mentolbooking'
go

SET IDENTITY_INSERT dbo.ntfNotifyTemplate OFF
GO

--dbo.ntfSubscription
if not exists (select 1 from dbo.ntfSubscription where TemplateId = (select idr from dbo.ntfNotifyTemplate where PrivateName = 'BookingChange') and IsActive = 1)
insert into dbo.ntfSubscription (TemplateId, Name, IsActive)
select idr, 'Бронирование комнаты', 1 from dbo.ntfNotifyTemplate where PrivateName = 'BookingChange'
go
if not exists (select 1 from dbo.ntfSubscription where TemplateId = (select idr from dbo.ntfNotifyTemplate where PrivateName = 'RecordingAddDelete') and IsActive = 1)
insert into dbo.ntfSubscription (TemplateId, Name, IsActive)
select idr, 'Добавление\удаление записи ВКС', 1 from dbo.ntfNotifyTemplate where PrivateName = 'RecordingAddDelete'
go
if not exists (select 1 from dbo.ntfSubscription where TemplateId = (select idr from dbo.ntfNotifyTemplate where PrivateName = 'RecordingVksUsersChange') and IsActive = 1)
insert into dbo.ntfSubscription (TemplateId, Name, IsActive)
select idr, 'Права на запись ВКС', 1 from dbo.ntfNotifyTemplate where PrivateName = 'RecordingVksUsersChange'
go
if not exists (select 1 from dbo.ntfSubscription where TemplateId = (select idr from dbo.ntfNotifyTemplate where PrivateName = 'RecordingNotification') and IsActive = 1)
insert into dbo.ntfSubscription (TemplateId, Name, IsActive)
select idr, 'Нотификация о предстоящем удалении записи ВКС', 1 from dbo.ntfNotifyTemplate where PrivateName = 'RecordingNotification'
go
if not exists (select 1 from dbo.ntfSubscription where TemplateId = (select idr from dbo.ntfNotifyTemplate where PrivateName = 'BookingChangePincodeNotification') and IsActive = 1)
insert into dbo.ntfSubscription (TemplateId, Name, IsActive)
select idr, 'Нотификация о предстоящей смене ПИН-кода', 1 from dbo.ntfNotifyTemplate where PrivateName = 'BookingChangePincodeNotification'
go
if not exists (select 1 from dbo.ntfSubscription where TemplateId = (select idr from dbo.ntfNotifyTemplate where PrivateName = 'ConferenceEnded') and IsActive = 1)
insert into dbo.ntfSubscription (TemplateId, Name, IsActive)
select idr, 'Нотификация о завершенной встрече', 1 from dbo.ntfNotifyTemplate where PrivateName = 'ConferenceEnded'
go

--dbo.APISMSSettings
if not exists (select 1 from dbo.APISMSSettings where idr = 1)
insert into dbo.APISMSSettings (idr, host, port, login, pswd)
select 1, NULL, 0, NULL, NULL
go

--OutlookBookingDefault
insert into dbo.OutlookBookingDefault (id, name, description, location, ownerid, datestart, timezone, duration, isusepin, schedule, spaceid, connectiontypeid, attemptscount, delay, issendnotification, issynctoexchange, openconferencebefore, isneverusepin, dateend, repeatcount, pinpoliticsid, pinschedule, pindatestart, pincode, typeid, scheduletab, pinscheduletab, laststart, pinlaststart, rtffiletemplate) VALUES(1, N'-', N'-', N'-', N'-', N'-', N'-', 60, 1, N'', N'-', N'-', 2, 60, 1, 1, 5, 0, NULL, 0, 3, N'days:Friday', NULL, N'', 1, N'1,1', N'2', NULL, NULL, N'{\rtf1\adeflang1025\ansi\ansicpg1251\uc1\adeff0\deff0\stshfdbch0\stshfloch0\stshfhich0\stshfbi0\deflang1049\deflangfe1049\themelang1049\themelangfe0\themelangcs0{\fonttbl{\f0\fbidi \froman\fcharset204\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\f34\fbidi \froman\fcharset0\fprq2{\*\panose 02040503050406030204}Cambria Math;}
{\flomajor\f31500\fbidi \froman\fcharset204\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\fdbmajor\f31501\fbidi \froman\fcharset204\fprq2{\*\panose 02020603050405020304}Times New Roman;}
{\fhimajor\f31502\fbidi \fswiss\fcharset204\fprq2{\*\panose 020f0302020204030204}Calibri Light;}{\fbimajor\f31503\fbidi \froman\fcharset204\fprq2{\*\panose 02020603050405020304}Times New Roman;}
{\flominor\f31504\fbidi \froman\fcharset204\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\fdbminor\f31505\fbidi \froman\fcharset204\fprq2{\*\panose 02020603050405020304}Times New Roman;}
{\fhiminor\f31506\fbidi \fswiss\fcharset204\fprq2{\*\panose 020f0502020204030204}Calibri;}{\fbiminor\f31507\fbidi \froman\fcharset204\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\f45\fbidi \froman\fcharset0\fprq2 Times New Roman;}
{\f43\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}{\f46\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}{\f47\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}{\f48\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}
{\f49\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}{\f50\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}{\f51\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}
{\flomajor\f31510\fbidi \froman\fcharset0\fprq2 Times New Roman;}{\flomajor\f31508\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}{\flomajor\f31511\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}
{\flomajor\f31512\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}{\flomajor\f31513\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}{\flomajor\f31514\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}
{\flomajor\f31515\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}{\flomajor\f31516\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}{\fdbmajor\f31520\fbidi \froman\fcharset0\fprq2 Times New Roman;}
{\fdbmajor\f31518\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}{\fdbmajor\f31521\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}{\fdbmajor\f31522\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}
{\fdbmajor\f31523\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}{\fdbmajor\f31524\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}{\fdbmajor\f31525\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}
{\fdbmajor\f31526\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}{\fhimajor\f31530\fbidi \fswiss\fcharset0\fprq2 Calibri Light;}{\fhimajor\f31528\fbidi \fswiss\fcharset238\fprq2 Calibri Light CE;}
{\fhimajor\f31531\fbidi \fswiss\fcharset161\fprq2 Calibri Light Greek;}{\fhimajor\f31532\fbidi \fswiss\fcharset162\fprq2 Calibri Light Tur;}{\fhimajor\f31533\fbidi \fswiss\fcharset177\fprq2 Calibri Light (Hebrew);}
{\fhimajor\f31534\fbidi \fswiss\fcharset178\fprq2 Calibri Light (Arabic);}{\fhimajor\f31535\fbidi \fswiss\fcharset186\fprq2 Calibri Light Baltic;}{\fhimajor\f31536\fbidi \fswiss\fcharset163\fprq2 Calibri Light (Vietnamese);}
{\fbimajor\f31540\fbidi \froman\fcharset0\fprq2 Times New Roman;}{\fbimajor\f31538\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}{\fbimajor\f31541\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}
{\fbimajor\f31542\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}{\fbimajor\f31543\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}{\fbimajor\f31544\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}
{\fbimajor\f31545\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}{\fbimajor\f31546\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}{\flominor\f31550\fbidi \froman\fcharset0\fprq2 Times New Roman;}
{\flominor\f31548\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}{\flominor\f31551\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}{\flominor\f31552\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}
{\flominor\f31553\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}{\flominor\f31554\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}{\flominor\f31555\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}
{\flominor\f31556\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}{\fdbminor\f31560\fbidi \froman\fcharset0\fprq2 Times New Roman;}{\fdbminor\f31558\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}
{\fdbminor\f31561\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}{\fdbminor\f31562\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}{\fdbminor\f31563\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}
{\fdbminor\f31564\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}{\fdbminor\f31565\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}{\fdbminor\f31566\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}
{\fhiminor\f31570\fbidi \fswiss\fcharset0\fprq2 Calibri;}{\fhiminor\f31568\fbidi \fswiss\fcharset238\fprq2 Calibri CE;}{\fhiminor\f31571\fbidi \fswiss\fcharset161\fprq2 Calibri Greek;}{\fhiminor\f31572\fbidi \fswiss\fcharset162\fprq2 Calibri Tur;}
{\fhiminor\f31573\fbidi \fswiss\fcharset177\fprq2 Calibri (Hebrew);}{\fhiminor\f31574\fbidi \fswiss\fcharset178\fprq2 Calibri (Arabic);}{\fhiminor\f31575\fbidi \fswiss\fcharset186\fprq2 Calibri Baltic;}
{\fhiminor\f31576\fbidi \fswiss\fcharset163\fprq2 Calibri (Vietnamese);}{\fbiminor\f31580\fbidi \froman\fcharset0\fprq2 Times New Roman;}{\fbiminor\f31578\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}
{\fbiminor\f31581\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}{\fbiminor\f31582\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}{\fbiminor\f31583\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}
{\fbiminor\f31584\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}{\fbiminor\f31585\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}{\fbiminor\f31586\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}}
{\colortbl;\red0\green0\blue0;\red0\green0\blue255;\red0\green255\blue255;\red0\green255\blue0;\red255\green0\blue255;\red255\green0\blue0;\red255\green255\blue0;\red255\green255\blue255;\red0\green0\blue128;\red0\green128\blue128;\red0\green128\blue0;
\red128\green0\blue128;\red128\green0\blue0;\red128\green128\blue0;\red128\green128\blue128;\red192\green192\blue192;\red0\green0\blue0;\red0\green0\blue0;}{\*\defchp }{\*\defpap 
\ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 }\noqfpromote {\stylesheet{\ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af0\afs24\alang1025 \ltrch\fcs0 
\fs24\lang1049\langfe1049\loch\f0\hich\af0\dbch\af31505\cgrid\langnp1049\langfenp1049 \snext0 \sqformat \spriority0 Normal;}{\*\cs10 \additive \ssemihidden \sunhideused \spriority1 Default Paragraph Font;}{\*
\ts11\tsrowd\trftsWidthB3\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\trcbpat1\trcfpat1\tblind0\tblindtype3\tsvertalt\tsbrdrt\tsbrdrl\tsbrdrb\tsbrdrr\tsbrdrdgl\tsbrdrdgr\tsbrdrh\tsbrdrv 
\ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af0\afs20\alang1025 \ltrch\fcs0 \fs20\lang1049\langfe1049\cgrid\langnp1049\langfenp1049 \snext11 \ssemihidden \sunhideused Normal Table;}{
\s15\ql \li0\ri0\sb100\sa100\sbauto1\saauto1\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af0\afs24\alang1025 \ltrch\fcs0 \fs24\lang1049\langfe1049\loch\f0\hich\af0\dbch\af31505\cgrid\langnp1049\langfenp1049 
\sbasedon0 \snext15 \spriority0 msonormal;}{\s16\ql \li0\ri0\sb100\sa100\sbauto1\saauto1\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af0\afs24\alang1025 \ltrch\fcs0 
\fs24\cf6\lang1049\langfe1049\loch\f0\hich\af0\dbch\af31505\cgrid\langnp1049\langfenp1049 \sbasedon0 \snext16 \spriority0 textalarm;}{\s17\ql \li0\ri0\sb100\sa100\sbauto1\saauto1\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 
\rtlch\fcs1 \ab\af0\afs24\alang1025 \ltrch\fcs0 \b\fs24\cf2\lang1049\langfe1049\loch\f0\hich\af0\dbch\af31505\cgrid\langnp1049\langfenp1049 \sbasedon0 \snext17 \spriority0 bookinguri;}{
\s18\ql \li0\ri0\sb100\sa100\sbauto1\saauto1\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \ab\af0\afs24\alang1025 \ltrch\fcs0 \b\fs24\lang1049\langfe1049\loch\f0\hich\af0\dbch\af31505\cgrid\langnp1049\langfenp1049 
\sbasedon0 \snext18 \spriority0 bookinguritext;}{\*\cs19 \additive \rtlch\fcs1 \af0 \ltrch\fcs0 \cf6 \sbasedon10 \spriority0 textalarm1;}{\*\cs20 \additive \rtlch\fcs1 \ab\af0 \ltrch\fcs0 \b \sbasedon10 \spriority0 bookinguritext1;}{\*\cs21 \additive 
\rtlch\fcs1 \ab\af0 \ltrch\fcs0 \b\cf2 \sbasedon10 \spriority0 bookinguri1;}}{\*\listtable{\list\listtemplateid1309841710{\listlevel\levelnfc0\levelnfcn0\leveljc0\leveljcn0\levelfollow0\levelstartat1\levelspace0\levelindent0{\leveltext
\''02\''00.;}{\levelnumbers\''01;}\rtlch\fcs1 \af0 \ltrch\fcs0 \fi-360\li720\jclisttab\tx720\lin720 }{\listlevel\levelnfc0\levelnfcn0\leveljc0\leveljcn0\levelfollow0\levelstartat1\lvltentative\levelspace0\levelindent0{\leveltext\''02\''01.;}{\levelnumbers
\''01;}\rtlch\fcs1 \af0 \ltrch\fcs0 \fi-360\li1440\jclisttab\tx1440\lin1440 }{\listlevel\levelnfc0\levelnfcn0\leveljc0\leveljcn0\levelfollow0\levelstartat1\lvltentative\levelspace0\levelindent0{\leveltext\''02\''02.;}{\levelnumbers\''01;}\rtlch\fcs1 \af0 
\ltrch\fcs0 \fi-360\li2160\jclisttab\tx2160\lin2160 }{\listlevel\levelnfc0\levelnfcn0\leveljc0\leveljcn0\levelfollow0\levelstartat1\lvltentative\levelspace0\levelindent0{\leveltext\''02\''03.;}{\levelnumbers\''01;}\rtlch\fcs1 \af0 \ltrch\fcs0 \fi-360\li2880
\jclisttab\tx2880\lin2880 }{\listlevel\levelnfc0\levelnfcn0\leveljc0\leveljcn0\levelfollow0\levelstartat1\lvltentative\levelspace0\levelindent0{\leveltext\''02\''04.;}{\levelnumbers\''01;}\rtlch\fcs1 \af0 \ltrch\fcs0 \fi-360\li3600\jclisttab\tx3600\lin3600 }
{\listlevel\levelnfc0\levelnfcn0\leveljc0\leveljcn0\levelfollow0\levelstartat1\lvltentative\levelspace0\levelindent0{\leveltext\''02\''05.;}{\levelnumbers\''01;}\rtlch\fcs1 \af0 \ltrch\fcs0 \fi-360\li4320\jclisttab\tx4320\lin4320 }{\listlevel\levelnfc0
\levelnfcn0\leveljc0\leveljcn0\levelfollow0\levelstartat1\lvltentative\levelspace0\levelindent0{\leveltext\''02\''06.;}{\levelnumbers\''01;}\rtlch\fcs1 \af0 \ltrch\fcs0 \fi-360\li5040\jclisttab\tx5040\lin5040 }{\listlevel\levelnfc0\levelnfcn0\leveljc0
\leveljcn0\levelfollow0\levelstartat1\lvltentative\levelspace0\levelindent0{\leveltext\''02\''07.;}{\levelnumbers\''01;}\rtlch\fcs1 \af0 \ltrch\fcs0 \fi-360\li5760\jclisttab\tx5760\lin5760 }{\listlevel\levelnfc0\levelnfcn0\leveljc0\leveljcn0\levelfollow0
\levelstartat1\lvltentative\levelspace0\levelindent0{\leveltext\''02\''08.;}{\levelnumbers\''01;}\rtlch\fcs1 \af0 \ltrch\fcs0 \fi-360\li6480\jclisttab\tx6480\lin6480 }{\listname ;}\listid417025813}}{\*\listoverridetable{\listoverride\listid417025813
\listoverridecount0\ls1}}{\*\rsidtbl \rsid1521638\rsid8064870}{\mmathPr\mmathFont34\mbrkBin0\mbrkBinSub0\msmallFrac0\mdispDef1\mlMargin0\mrMargin0\mdefJc1\mwrapIndent1440\mintLim0\mnaryLim1}{\info{\author user}{\operator user}
{\creatim\yr2021\mo11\dy26\hr10\min56}{\revtim\yr2021\mo11\dy26\hr10\min56}{\version2}{\edmins1}{\nofpages1}{\nofwords56}{\nofchars323}{\nofcharsws378}{\vern33}}{\*\xmlnstbl {\xmlns1 http://schemas.microsoft.com/office/word/2003/wordml}}
\paperw11906\paperh16838\margl1701\margr850\margt1134\margb1134\gutter0\ltrsect 
\deftab708\widowctrl\ftnbj\aenddoc\trackmoves0\trackformatting1\donotembedsysfont1\relyonvml0\donotembedlingdata0\grfdocevents0\validatexml1\showplaceholdtext0\ignoremixedcontent0\saveinvalidxml0\showxmlerrors1
\noxlattoyen\expshrtn\noultrlspc\dntblnsbdb\nospaceforul\formshade\horzdoc\dgmargin\dghspace180\dgvspace180\dghorigin1701\dgvorigin1134\dghshow1\dgvshow1
\jexpand\viewkind1\viewscale100\pgbrdrhead\pgbrdrfoot\splytwnine\ftnlytwnine\htmautsp\nolnhtadjtbl\useltbaln\alntblind\lytcalctblwd\lyttblrtgr\lnbrkrule\nobrkwrptbl\allowfieldendsel\nojkernpunct\rsidroot1521638
\newtblstyruls\usenormstyforlist\noindnmbrts\felnbrelev\nocxsptable\indrlsweleven\noafcnsttbl\afelev\utinl\hwelev\spltpgpar\notcvasp\notbrkcnstfrctbl\notvatxbx\krnprsnet\cachedcolbal \nouicompat \fet0{\*\wgrffmtfilter 2450}\nofeaturethrottle1
\ilfomacatclnup0\ltrpar \sectd \ltrsect\linex0\headery708\footery708\colsx708\endnhere\pgbrdropt32\sectlinegrid360\sectdefaultcl\sftnbj {\*\pnseclvl1\pnucrm\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl2\pnucltr\pnstart1\pnindent720\pnhang 
{\pntxta .}}{\*\pnseclvl3\pndec\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl4\pnlcltr\pnstart1\pnindent720\pnhang {\pntxta )}}{\*\pnseclvl5\pndec\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl6\pnlcltr\pnstart1\pnindent720\pnhang 
{\pntxtb (}{\pntxta )}}{\*\pnseclvl7\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl8\pnlcltr\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl9\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}
\pard\plain \ltrpar\ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af0\afs24\alang1025 \ltrch\fcs0 \fs24\lang1049\langfe1049\loch\af0\hich\af0\dbch\af31505\cgrid\langnp1049\langfenp1049 {\rtlch\fcs1 \af0 
\ltrch\fcs0 \dbch\af0\insrsid8064870 \line }{\rtlch\fcs1 \af0 \ltrch\fcs0 \cs19\cf6\dbch\af0\insrsid8064870 ===\''cd\''e5 \''f3\''e4\''e0\''eb\''ff\''f2\''fc \''e8 \''ed\''e5 \''e8\''e7\''ec\''e5\''ed\''ff\''f2\''fc \''ed\''e8\''f7\''e5\''e3\''ee \''e2 \''fd\''f2\''ee\''ec \''f1\''ee\''ee
\''e1\''f9\''e5\''ed\''e8\''e8===}{\rtlch\fcs1 \af0 \ltrch\fcs0 \dbch\af0\insrsid8064870 \line \line }{\rtlch\fcs1 \ab\af0 \ltrch\fcs0 \cs20\b\dbch\af0\insrsid8064870 \''cf\''ee \''c2\''e0\''f8\''e5\''ec\''f3 \''e7\''e0\''ef\''f0\''ee\''f1\''f3 \''e2\''fb\''e4\''e5\''eb\''e5\''ed 
\''ed\''ee\''ec\''e5\''f0 \''ea\''ee\''ed\''f4\''e5\''f0\''e5\''ed\''f6\''e8\''e8:}{\rtlch\fcs1 \af0 \ltrch\fcs0 \dbch\af0\insrsid8064870  }{\rtlch\fcs1 \ab\af0 \ltrch\fcs0 \cs21\b\cf2\dbch\af0\insrsid8064870 \{space_uri\}}{\rtlch\fcs1 \af0 \ltrch\fcs0 
\dbch\af0\insrsid8064870 \line \line }{\rtlch\fcs1 \af0 \ltrch\fcs0 \cs19\cf6\dbch\af0\insrsid8064870 \''c2\''cd\''c8\''cc\''c0\''cd\''c8\''c5! \''ca\''ee\''ed\''f4\''e5\''f0\''e5\''ed\''f6\''e8\''ff \''e7\''e0\''f9\''e8\''f9\''e5\''ed\''e0 \''cf\''c8\''cd-\''ea\''ee\''e4\''ee\''ec.}{
\rtlch\fcs1 \af0 \ltrch\fcs0 \dbch\af0\insrsid8064870 \line \''cf\''c8\''cd-\''ea\''ee\''e4 \''e2\''f1\''f2\''f0\''e5\''f7\''e8: \{booking_pincode\}\line \line }{\rtlch\fcs1 \af0 \ltrch\fcs0 \cs19\cf6\dbch\af0\insrsid8064870 \''ca\''ee\''ed\''f4\''e5\''f0\''e5\''ed\''f6\''e8
\''ff \''f1\''f2\''e0\''ed\''e5\''f2 \''e4\''ee\''f1\''f2\''f3\''ef\''ed\''e0 \''e7\''e0 \{booking_openconferencebefore\} \''ec\''e8\''ed\''f3\''f2 \''e4\''ee \''ed\''e0\''e7\''ed\''e0\''f7\''e5\''ed\''ed\''ee\''e3\''ee \''e2\''f0\''e5\''ec\''e5\''ed\''e8.}{\rtlch\fcs1 \af0 \ltrch\fcs0 
\dbch\af0\insrsid8064870 
\par {\listtext\pard\plain\ltrpar \rtlch\fcs1 \af0 \ltrch\fcs0 \hich\af0\dbch\af0\loch\f0 1.\tab}}\pard \ltrpar\ql \fi-360\li720\ri0\sb100\sa100\sbauto1\saauto1\widctlpar\jclisttab\tx720\wrapdefault\aspalpha\aspnum\faauto\ls1\adjustright\rin0\lin720\itap0 {
\rtlch\fcs1 \af0 \ltrch\fcs0 \dbch\af0\insrsid8064870 \''cf\''ee\''e4\''ea\''eb\''fe\''f7\''e5\''ed\''e8\''e5 \''f1 \''e2\''ed\''f3\''f2\''f0\''e5\''ed\''ed\''e5\''e3\''ee \''f1\''f2\''e0\''f6\''e8\''ee\''ed\''e0\''f0\''ed\''ee\''e3\''ee \''f2\''e5\''eb\''e5\''f4\''ee\''ed\''e0 \''ef\''ee \''ef\''f0
\''ff\''ec\''ee\''ec\''f3 \''e2\''ed\''f3\''f2\''f0\''e5\''ed\''ed\''e5\''ec\''f3 \''ed\''ee\''ec\''e5\''f0\''f3:\line }{\rtlch\fcs1 \ab\af0 \ltrch\fcs0 \cs21\b\cf2\dbch\af0\insrsid8064870 \{space_uri\}}{\rtlch\fcs1 \af0 \ltrch\fcs0 \dbch\af0\insrsid8064870 
\par }\pard \ltrpar\ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 {\rtlch\fcs1 \af0 \ltrch\fcs0 \dbch\af0\insrsid8064870 
\par }{\*\themedata 504b030414000600080000002100e9de0fbfff0000001c020000130000005b436f6e74656e745f54797065735d2e786d6cac91cb4ec3301045f748fc83e52d4a
9cb2400825e982c78ec7a27cc0c8992416c9d8b2a755fbf74cd25442a820166c2cd933f79e3be372bd1f07b5c3989ca74aaff2422b24eb1b475da5df374fd9ad
5689811a183c61a50f98f4babebc2837878049899a52a57be670674cb23d8e90721f90a4d2fa3802cb35762680fd800ecd7551dc18eb899138e3c943d7e503b6
b01d583deee5f99824e290b4ba3f364eac4a430883b3c092d4eca8f946c916422ecab927f52ea42b89a1cd59c254f919b0e85e6535d135a8de20f20b8c12c3b0
0c895fcf6720192de6bf3b9e89ecdbd6596cbcdd8eb28e7c365ecc4ec1ff1460f53fe813d3cc7f5b7f020000ffff0300504b030414000600080000002100a5d6
a7e7c0000000360100000b0000005f72656c732f2e72656c73848fcf6ac3300c87ef85bd83d17d51d2c31825762fa590432fa37d00e1287f68221bdb1bebdb4f
c7060abb0884a4eff7a93dfeae8bf9e194e720169aaa06c3e2433fcb68e1763dbf7f82c985a4a725085b787086a37bdbb55fbc50d1a33ccd311ba548b6309512
0f88d94fbc52ae4264d1c910d24a45db3462247fa791715fd71f989e19e0364cd3f51652d73760ae8fa8c9ffb3c330cc9e4fc17faf2ce545046e37944c69e462
a1a82fe353bd90a865aad41ed0b5b8f9d6fd010000ffff0300504b0304140006000800000021006b799616830000008a0000001c0000007468656d652f746865
6d652f7468656d654d616e616765722e786d6c0ccc4d0ac3201040e17da17790d93763bb284562b2cbaebbf600439c1a41c7a0d29fdbd7e5e38337cedf14d59b
4b0d592c9c070d8a65cd2e88b7f07c2ca71ba8da481cc52c6ce1c715e6e97818c9b48d13df49c873517d23d59085adb5dd20d6b52bd521ef2cdd5eb9246a3d8b
4757e8d3f729e245eb2b260a0238fd010000ffff0300504b030414000600080000002100507a8df2be070000fc200000160000007468656d652f7468656d652f
7468656d65312e786d6cec595f8f1bb7117f2f90efb0d87759ff76f5e760399056922ff69d6d58b2833cf2246a973eee5258527716020381f39497040192a00f
0dd0f6a50f41d103ea2241d1a05fe1f2190cd868d30fd12177a52525cabe3bb88051dc1d70b74bfd66f8e3cc706644defcf0694c9d139c72c2928e5bbd51711d
9c4cd8942461c77d341e965aaec3054aa688b20477dc25e6ee87b73ef8cd4db427221c6307e413be873a6e24c47caf5ce6131846fc069be3043e9bb13446025e
d3b03c4dd129e88d69b956a934ca312289eb242806b5e77f3cffdbf93fcecf9cfbb3199960f7d64aff80c22489e0726042d391d48e57427ff8e5f9f9d9f9cfe7
2fcecf7ef90c9e7f86ff5f29d9e971554af0250f68ea9c20da7161ea293b1de3a7c27528e2023ee8b815f5e3966fdd2ca3bd5c888a1db29adc50fde472b9c0f4
b8a6e64cc3a3f5a49ee77b8dee5abf0250b18d1b34078d4163ad4f01d064022bcfb8983a9bb5c0cbb11a287bb4e8ee37fbf5aa81d7f4d7b738777df96be01528
d3ef6de187c300ac68e01528c3fb5b78bfd7eef54dfd0a94e11b5bf866a5dbf79a867e058a28498eb7d015bf510f56ab5d43668cee5be16ddf1b366bb9f20205
d1b08e3639c58c25e2a2b117a3272c1d828014a44890c411cb399ea109047a8028394a897340c20802718e12c661b852ab0c2b75f82b7f3df5a43c8cf630d2a4
254f60c6b786243f874f5232171df70e687535c8ab9f7e7af9fcc5cbe73fbefcfcf397cfff92cfad541972fb280975b95ffff4f57fbeffccf9f75f7fffeb37df
66536fe2b98e7ffde72f5efffd9f6f520f2b2e4cf1eabbb3d72fce5efdf6cb7ffdf08d457b3745473a7c4c62cc9d7bf8d479c86258a0853f3e4a2f27318e10d1
25ba49c85182e42c16fd031119e87b4b449105d7c3a61d1fa7907a6cc0db8b2706e151942e04b168bc1bc506f09031da63a9d50a77e55c9a99c78b24b44f9e2e
74dc43844e6c73072831bc3c58cc2107139bca20c206cd071425028538c1c2919fb1638c2dabfb8410c3ae87649232ce66c2f984383d44ac26199323239a0aa1
7d12835f963682e06fc336878f9d1ea3b655f7f1898984bd81a885fc1853c38cb7d142a0d8a6728c62aa1bfc0089c84672b44c273a6ec005783ac494398329e6
dc26733f85f56a4ebf0b69c6eef643ba8c4d642ac8b14de701624c47f6d97110a1786ec38e4812e9d88ff8318428721e3061831f327387c877f0034a76bafb31
c186bbdf9e0d1e4186d5291501223f59a4165fdec6cc88dfd192ce10b6a59a6e1a1b29b69b126b74f416a111da071853748aa6183b8f3eb230e8b1b961f382f4
9d08b2ca3eb605d61d64c6aa7c4f30c78e6a76b6f3e401e146c88e70c876f0395c6e249e254a6294eed27c0fbcaedb7c00a52eb605c07d3a39d681f708b48910
2f56a3dce7a0430bee9d5a1f44c82860f29ddbe375991afebbc81e837df9c4a071817d0932f8d23290d8759937da668ca83141113063045d862ddd8288e1fe42
44165725b6b0cacdcc4d5bb801ba25a3e98949f2d60e68a3f7f1ff77bd0f7418af7ef7bd65b3bd9b7ec7aed8485697ec74762593fd8dfe66176eb3ab09583a25
ef7f53d3478be401863ab29db1ae7b9aeb9ec6fdbfef6976ede7eb4e6657bf71ddc9b8d0615c7732f9e1cabbe9648ae605fa1a79e0911dfca863a0f8c2a74033
42e9482c293ee0ea2088c3f79be91006a51e75488ad7a784f3081e65d983090d5c982225e3a44c7c4c44348ad01c4e8baaae5412f25c75c89d39e37088a486ad
ba259e2ee24336cd0e43ab5579f099555a8e44315ef1d7e37070253274a3591cf0add52bb6a13a985d1190b29721a14d6692a85b48345783d248ea18188c6621
a156f64e58b42d2c5a52fdca555b2c80dada2bf005dc81afed1dd7f7400484e07c0e9af5a9f453e6ea95779533dfa5a77719d3880068b857115078ba2db9ee5c
9e5c5d166a17f0b441420b379384b28c6af878045f8bf3e894a317a171595fb70b971af4a429d47c105a058d66eb4d2caeea6b90dbcc0d34d133054d9cd38edb
a8fb10321334efb833384486c7780eb1c3e577304443b8ac998834dbf057c92cf3948b3ee25166709574b26c101381538792b8e3cae5afdd4013954314b76a0d
12c27b4bae0d69e57d23074e379d8c67333c11badbb51169e9ec15327c962bac9f2af1ab83a5245b80bb47d1f4d439a28bf4218210f39b5569c029e1709750cd
ac39257059b64e6445fc6d14a63cedeab7552a86b27144e711ca2b8a9ecc33b84ae56b3aea6d6d03ed2d5f33185433495e088f42596075a31ad5745d35320e3b
abeedb85a4e5b4a459d44c23abc8aa69cf62c60cab32b061cbab15798dd5cac490d3f40a9fa5eecd94db5ee5ba8d3e615d25c0e06bfb59aaee050a8246ad98cc
a026196fa76199b3f351b376ac16f8166a1729125ad66facd46ed86d5d23acd3c1e0952a3fc86d462d0ccd567da6b2b4ba68d72fc2d9d113481e7de87a175470
e54ab8d64e11344423d593646903b6c853916f0d78721629e9b89f56fcae17d4fca05469f9839257f72aa596dfad97babe5faf0efc6aa5dfab3d83c222a2b8ea
6797fc43b8d0a0cbfcaa5f8d6f5df7c7ab3b9b1b13169799bac52f2be2eababf5a33aefbb35b7e672c2ff35d8740d2f9b4511bb6ebed5ea3d4ae778725afdf6b
95da41a357ea3782667fd80ffc567bf8cc754e14d8ebd603af3168951ad52028798d8aa4df6a979a5eadd6f59addd6c0eb3ecbdb185879963e725b807915af5b
ff050000ffff0300504b0304140006000800000021000dd1909fb60000001b010000270000007468656d652f7468656d652f5f72656c732f7468656d654d616e
616765722e786d6c2e72656c73848f4d0ac2301484f78277086f6fd3ba109126dd88d0add40384e4350d363f2451eced0dae2c082e8761be9969bb979dc91363
32de3168aa1a083ae995719ac16db8ec8e4052164e89d93b64b060828e6f37ed1567914b284d262452282e3198720e274a939cd08a54f980ae38a38f56e422a3
a641c8bbd048f7757da0f19b017cc524bd62107bd5001996509affb3fd381a89672f1f165dfe514173d9850528a2c6cce0239baa4c04ca5bbabac4df000000ff
ff0300504b01022d0014000600080000002100e9de0fbfff0000001c0200001300000000000000000000000000000000005b436f6e74656e745f54797065735d
2e786d6c504b01022d0014000600080000002100a5d6a7e7c0000000360100000b00000000000000000000000000300100005f72656c732f2e72656c73504b01
022d00140006000800000021006b799616830000008a0000001c00000000000000000000000000190200007468656d652f7468656d652f7468656d654d616e61
6765722e786d6c504b01022d0014000600080000002100507a8df2be070000fc2000001600000000000000000000000000d60200007468656d652f7468656d65
2f7468656d65312e786d6c504b01022d00140006000800000021000dd1909fb60000001b0100002700000000000000000000000000c80a00007468656d652f7468656d652f5f72656c732f7468656d654d616e616765722e786d6c2e72656c73504b050600000000050005005d010000c30b00000000}
{\*\colorschememapping 3c3f786d6c2076657273696f6e3d22312e302220656e636f64696e673d225554462d3822207374616e64616c6f6e653d22796573223f3e0d0a3c613a636c724d
617020786d6c6e733a613d22687474703a2f2f736368656d61732e6f70656e786d6c666f726d6174732e6f72672f64726177696e676d6c2f323030362f6d6169
6e22206267313d226c743122207478313d22646b3122206267323d226c743222207478323d22646b322220616363656e74313d22616363656e74312220616363
656e74323d22616363656e74322220616363656e74333d22616363656e74332220616363656e74343d22616363656e74342220616363656e74353d22616363656e74352220616363656e74363d22616363656e74362220686c696e6b3d22686c696e6b2220666f6c486c696e6b3d22666f6c486c696e6b222f3e}
{\*\latentstyles\lsdstimax376\lsdlockeddef0\lsdsemihiddendef0\lsdunhideuseddef0\lsdqformatdef0\lsdprioritydef99{\lsdlockedexcept \lsdqformat1 \lsdpriority0 \lsdlocked0 Normal;\lsdqformat1 \lsdpriority9 \lsdlocked0 heading 1;
\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 2;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 3;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 4;
\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 5;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 6;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 7;
\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 8;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 9;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 1;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 5;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 6;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 7;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 8;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 9;
\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 1;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 2;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 3;
\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 4;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 5;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 6;
\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 7;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 8;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 9;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Normal Indent;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 footnote text;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 annotation text;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 header;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 footer;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index heading;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority35 \lsdlocked0 caption;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 table of figures;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 envelope address;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 envelope return;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 footnote reference;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 annotation reference;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 line number;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 page number;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 endnote reference;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 endnote text;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 table of authorities;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 macro;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 toa heading;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Bullet;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Number;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List 3;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List 5;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Bullet 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Bullet 3;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Bullet 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Bullet 5;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Number 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Number 3;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Number 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Number 5;\lsdqformat1 \lsdpriority10 \lsdlocked0 Title;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Closing;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Signature;\lsdsemihidden1 \lsdunhideused1 \lsdpriority1 \lsdlocked0 Default Paragraph Font;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text Indent;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Continue;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Continue 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Continue 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Continue 4;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Continue 5;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Message Header;\lsdqformat1 \lsdpriority11 \lsdlocked0 Subtitle;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Salutation;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Date;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text First Indent;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text First Indent 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Note Heading;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text Indent 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text Indent 3;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Block Text;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Hyperlink;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 FollowedHyperlink;\lsdqformat1 \lsdpriority22 \lsdlocked0 Strong;
\lsdqformat1 \lsdpriority20 \lsdlocked0 Emphasis;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Document Map;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Plain Text;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 E-mail Signature;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Top of Form;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Bottom of Form;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Normal (Web);\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Acronym;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Address;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Cite;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Code;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Definition;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Keyboard;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Preformatted;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Sample;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Typewriter;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Variable;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Normal Table;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 annotation subject;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 No List;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Outline List 1;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Outline List 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Outline List 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Simple 1;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Simple 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Simple 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Classic 1;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Classic 2;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Classic 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Classic 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Colorful 1;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Colorful 2;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Colorful 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Columns 1;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Columns 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Columns 3;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Columns 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Columns 5;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Grid 1;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Grid 2;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Grid 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Grid 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Grid 5;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Grid 6;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Grid 7;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Grid 8;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table List 1;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table List 2;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table List 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table List 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table List 5;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table List 6;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table List 7;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table List 8;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table 3D effects 1;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table 3D effects 2;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table 3D effects 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Contemporary;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Elegant;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Professional;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Subtle 1;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Subtle 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Web 1;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Web 2;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Web 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Balloon Text;\lsdpriority39 \lsdlocked0 Table Grid;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Table Theme;\lsdsemihidden1 \lsdlocked0 Placeholder Text;
\lsdqformat1 \lsdpriority1 \lsdlocked0 No Spacing;\lsdpriority60 \lsdlocked0 Light Shading;\lsdpriority61 \lsdlocked0 Light List;\lsdpriority62 \lsdlocked0 Light Grid;\lsdpriority63 \lsdlocked0 Medium Shading 1;\lsdpriority64 \lsdlocked0 Medium Shading 2;
\lsdpriority65 \lsdlocked0 Medium List 1;\lsdpriority66 \lsdlocked0 Medium List 2;\lsdpriority67 \lsdlocked0 Medium Grid 1;\lsdpriority68 \lsdlocked0 Medium Grid 2;\lsdpriority69 \lsdlocked0 Medium Grid 3;\lsdpriority70 \lsdlocked0 Dark List;
\lsdpriority71 \lsdlocked0 Colorful Shading;\lsdpriority72 \lsdlocked0 Colorful List;\lsdpriority73 \lsdlocked0 Colorful Grid;\lsdpriority60 \lsdlocked0 Light Shading Accent 1;\lsdpriority61 \lsdlocked0 Light List Accent 1;
\lsdpriority62 \lsdlocked0 Light Grid Accent 1;\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 1;\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 1;\lsdpriority65 \lsdlocked0 Medium List 1 Accent 1;\lsdsemihidden1 \lsdlocked0 Revision;
\lsdqformat1 \lsdpriority34 \lsdlocked0 List Paragraph;\lsdqformat1 \lsdpriority29 \lsdlocked0 Quote;\lsdqformat1 \lsdpriority30 \lsdlocked0 Intense Quote;\lsdpriority66 \lsdlocked0 Medium List 2 Accent 1;\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 1;
\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 1;\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 1;\lsdpriority70 \lsdlocked0 Dark List Accent 1;\lsdpriority71 \lsdlocked0 Colorful Shading Accent 1;\lsdpriority72 \lsdlocked0 Colorful List Accent 1;
\lsdpriority73 \lsdlocked0 Colorful Grid Accent 1;\lsdpriority60 \lsdlocked0 Light Shading Accent 2;\lsdpriority61 \lsdlocked0 Light List Accent 2;\lsdpriority62 \lsdlocked0 Light Grid Accent 2;\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 2;
\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 2;\lsdpriority65 \lsdlocked0 Medium List 1 Accent 2;\lsdpriority66 \lsdlocked0 Medium List 2 Accent 2;\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 2;\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 2;
\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 2;\lsdpriority70 \lsdlocked0 Dark List Accent 2;\lsdpriority71 \lsdlocked0 Colorful Shading Accent 2;\lsdpriority72 \lsdlocked0 Colorful List Accent 2;\lsdpriority73 \lsdlocked0 Colorful Grid Accent 2;
\lsdpriority60 \lsdlocked0 Light Shading Accent 3;\lsdpriority61 \lsdlocked0 Light List Accent 3;\lsdpriority62 \lsdlocked0 Light Grid Accent 3;\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 3;\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 3;
\lsdpriority65 \lsdlocked0 Medium List 1 Accent 3;\lsdpriority66 \lsdlocked0 Medium List 2 Accent 3;\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 3;\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 3;\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 3;
\lsdpriority70 \lsdlocked0 Dark List Accent 3;\lsdpriority71 \lsdlocked0 Colorful Shading Accent 3;\lsdpriority72 \lsdlocked0 Colorful List Accent 3;\lsdpriority73 \lsdlocked0 Colorful Grid Accent 3;\lsdpriority60 \lsdlocked0 Light Shading Accent 4;
\lsdpriority61 \lsdlocked0 Light List Accent 4;\lsdpriority62 \lsdlocked0 Light Grid Accent 4;\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 4;\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 4;\lsdpriority65 \lsdlocked0 Medium List 1 Accent 4;
\lsdpriority66 \lsdlocked0 Medium List 2 Accent 4;\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 4;\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 4;\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 4;\lsdpriority70 \lsdlocked0 Dark List Accent 4;
\lsdpriority71 \lsdlocked0 Colorful Shading Accent 4;\lsdpriority72 \lsdlocked0 Colorful List Accent 4;\lsdpriority73 \lsdlocked0 Colorful Grid Accent 4;\lsdpriority60 \lsdlocked0 Light Shading Accent 5;\lsdpriority61 \lsdlocked0 Light List Accent 5;
\lsdpriority62 \lsdlocked0 Light Grid Accent 5;\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 5;\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 5;\lsdpriority65 \lsdlocked0 Medium List 1 Accent 5;\lsdpriority66 \lsdlocked0 Medium List 2 Accent 5;
\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 5;\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 5;\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 5;\lsdpriority70 \lsdlocked0 Dark List Accent 5;\lsdpriority71 \lsdlocked0 Colorful Shading Accent 5;
\lsdpriority72 \lsdlocked0 Colorful List Accent 5;\lsdpriority73 \lsdlocked0 Colorful Grid Accent 5;\lsdpriority60 \lsdlocked0 Light Shading Accent 6;\lsdpriority61 \lsdlocked0 Light List Accent 6;\lsdpriority62 \lsdlocked0 Light Grid Accent 6;
\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 6;\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 6;\lsdpriority65 \lsdlocked0 Medium List 1 Accent 6;\lsdpriority66 \lsdlocked0 Medium List 2 Accent 6;
\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 6;\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 6;\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 6;\lsdpriority70 \lsdlocked0 Dark List Accent 6;\lsdpriority71 \lsdlocked0 Colorful Shading Accent 6;
\lsdpriority72 \lsdlocked0 Colorful List Accent 6;\lsdpriority73 \lsdlocked0 Colorful Grid Accent 6;\lsdqformat1 \lsdpriority19 \lsdlocked0 Subtle Emphasis;\lsdqformat1 \lsdpriority21 \lsdlocked0 Intense Emphasis;
\lsdqformat1 \lsdpriority31 \lsdlocked0 Subtle Reference;\lsdqformat1 \lsdpriority32 \lsdlocked0 Intense Reference;\lsdqformat1 \lsdpriority33 \lsdlocked0 Book Title;\lsdsemihidden1 \lsdunhideused1 \lsdpriority37 \lsdlocked0 Bibliography;
\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority39 \lsdlocked0 TOC Heading;\lsdpriority41 \lsdlocked0 Plain Table 1;\lsdpriority42 \lsdlocked0 Plain Table 2;\lsdpriority43 \lsdlocked0 Plain Table 3;\lsdpriority44 \lsdlocked0 Plain Table 4;
\lsdpriority45 \lsdlocked0 Plain Table 5;\lsdpriority40 \lsdlocked0 Grid Table Light;\lsdpriority46 \lsdlocked0 Grid Table 1 Light;\lsdpriority47 \lsdlocked0 Grid Table 2;\lsdpriority48 \lsdlocked0 Grid Table 3;\lsdpriority49 \lsdlocked0 Grid Table 4;
\lsdpriority50 \lsdlocked0 Grid Table 5 Dark;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful;\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful;\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 1;\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 1;
\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 1;\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 1;\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 1;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 1;
\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 1;\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 2;\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 2;\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 2;
\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 2;\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 2;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 2;\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 2;
\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 3;\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 3;\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 3;\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 3;
\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 3;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 3;\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 3;\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 4;
\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 4;\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 4;\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 4;\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 4;
\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 4;\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 4;\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 5;\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 5;
\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 5;\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 5;\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 5;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 5;
\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 5;\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 6;\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 6;\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 6;
\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 6;\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 6;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 6;\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 6;
\lsdpriority46 \lsdlocked0 List Table 1 Light;\lsdpriority47 \lsdlocked0 List Table 2;\lsdpriority48 \lsdlocked0 List Table 3;\lsdpriority49 \lsdlocked0 List Table 4;\lsdpriority50 \lsdlocked0 List Table 5 Dark;
\lsdpriority51 \lsdlocked0 List Table 6 Colorful;\lsdpriority52 \lsdlocked0 List Table 7 Colorful;\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 1;\lsdpriority47 \lsdlocked0 List Table 2 Accent 1;\lsdpriority48 \lsdlocked0 List Table 3 Accent 1;
\lsdpriority49 \lsdlocked0 List Table 4 Accent 1;\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 1;\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 1;\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 1;
\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 2;\lsdpriority47 \lsdlocked0 List Table 2 Accent 2;\lsdpriority48 \lsdlocked0 List Table 3 Accent 2;\lsdpriority49 \lsdlocked0 List Table 4 Accent 2;
\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 2;\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 2;\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 2;\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 3;
\lsdpriority47 \lsdlocked0 List Table 2 Accent 3;\lsdpriority48 \lsdlocked0 List Table 3 Accent 3;\lsdpriority49 \lsdlocked0 List Table 4 Accent 3;\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 3;
\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 3;\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 3;\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 4;\lsdpriority47 \lsdlocked0 List Table 2 Accent 4;
\lsdpriority48 \lsdlocked0 List Table 3 Accent 4;\lsdpriority49 \lsdlocked0 List Table 4 Accent 4;\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 4;\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 4;
\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 4;\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 5;\lsdpriority47 \lsdlocked0 List Table 2 Accent 5;\lsdpriority48 \lsdlocked0 List Table 3 Accent 5;
\lsdpriority49 \lsdlocked0 List Table 4 Accent 5;\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 5;\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 5;\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 5;
\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 6;\lsdpriority47 \lsdlocked0 List Table 2 Accent 6;\lsdpriority48 \lsdlocked0 List Table 3 Accent 6;\lsdpriority49 \lsdlocked0 List Table 4 Accent 6;
\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 6;\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 6;\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 6;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Mention;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Smart Hyperlink;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Hashtag;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Unresolved Mention;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Smart Link;}}{\*\datastore 01050000
02000000180000004d73786d6c322e534158584d4c5265616465722e362e3000000000000000000000060000
d0cf11e0a1b11ae1000000000000000000000000000000003e000300feff090006000000000000000000000001000000010000000000000000100000feffffff00000000feffffff0000000000000000ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
fffffffffffffffffdfffffffeffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffff52006f006f007400200045006e00740072007900000000000000000000000000000000000000000000000000000000000000000000000000000000000000000016000500ffffffffffffffffffffffff0c6ad98892f1d411a65f0040963251e5000000000000000000000000b07b
fbb192e2d701feffffff00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000ffffffffffffffffffffffff00000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000ffffffffffffffffffffffff0000000000000000000000000000000000000000000000000000
000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000ffffffffffffffffffffffff000000000000000000000000000000000000000000000000
0000000000000000000000000000000000000000000000000105000000000000}}')
go

--dbo.mmsSettings
if not exists (select 1 from dbo.mmsSettings where ParamName = 'The _ separator for the path is replaced by')
insert into dbo.mmsSettings (ParamName, Value) values ('The _ separator for the path is replaced by', '\')
go
