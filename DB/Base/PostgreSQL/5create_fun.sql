create or replace function patindex(pattern text, expression text) 
RETURNS INT AS 
$$
SELECT
    COALESCE(
        STRPOS(
             $2
            ,(
                SELECT
                    ( REGEXP_MATCHES(
                        $2
                        ,'(' || REPLACE( REPLACE( TRIM( $1, '%' ), '%', '.*?' ), '_', '.' ) || ')'
                        ,'i'
                    ) )[ 1 ]
                LIMIT 1
            )
        )
        ,0
    )
;
$$ LANGUAGE 'sql' IMMUTABLE;

CREATE OR REPLACE FUNCTION DateDiff (units VARCHAR(30), start_t TIMESTAMP, end_t TIMESTAMP) 
     RETURNS INT AS $BODY$
   DECLARE
     diff_interval INTERVAL; 
     diff INT = 0;
     years_diff INT = 0;
   BEGIN
     units := lower(units);
     IF units IN ('yy', 'yyyy', 'year', 'mm', 'm', 'month') THEN
       years_diff = DATE_PART('year', end_t) - DATE_PART('year', start_t);
 
       IF units IN ('yy', 'yyyy', 'year') THEN
         -- SQL Server does not count full years passed (only difference between year parts)
         RETURN years_diff;
       ELSE
         -- If end month is less than start month it will subtracted
         RETURN years_diff * 12 + (DATE_PART('month', end_t) - DATE_PART('month', start_t)); 
       END IF;
     END IF;
 
     -- Minus operator returns interval 'DDD days HH:MI:SS'  
     diff_interval = end_t - start_t;
 
     diff = diff + DATE_PART('day', diff_interval);
 
     IF units IN ('wk', 'ww', 'week') THEN
       diff = diff/7;
       RETURN diff;
     END IF;
 
     IF units IN ('dd', 'd', 'day') THEN
       RETURN diff;
     END IF;
 
     diff = diff * 24 + DATE_PART('hour', diff_interval); 
 
     IF units IN ('hh', 'hour') THEN
        RETURN diff;
     END IF;
 
     diff = diff * 60 + DATE_PART('minute', diff_interval);
 
     IF units IN ('mi', 'n', 'minute') THEN
        RETURN diff;
     END IF;
 
     diff = diff * 60 + DATE_PART('second', diff_interval);
 
     RETURN diff;
   END;
$BODY$ LANGUAGE plpgsql;

create or replace function dbo.set_session_context_userid(p_userid text) 
returns void
    LANGUAGE 'plpgsql'
    VOLATILE 
as
/*
select dbo.set_session_context_userid('10') 
*/
$BODY$
BEGIN
    perform set_config('security.userid', p_userid, false);
    return;
END;
$BODY$;

create or replace function dbo.SetDefaultRLS (var_UserId int) returns void as
$$
begin
  insert into dbo.rlsLinkUserToObject (UserId, SettingObjectId)
    select var_UserId, Id from dbo.rlsSettingObjects  where PrivateName = 'ONLYUSERDATA' and settingListId in (select id from dbo.rlsSettingList where  PrivateName = 'SPACES_LIST');

  insert into dbo.rlsLinkUserToObject (UserId, SettingObjectId)
    select var_UserId, Id from dbo.rlsSettingObjects  where PrivateName = 'ONLYUSERANDACCESSDATA' and settingListId in (select id from dbo.rlsSettingList where  PrivateName = 'RECORDING_LIST');
	
  /*add for. required for user role*/
  delete from dbo.AspNetUserRoles where UserId = var_UserId;

  insert into dbo.AspNetUserRoles (UserId, RoleId)
    select var_UserId, 1
    union all
    select var_UserId, 10
    union all
    select var_UserId, 103
    union all
    select var_UserId, 102;
    /*add for. required for user role*/
end;
$$ language 'plpgsql' volatile;

drop function if exists dbo.SetRLS(int);

create or replace function dbo.SetRLS (var_UserId int) returns void 
as
$$
declare
  var_NeedRls         smallint;
  var_is_error        int := 0;
  var_SettingList_org int := 1;
begin

  -- проверяем нужно ли рассчитывать, только если NeedRls == 1 
  select coalesce(NeedRls,0) into var_NeedRls from dbo.AspNetUsers where id = var_UserId;
  if var_NeedRls != 1 then
    return;
  end if;

-- ================================ --
  begin
    drop table if exists tmp_rlsSpace_new;
	create temporary table tmp_rlsSpace_new(UserId int, SpaceId int) on commit drop;

    select id into var_SettingList_org from dbo.rlsSettingList where PrivateName= 'SPACES_LIST' limit 1;

    --rlsSpace
    insert into tmp_rlsSpace_new (UserId, SpaceId)
      select var_UserId, SpaceId 
	  from (
            select SpaceId 
			from dbo.rlsLinkUserToObject l
              join dbo.rlsPermissionToSpace d on l.Id = d.LinkId
            where l.UserId  = var_UserId
           ) e;

    delete from dbo.rlsSpace 
	where UserId = var_UserId 
      and SpaceId in (
            select r.SpaceId 
			from dbo.rlsSpace r
              left join tmp_rlsSpace_new e on e.UserId = var_UserId and e.UserId = r.UserId and e.SpaceId = r.SpaceId
            where r.UserId = var_UserId
              and e.UserId is null
			        );

    insert into dbo.rlsSpace (UserId, SpaceId)
      select distinct n.UserId, n.SpaceId from tmp_rlsSpace_new n
      where not exists(select 1 from dbo.rlsSpace r where r.SpaceId = n.SpaceId and r.UserId = n.UserId);

  exception
     when others then 
      	  var_is_error = 1;
	      RAISE NOTICE 'exception: %', SQLERRM;
  end;

  -- При успешном выполнении обновлять поле dbo.AspNetUsers.NeedRls=0
  if var_is_error = 0 then
    update dbo.AspNetUsers set NeedRls = 0 where id = var_UserId and NeedRls != 0;
  end if;
end;
$$ language 'plpgsql' volatile;

drop function if exists dbo.periodicSetRLS();

create or replace function dbo.periodicSetRLS() returns void
as
$$
/*
select dbo.periodicSetRLS();
*/
declare 
  var_userId   int = null;
  var_msg      varchar(1024) :='';
  var_errorCnt int := 0;
  i            record;
begin
  for i in (select id as userid from dbo.AspNetUsers s order by 1) loop
    raise notice 'userId = %', i.userid;
    begin
      update dbo.AspNetUsers set NeedRls = 1 where id = i.userid and (NeedRls != 1 or NeedRls is NULL);
      perform dbo.SetRLS(i.userId);
      exception
		  WHEN others THEN
               raise notice 'ERROR: userId = % %', i.userid, sqlerrm;
               var_errorCnt := var_errorCnt + 1;
	end;
  end loop; 
end;
$$ language 'plpgsql' volatile;

drop function if exists dbo.ntfGetNotifyBookingChange(int, int);

CREATE OR REPLACE FUNCTION dbo.ntfGetNotifyBookingChange (p_subscriptionId int = null, p_Serviceid int = null)
returns void
LANGUAGE 'plpgsql'
VOLATILE 
as
/*
--#19967 - постановка: отслеживает записи в dbo.ntfEvents, где OperatioInfo in ('BOOKING_ADD', 'BOOKING_EDIT', 'BOOKING_DELETE') и ProcessingDate is null.
                       формируются записи в dbo.ntfNotifyLog, только если есть хотя бы одна запись с условием IsActive = 1 
                       и TemplateId = (select idr from dbo.ntfNotifyTemplate where PrivateName = 'BookingChange')
*/
/*
select * from dbo.ntfGetNotifyBookingChange()
select * from dbo.ntfNotifyLog order by idr desc
--delete from dbo.ntfNotifyLog
*/
$BODY$
declare
    var_NotifyTransportTypeId int := (select id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id limit 1);
    var_def_serviceId         int := (select id from dbo.services where name = 'mentolbooking' order by id limit 1);
BEGIN
    p_Serviceid := coalesce(p_Serviceid, var_def_serviceId);

    if p_subscriptionId is null then
        p_subscriptionId := (select idr from dbo.ntfSubscription where IsActive = True and TemplateId = (select idr from dbo.ntfNotifyTemplate where PrivateName = 'BookingChange' limit 1) order by idr limit 1);
    end if;
    /* если подписок нет - завершаем работу */
    if p_subscriptionId is null then
        return;
    end if;
    /* если передали подписку то проверяем что она активна */
    if not exists(select 1 from dbo.ntfSubscription where idr = p_subscriptionId and IsActive = True and TemplateId = (select idr from dbo.ntfNotifyTemplate where PrivateName = 'BookingChange' limit 1)) then
        return;
    end if;

    create temp table tmp_params(
         ntfEventsId  int
        ,bookingId    int
        ,booking_name text
        ,email_json   jsonb
    ) on commit drop;
    insert into tmp_params(ntfEventsId,bookingId,booking_name,email_json)
    select 
         idr                                  as ntfEventsId
        ,cast(trim(Param1) as int)            as bookingId
        ,param2::jsonb #>> '{booking_name}'   as booking_name
        ,Param2::jsonb                        as email_json
    from dbo.ntfEvents ntf 
    where
        upper(ntf.OperationInfo) in ('BOOKING_ADD', 'BOOKING_EDIT', 'BOOKING_DELETE') 
    and ntf.ProcessingDate is null
    and ServiceId = p_Serviceid;

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
         now()                     as DateRecord
        ,p_subscriptionId          as SubscriptionId
        ,''                        as Info
        ,0                         as EmployeeId
        ,el.email                  as NotifyEmail
        ,p.ntfEventsId             as ntfEventsId
        ,p.booking_name            as InfoSubject
        ,var_NotifyTransportTypeId as NotifyTransportTypeId
        ,0                         as AttemptCount
    from tmp_params p
      left join lateral (select coalesce(string_agg(t.email,';'),'') as email
                         from (select value as email 
	                       from jsonb_array_elements_text(p.email_json #> '{vksusers_emails}')) t
			 ) as el on True;
end;
$BODY$;

drop function if exists dbo.ntfGetNotifyRecordingDelete(int, int);
drop function if exists dbo.ntfGetNotifyRecordingAddDelete(int, int);

CREATE OR REPLACE FUNCTION dbo.ntfGetNotifyRecordingAddDelete(var_subscriptionId int=null, var_Serviceid int = null)
returns void
LANGUAGE 'plpgsql'
VOLATILE 
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
select dbo.ntfGetNotifyRecordingAddDelete()
select * from dbo.ntfNotifyLog
*/
$BODY$
declare
    p_NotifyTransportTypeId int := (select id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id limit 1);
	p_TemplateId int  := (select idr from dbo.ntfNotifyTemplate where PrivateName = 'RecordingAddDelete' order by idr asc limit 1);
BEGIN
	var_subscriptionId = coalesce(var_subscriptionId, (select idr from dbo.ntfSubscription where IsActive = True and TemplateId = p_TemplateId order by idr asc limit 1));

	-- если подписок нет - выходим, ничего не делаем
	if var_subscriptionId is null 
    then
		return;
    end if;

	create temp table tmp_events(ntfEventsId int, Param1 varchar(256) , Param2 varchar(8000), OperationInfo varchar(256) ) on commit drop;

	insert into tmp_events(ntfEventsId, Param1, Param2, OperationInfo)
	select e.idr as ntfEventsId, Param1, Param2, OperationInfo from dbo.ntfEvents e where OperationInfo in ('RECORDING_DELETE', 'RECORDING_ADD') and ProcessingDate is null;

	insert into dbo.ntfNotifyLog(DateRecord, SubscriptionId, Info, EmployeeId, NotifyEmail, ntfEventsId, InfoSubject, AttemptCount, NotifyTransportTypeId)
	select 
		 now()            as DateRecord
		,var_subscriptionId as SubscriptionId
		,''               as Info
		,0                as EmployeeId
        ,(param2::json) #>> '{vksUsersEmail}' as InfoSubject
		,e.ntfEventsId    as ntfEventsId
        ,coalesce((param2::json) #>> '{booking_name}','')||case when OperationInfo = 'RECORDING_DELETE' then ' Удалена запись ВКС' else  ' Добавлена запись ВКС' end as InfoSubject
		,0                as AttemptCount
		,p_NotifyTransportTypeId       as NotifyTransportTypeId
	from tmp_events e;
end;
$BODY$;

drop function if exists dbo.ntfGetNotifyRecordingVksUsersChange(int, int);

CREATE OR REPLACE FUNCTION dbo.ntfGetNotifyRecordingVksUsersChange(var_subscriptionId int=null, var_Serviceid int = null)
returns void
LANGUAGE 'plpgsql'
VOLATILE 
as
--#20207 - постановка
/*
select dbo.ntfGetNotifyRecordingVksUsersChange()
select * from dbo.ntfNotifyLog
*/
$BODY$
declare
    p_NotifyTransportTypeId int := (select id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id limit 1);
	p_TemplateId int  := (select idr from dbo.ntfNotifyTemplate where PrivateName = 'RecordingVksUsersChange' order by idr asc limit 1);
BEGIN
	var_subscriptionId := (select idr from dbo.ntfSubscription where IsActive = True and TemplateId = p_TemplateId order by idr asc) limit 1;

	-- если подписок нет - выходим, ничего не делаем
	if var_subscriptionId is null 
    then
		return;
    end if;

	create temp table tmp_events(ntfEventsId int, Param1 varchar(256) , Param2 varchar(8000) ) on commit drop;

	insert into tmp_events(ntfEventsId, Param1, Param2)
	  select e.idr as ntfEventsId, Param1, Param2 from dbo.ntfEvents e where OperationInfo in ('RECORDINGVKSUSERS_ADD', 'RECORDINGVKSUSERS_EDIT', 'RECORDINGVKSUSERS_DELETE') and ProcessingDate is null;

	if exists(select 1 from tmp_events) then
		insert into dbo.ntfNotifyLog(DateRecord, SubscriptionId, Info, EmployeeId, NotifyEmail, ntfEventsId, InfoSubject, AttemptCount, NotifyTransportTypeId)
		  select 
			 getdate()          as DateRecord
			,var_subscriptionId as SubscriptionId
			,''                 as Info
			,0                  as EmployeeId
            ,trim((param2::json) #>> '{userEmail}')::text as NotifyEmail
			,e.ntfEventsId      as ntfEventsId
			,coalesce((param2::json) #>> '{booking_name}','') || ' Права на запись ВКС' as InfoSubject
			,0                  as AttemptCount
			,p_NotifyTransportTypeId       as NotifyTransportTypeId
		  from tmp_events e;
		--print 'rows inserted: '+cast(p_p_rowcount as varchar);
	end if;

    return;
end;
$BODY$;

drop function if exists is_valid_json();

create or replace function is_valid_json(p_json text)
  returns boolean
as
$$
begin
  return (p_json::json is not null);
exception 
  when others then
     return false;  
end;
$$
language plpgsql
immutable;


drop function if exists dbo.ntfGetNotifyRecordingNotification(int, int);

CREATE OR REPLACE FUNCTION dbo.ntfGetNotifyRecordingNotification(var_subscriptionId int=null, var_Serviceid int = null)
returns void
LANGUAGE 'plpgsql'
VOLATILE 
as
--#20327 - постановка
/*
select dbo.ntfGetNotifyRecordingNotification()
select * from dbo.ntfNotifyLog order by idr desc
*/
$BODY$
declare
    p_NotifyTransportTypeId int := (select id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id limit 1);
	p_TemplateId int  := (select idr from dbo.ntfNotifyTemplate where PrivateName = 'RecordingVksUsersChange' order by idr asc limit 1);
BEGIN
	var_subscriptionId := coalesce(var_subscriptionId, (select idr from dbo.ntfSubscription where IsActive = True and TemplateId = p_TemplateId order by idr asc limit 1));

	-- если подписок нет - выходим, ничего не делаем
	if var_subscriptionId is null 
    then
		return;
    end if;

	create temp table tmp_events(ntfEventsId int, Param1 varchar(256) , Param2 varchar(8000) ) on commit drop;

	insert into tmp_events(ntfEventsId, Param1, Param2)
	  select e.idr as ntfEventsId, Param1, Param2 from dbo.ntfEvents e where OperationInfo in ('RECORDING_DELETE_NOTIFICATION') and ProcessingDate is null
	 	and is_valid_json(param2);

	if exists(select 1 from tmp_events) then
		insert into dbo.ntfNotifyLog(DateRecord, SubscriptionId, Info, EmployeeId, NotifyEmail, ntfEventsId, InfoSubject, AttemptCount, NotifyTransportTypeId)
		  select 
			 now()              as DateRecord
			,var_subscriptionId as SubscriptionId
			,''                 as Info
			,0                  as EmployeeId
                        ,trim((param2::json) #>> '{vksUsersEmail}')::text as NotifyEmail
			,e.ntfEventsId      as ntfEventsId
			,coalesce((param2::json) #>> '{bookingName}','') || ' Предстоящее удаление записи ВКС' as InfoSubject
			,0                  as AttemptCount
			,p_NotifyTransportTypeId       as NotifyTransportTypeId
		  from tmp_events e;
		--print 'rows inserted: '+cast(p_p_rowcount as varchar);
	end if;

    return;
end;
$BODY$;

drop function if exists dbo.ntfGetNotifyBookingChangePincodeNotification(int, int);

CREATE OR REPLACE FUNCTION dbo.ntfGetNotifyBookingChangePincodeNotification(var_subscriptionId int=null, var_Serviceid int = null)
returns void
LANGUAGE 'plpgsql'
VOLATILE 
as
--#20353 - постановка
/*
select dbo.ntfGetNotifyBookingChangePincodeNotification()
select * from dbo.ntfNotifyLog
*/
$BODY$
declare
    p_NotifyTransportTypeId int := (select id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id limit 1);
	p_TemplateId int  := (select idr from dbo.ntfNotifyTemplate where PrivateName = 'BookingChangePincodeNotification' order by idr asc limit 1);
BEGIN
	var_subscriptionId :=coalesce((select idr from dbo.ntfSubscription where IsActive = True and TemplateId = p_TemplateId order by idr asc limit 1), var_subscriptionId);

	-- если подписок нет - выходим, ничего не делаем
	if var_subscriptionId is null 
    then
		return;
    end if;

	create temp table tmp_events(ntfEventsId int, Param1 varchar(256) , Param2 varchar(8000) ) on commit drop;

	insert into tmp_events(ntfEventsId, Param1, Param2)
	  select e.idr as ntfEventsId, Param1, Param2 from dbo.ntfEvents e where OperationInfo in ('BOOKING_CHANGE_PINCODE_NOTIFICATION') and ProcessingDate is null;

	if exists(select 1 from tmp_events) then
		insert into dbo.ntfNotifyLog(DateRecord, SubscriptionId, Info, EmployeeId, NotifyEmail, ntfEventsId, InfoSubject, AttemptCount, NotifyTransportTypeId)
		  select 
			 now()              as DateRecord
			,var_subscriptionId as SubscriptionId
			,''                 as Info
			,0                  as EmployeeId
            ,trim((param2::json) #>> '{vksUser_email}')::text as NotifyEmail
			,e.ntfEventsId      as ntfEventsId
			,coalesce((param2::json) #>> '{booking_name}','') || ' Предстоящая смена ПИН-кода' as InfoSubject
			,0                  as AttemptCount
			,p_NotifyTransportTypeId       as NotifyTransportTypeId
		  from tmp_events e;
		--print 'rows inserted: '+cast(p_p_rowcount as varchar);
	end if;

    return;
end;
$BODY$;

drop function if exists dbo.ntfGetNotifyConferenceEnded(int, int);

CREATE OR REPLACE FUNCTION dbo.ntfGetNotifyConferenceEnded(var_subscriptionId int=null, var_Serviceid int = null)
returns void
LANGUAGE 'plpgsql'
VOLATILE 
as
--#20481 - постановка
/*
select dbo.ntfGetNotifyConferenceEnded()
select * from dbo.ntfNotifyLog
*/
$BODY$
declare
    p_NotifyTransportTypeId int := (select id from dbo.NotifyTransportType nt where nt.PrivateName = 'EMAIL' order by id limit 1);
	p_TemplateId int  := (select idr from dbo.ntfNotifyTemplate where PrivateName = 'ConferenceEnded' order by idr asc limit 1);
BEGIN
	var_subscriptionId :=coalesce((select idr from dbo.ntfSubscription where IsActive = True and TemplateId = p_TemplateId order by idr asc limit 1), var_subscriptionId);

	-- если подписок нет - выходим, ничего не делаем
	if var_subscriptionId is null 
    then
		return;
    end if;

	create temp table tmp_events(ntfEventsId int, Param1 varchar(256) , Param2 varchar(8000) ) on commit drop;

	insert into tmp_events(ntfEventsId, Param1, Param2)
	  select e.idr as ntfEventsId, Param1, Param2 from dbo.ntfEvents e where OperationInfo = 'CONFERENCE_ENDED' and ProcessingDate is null and is_valid_json(param2);

	if exists(select 1 from tmp_events) then
		insert into dbo.ntfNotifyLog(DateRecord, SubscriptionId, Info, EmployeeId, NotifyEmail, ntfEventsId, InfoSubject, AttemptCount, NotifyTransportTypeId)
		  select 
			 now()              as DateRecord
			,var_subscriptionId as SubscriptionId
			,''                 as Info
			,0                  as EmployeeId
            ,trim((param2::json) #>> '{vksUsersEmail}')::text as NotifyEmail
			,e.ntfEventsId      as ntfEventsId
			,coalesce((param2::json) #>> '{bookingName}','') || ' Встреча завершена' as InfoSubject
			,0                  as AttemptCount
			,p_NotifyTransportTypeId       as NotifyTransportTypeId
		  from tmp_events e;
		--print 'rows inserted: '+cast(p_p_rowcount as varchar);
	end if;

    return;
end;
$BODY$;