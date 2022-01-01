/*==============================================================*/
/* Table: licensexml                                            */
/*==============================================================*/
create table dbo.licensexml (
   Products             text                 null,
   SerialNumber         VARCHAR(50)          null,
   DateStart            VARCHAR(50)          null,
   DateEnd              VARCHAR(50)          null,
   Hash                 VARCHAR(512)         null,
   FileAll              VARCHAR(8000)           null
);

-- set table ownership
alter table dbo.licensexml owner to dbo
;

/*==============================================================*/
/* Table: mmsSettings                                           */
/*==============================================================*/
create table dbo.mmsSettings (
   idr                  SERIAL not null,
   ParamName            varchar(150)         not null,
   Value                varchar(150)         null,
   constraint PK_MMSSETTINGS primary key (idr)
);

-- set table ownership
alter table dbo.mmsSettings owner to dbo
;

/*==============================================================*/
/* Table: timezone                                            */
/*==============================================================*/
create table dbo.timezone (
   idr                  INT4                 not null,
   name                 varchar(64)          not null,
   privatename          varchar(64)          not null,
   offsetminute         int4                 not null,
   standartid           varchar(64)          not null,
   constraint PK_TIMEZONE primary key (idr)
);

-- set table ownership
alter table dbo.timezone owner to dbo
;

/*==============================================================*/
/* Index: IX_standartid                                         */
/*==============================================================*/
create unique index IX_standartid on dbo.timezone (
standartid ASC
);

/*==============================================================*/
/* Table: bookingtype                                            */
/*==============================================================*/
create table dbo.bookingtype (
   idr                  INT4                  not null,
   name                 varchar(64)          not null,
   privatename          varchar(64)          not null,
   constraint PK_BOOKINGTYPE primary key (idr)
);

-- set table ownership
alter table dbo.bookingtype owner to dbo
;

/*==============================================================*/
/* Table: SMTPSettings                                          */
/*==============================================================*/
create table dbo.SMTPSettings (
   idr                  INT4                 not null,
   server               VARCHAR(250)         null,
   port                 INT4                 null,
   login                VARCHAR(128)         null,
   pswd                 VARCHAR(128)         null,
   addressfrom          VARCHAR(128)         null,
   securemode           VARCHAR(512)         null,
   auth                 VARCHAR(50)          null,
   domain               VARCHAR(100)         null,
   ntlmdomain           VARCHAR(100)         null,
   constraint PK_SMTPSETTINGS primary key (idr)
);

-- set table ownership
alter table dbo.SMTPSettings owner to dbo
;

/*==============================================================*/
/* Table: FilterColumnsList                                     */
/*==============================================================*/
create table dbo.FilterColumnsList (
   idr                  SERIAL not null,
   TableId              INT4                 null,
   ColumnName           varchar(256)         null,
   FilterTypeId         INT4                 null,
   DataQuery            text                 null,
   ConditionColumn      varchar(1024)        null,
   DisplayMember        varchar(1024)        null,
   ValueMember          varchar(1024)        null,
   IsTableColumn        bool                 not null default true,
   FilterSql            varchar(4000)        null,
   WhereColumn          varchar(100)         null,
   Title                varchar(100)         null,
   constraint PK_FILTERCOLUMNSLIST primary key (idr)
);

-- set table ownership
alter table dbo.FilterColumnsList owner to dbo
;

/*==============================================================*/
/* Table: FilterForColumnTypeList                               */
/*==============================================================*/
create table dbo.FilterForColumnTypeList (
   idr                  SERIAL not null,
   TypeId               INT4                 not null,
   TypeName             varchar(256)         null,
   DataQuery            text                 null,
   constraint PK_FILTERFORCOLUMNTYPELIST primary key (TypeId)
);

-- set table ownership
alter table dbo.FilterForColumnTypeList owner to dbo
;

/*==============================================================*/
/* Table: FilterOperationsList                                  */
/*==============================================================*/
create table dbo.FilterOperationsList (
   idr                  SERIAL not null,
   OperationName        varchar(1024)        null,
   Operand              varchar(1024)        null,
   ColumnTypeFilt       INT4                 null,
   constraint PK_FILTEROPERATIONSLIST primary key (idr)
);

-- set table ownership
alter table dbo.FilterOperationsList owner to dbo
;

/*==============================================================*/
/* Table: FilterTablesList                                      */
/*==============================================================*/
create table dbo.FilterTablesList (
   idr                  SERIAL not null,
   TableName            varchar(256)         null,
   DBTable              varchar(256)         null,
   AccessCondition      varchar(1024)        null,
   constraint PK_FILTERTABLESLIST primary key (idr)
);

-- set table ownership
alter table dbo.FilterTablesList owner to dbo
;

/*==============================================================*/
/* Table: FilterValue                                           */
/*==============================================================*/
create table dbo.FilterValue (
   idr                  SERIAL not null,
   FilterId             INT4                 null,
   ColumnId             INT4                 null,
   OperationId          INT4                 null,
   FValue               varchar(1024)        null,
   constraint PK_FILTERVALUE primary key (idr)
);

-- set table ownership
alter table dbo.FilterValue owner to dbo
;

/*==============================================================*/
/* Table: FiltersList                                           */
/*==============================================================*/
create table dbo.FiltersList (
   idr                  SERIAL not null,
   FilterName           varchar(256)         null,
   IsCommon             INT4                 null,
   constraint PK_FILTERSLIST primary key (idr)
);

-- set table ownership
alter table dbo.FiltersList owner to dbo
;

/*==============================================================*/
/* Table: FiltersToUserLink                                     */
/*==============================================================*/
create table dbo.FiltersToUserLink (
   idr                  SERIAL not null,
   FilterId             INT4                  null,
   UserId               INT4                  null,
   IsActive             INT4                  null,
   constraint PK_FILTERSTOUSERLINK primary key (idr)
);

-- set table ownership
alter table dbo.FiltersToUserLink owner to dbo
;

/*==============================================================*/
/* Table: Logs                                                  */
/*==============================================================*/
create table dbo.Logs (
   idr                  SERIAL not null,
   ProductId            INT4                 null,
   TypeId               INT4                 not null,
   LevelId              INT2                 null,
   UserName             varchar(250)         null,
   DateRecord           timestamp without time zone null,
   Action               varchar(500)         null,
   Description          varchar(2000)        null,
   IP                   varchar(200)         null,
   ObjectId             INT4                 null,
   PropertyId           INT2                 null,
   constraint PK_LOGS primary key (idr)
);

-- set table ownership
alter table dbo.Logs owner to dbo
;

/*==============================================================*/
/* Table: LogsType                                              */
/*==============================================================*/
create table dbo.LogsType (
   idr                  SERIAL not null,
   name                 varchar(150)         null,
   PrivateName          varchar(128)         null,
   constraint PK_LOGSTYPE primary key (idr)
);

-- set table ownership
alter table dbo.LogsType owner to dbo
;

/*==============================================================*/
/* Table: Products                                              */
/*==============================================================*/
create table dbo.Products (
   idr                  SERIAL not null,
   name                 varchar(150)         null,
   constraint PK_PRODUCTS primary key (idr)
);

-- set table ownership
alter table dbo.Products owner to dbo
;

/*==============================================================*/
/* Table: RefreshLog                                            */
/*==============================================================*/
create table dbo.RefreshLog (
   idr                  SERIAL not null,
   Info                 varchar(2048)        null,
   UploadDate           timestamp without time zone null,
   ServicesId           INT4                 not null,
   SitesIds             varchar(1000)        null,
   Mode                 INT4                 not null default 0,
   constraint PK_REFRESHLOG primary key (idr)
);

-- set table ownership
alter table dbo.RefreshLog owner to dbo
;

/*==============================================================*/
/* Table: RefreshToken                                          */
/*==============================================================*/
create table dbo.RefreshToken (
   Id                   SERIAL               not null,
   AspNetUserId         int4                 not null,
   CreateDt             timestamp without time zone not null,
   FingerPrint          varchar(500)         null,
   Ip                   varchar(100)         null,
   Token                varchar(5000)        not null,
   EndDate              timestamp without time zone null,
   constraint PK_REFRESHTOKEN primary key (Id)
);

-- set table ownership
alter table dbo.RefreshToken owner to dbo
;

/*==============================================================*/
/* Table: Role                                                  */
/*==============================================================*/
create table dbo.Role (
   Name                 varchar(255)         null,
   DisplayName          varchar(255)         null,
   Description          varchar(255)         null,
   Id                   SERIAL not null,
   constraint PK_ROLE primary key (Id)
);

-- set table ownership
alter table dbo.Role owner to dbo
;

/*==============================================================*/
/* Table: Services                                              */
/*==============================================================*/
create table dbo.Services (
   id                   SERIAL not null,
   name                 varchar(100)         not null,
   conffilename         varchar(100)         not null,
   pathexe              varchar(100)         null,
   description          varchar(250)         null,
   constraint PK_SERVICES primary key (id)
);

-- set table ownership
alter table dbo.Services owner to dbo
;

/*==============================================================*/
/* Table: TableColumnSettings                                   */
/*==============================================================*/
create table dbo.TableColumnSettings (
   Id                   SERIAL not null,
   TableName            varchar(255)         not null,
   ColumnName           varchar(255)         not null,
   Title                varchar(255)         null,
   Value                varchar(255)         not null,
   "order"              INT4                 not null,
   MinWidth             INT4                 not null default 0,
   Wrap                 varchar(255)         null,
   Template             varchar(255)         null,
   ClassName            varchar(255)         null,
   RoleId               INT4                 null,
   NoSortable           bool                 not null default false,
   NoResizable          bool                 not null default false,
   ActionButton         bool                 not null default false,
   CellRenderer         varchar(200)         not null default 'booleanColumnsNames',
   CellsWithoutHint     bool                 not null default true,
   VisibleCheckBox      bool                 not null default false,
   constraint PK_TABLECOLUMNSETTINGS primary key (Id)
);

-- set table ownership
alter table dbo.TableColumnSettings owner to dbo
;

/*==============================================================*/
/* Index: is_tablecolumnsettings_tablename_order                */
/*==============================================================*/
create unique index is_tablecolumnsettings_tablename_order on dbo.TableColumnSettings (
TableName ASC,
"order" ASC
);

/*==============================================================*/
/* Table: UserRoles                                             */
/*==============================================================*/
create table dbo.UserRoles (
   Id                   SERIAL not null,
   UserId               INT4                 not null,
   RoleId               INT4                 not null,
   constraint PK_USERROLES primary key (Id)
);

-- set table ownership
alter table dbo.UserRoles owner to dbo
;

/*==============================================================*/
/* Table: UserTableColumns                                      */
/*==============================================================*/
create table dbo.UserTableColumns (
   Id                   SERIAL not null,
   UserId               INT4                 not null,
   TableColumnId        INT4                 not null,
   "order"              INT4                 not null,
   Width                INT4                 not null default 0,
   constraint PK_USERTABLECOLUMNS primary key (Id)
);

-- set table ownership
alter table dbo.UserTableColumns owner to dbo
;

/*==============================================================*/
/* Table: Users                                                 */
/*==============================================================*/
create table dbo.Users (
   Id                   SERIAL not null,
   Email                varchar(256)        null,
   PasswordHash         varchar(4000)       null,
   SecurityStamp        varchar(4000)       null,
   UserName             varchar(256)        null,
   Post                 varchar(256)        null,
   UserFullName         varchar(100)        null,
   SID                  varchar(256)        null,
   Provider             varchar(50)         null,
   APIKey               varchar(150)        null,
   NeedRls              int2                null,
   Login                varchar(255)        null,
   NormalizedLogin      varchar(255)        null,
   NormalizedEmail      varchar(255)        null,
   constraint PK_Users primary key (Id)
);

-- set table ownership
alter table dbo.Users owner to dbo
;

/*==============================================================*/
/* Table: BalancerList                                          */
/*==============================================================*/
create table dbo.BalancerList (
   id                   int4                 not null,
   privatename          varchar(128)         null,
   constraint PK_BALANCERLIST primary key (id)
);

-- set table ownership
alter table dbo.BalancerList owner to dbo
;

/*==============================================================*/
/* Table: ServersGroups                                         */
/*==============================================================*/
create table dbo.ServersGroups (
   id                   SERIAL not null,
   name                 varchar(128)         null,
   description          varchar(256)         null,
   isusebalancer        int4                 null,
   balanceralgid        int4                 null,
   timezoneid           int                  null,
   constraint PK_SERVERSGROUPS primary key (id)
);

-- set table ownership
alter table dbo.ServersGroups owner to dbo
;

/*==============================================================*/
/* Table: Spacegroups                                           */
/*==============================================================*/
create table dbo.Spacegroups (
   id                   SERIAL not null,
   name                 varchar(128)         null,
   description          varchar(256)         null,
   constraint PK_SPACEGROUPS primary key (id)
);

-- set table ownership
alter table dbo.Spacegroups owner to dbo
;

/*==============================================================*/
/* Table: Space                                                 */
/*==============================================================*/
create table dbo.Space (
   id                   SERIAL not null,
   serversgroupsid      int4                 null,
   name                 varchar(512)         null,
   uri                  varchar(128)         null,
   tagcdr               varchar(128)         null,
   guid                 varchar(36)          null,
   password             varchar(128)         null,
   urialt               varchar(128)         null,
   passwordguest        varchar(128)         null,
   urivideo             varchar(128)         null,
   isguestaccessible    bool                 null,
   isavailableforbooking bool                null,
   spacegroupsid        int4                 null,
   callid               varchar(32)          null,
   calllegprofileguid   varchar(36)          null,
   callbrandingprofileguid varchar(36)          null,
   ownerid              int4                  null,
   constraint PK_SPACE primary key (id)
);

alter table dbo.Space owner to dbo
;

/*==============================================================*/
/* Table: ConnectionType                                        */
/*==============================================================*/
create table dbo.ConnectionType (
   id                   int4                 not null,
   privatename          varchar(32)          not null,
   name                 varchar(64)          not null,
   constraint PK_CONNECTIONTYPE primary key (id)
);

alter table dbo.ConnectionType owner to dbo
;

/*==============================================================*/
/* Table: Booking                                               */
/*==============================================================*/
create table dbo.Booking (
   id                   SERIAL not null,
   name                 varchar(256)         null,
   description          varchar(256)         null,
   location             varchar(256)         null,
   ownerid              int4                 null,
   datestart            timestamp without time zone null,
   timezone             int4                 null,
   duration             int4                 null,
   isusepin             bool                 null,
   schedule             varchar(128)         null,
   spaceid              int4                 not null,
   connectiontypeid     int4                 null,
   attemptscount        int4                 null,
   delay                int4                 null,
   issendnotification   bool                 null,
   issynctoexchange     bool                 null,
   openconferencebefore int4                 null,
   isneverusepin        bool                 null,
   dateend              timestamp without time zone null,
   repeatcount          int4                 null,
   pinpoliticsid        int4                 null,
   pinschedule          varchar(128)         null,
   pindatestart         timestamp without time zone null,
   typeid               int4                 null,
   pincode              varchar(128)         null,
   scheduletab          varchar(4)           null,
   pinscheduletab       varchar(4)           null,
   Ics                  text                 null,
   Uid                  varchar(256)         null,
   newpincode           varchar(128)         null,
   constraint PK_BOOKING primary key (id)
);

alter table dbo.Booking owner to dbo
;

/*==============================================================*/
/* Table: BookingStatus                                         */
/*==============================================================*/
create table dbo.BookingStatus (
   BookingId            int                  not null,
   StatusId             int                  not null,
   StatusDate           timestamp without time zone null,
   Message              varchar(2000)        null,
   Params               varchar(2000)        null,
   ActiveStatus         varchar(16)          null,
   NextRun              timestamp without time zone null,
   Error                bool                 null,
   ProlongationCount    int                  null,
   LastStart            timestamp without time zone null,
   RepeatCount          int                  null,
   CallId               varchar(50)          null,
   DateEnd              timestamp without time zone null,
   constraint PK_BookingStatus primary key (BookingId)
);

alter table dbo.BookingStatus owner to dbo
;

/*==============================================================*/
/* Index: IX_BookingStatus_CallId                               */
/*==============================================================*/
create index IX_BookingStatus_CallId on dbo.BookingStatus (
CallId ASC
);

/*==============================================================*/
/* Table: BookingStatusPin                                      */
/*==============================================================*/
create table dbo.BookingStatusPin (
   BookingId            int                  not null,
   NextChangePinCode    timestamp without time zone null,
   NewPinCode           varchar(128)         null,
   LastStart            timestamp without time zone null,
   constraint PK_BOOKINGSTATUSPIN primary key (BookingId)
);

alter table dbo.BookingStatusPin owner to dbo
;

/*==============================================================*/
/* Table: BookingTaskStatus                                     */
/*==============================================================*/
create table dbo.BookingTaskStatus (
   BookingId            int                  not null,
   StatusDate           timestamp without time zone not null,
   LastStart            timestamp without time zone null,
   NextProcessing       timestamp without time zone null,
   constraint PK_BookingTaskStatus primary key (BookingId)
);

alter table dbo.BookingTaskStatus owner to dbo
;

/*==============================================================*/
/* Table: pinpolitics                                           */
/*==============================================================*/
create table dbo.pinpolitics (
   idr                  SERIAL not null,
   name                 varchar(32)          not null,
   privatename          varchar(32)          not null,
   constraint PK_PINPOLITICS primary key (idr)
);

alter table dbo.pinpolitics owner to dbo
;

/*==============================================================*/
/* Table: LinkBookingToParticipant                              */
/*==============================================================*/
create table dbo.LinkBookingToParticipant (
   id                   SERIAL not null,
   bookingid            int4                 null,
   vksparticipantid     int4                 null,
   uri                  varchar(64)          null,
   callLegProfileGuid   varchar(36)          null,
   constraint PK_LINKBOOKINGTOPARTICIPANT primary key (id)
);

alter table dbo.LinkBookingToParticipant owner to dbo
;
/*==============================================================*/
/* Index: ix_linkbt_bookingid_vksparticipantid                  */
/*==============================================================*/
create unique index ix_linkbt_bookingid_vksparticipantid on dbo.LinkBookingToParticipant (
bookingid ASC,
vksparticipantid ASC
);

/*==============================================================*/
/* Table: vksCallCurrent                                        */
/*==============================================================*/
create table dbo.vksCallCurrent (
   idr                  SERIAL not null,
   GUID                 VARCHAR(36)          not null,
   ConferenceID         INT4                 null,
   CallCorrelatorGUID   VARCHAR(36)          null,
   CallType             varchar(50)          not null,
   Name                 varchar(100)         not null,
   NumCallLegs          int2                 null,
   MaxCallLegs          int2                 null,
   NumParticipantsLocal int2                 null,
   Locked               bool                 not null,
   Recording            bool                 not null,
   RecordingStatus      bool                 not null,
   Streaming            bool                 not null,
   StreamingStatus      bool                 not null,
   AllowAllMuteSelf     bool                 not null,
   AllowAllPresentationContribution bool     not null,
   MessagePosition      varchar(50)          null,
   MessageDuration      varchar(50)          null,
   ActiveWhenEmpty      bool                 not null,
   EndpointRecording    bool                 not null,
   DurationSeconds      INT4                 not null,
   CreateTime           timestamp without time zone null,
   StartTime            timestamp without time zone null,
   EndTime              timestamp without time zone null,
   DateLastRecord       timestamp without time zone not null,
   constraint PK_VKSCALLCURRENT primary key (idr)
);

-- set table ownership
alter table dbo.vksCallCurrent owner to dbo
;

/*==============================================================*/
/* Table: vksCallHistory                                        */
/*==============================================================*/
create table dbo.vksCallHistory (
   idr                  SERIAL not null,
   GUID                 VARCHAR(36)          null,
   ConferenceID         INT4                 null,
   CallCorrelatorGUID   VARCHAR(36)          null,
   CallType             varchar(50)          null,
   Name                 varchar(100)         not null,
   NumCallLegs          int2                 null,
   MaxCallLegs          int2                 null,
   NumParticipantsLocal int2                 null,
   Locked               bool                 null,
   Recording            bool                 null,
   RecordingStatus      bool                 null,
   Streaming            bool                 null,
   StreamingStatus      bool                 null,
   AllowAllMuteSelf     bool                 null,
   AllowAllPresentationContribution bool     null,
   MessagePosition      varchar(50)          null,
   MessageDuration      varchar(50)          null,
   ActiveWhenEmpty      bool                 null,
   EndpointRecording    bool                 null,
   DurationSeconds      int                  null,
   CreateTime           timestamp without time zone null,
   StartTime            timestamp without time zone null,
   EndTime              timestamp without time zone null,
   DateLastRecord       timestamp without time zone not null,
   constraint PK_VKSCALLHISTORY primary key (idr)
);

-- set table ownership
alter table dbo.vksCallHistory owner to dbo
;

/*==============================================================*/
/* Table: vksCallInstances                                      */
/*==============================================================*/
create table dbo.vksCallInstances (
   idr                  SERIAL not null,
   GUID                 VARCHAR(36)          not null,
   CallGUID             VARCHAR(36)          not null,
   RemoteParty          varchar(100)         not null,
   OriginalRemoteParty  varchar(100)         not null,
   Name                 varchar(100)         null,
   LocalAddress         varchar(100)         null,
   Type                 varchar(10)          null,
   Direction            varchar(10)          null,
   CanMove              bool                 null,
   CreateTime           timestamp without time zone null,
   StartTime            timestamp without time zone null,
   EndTime              timestamp without time zone null,
   DateLastRecord       timestamp without time zone not null,
   constraint PK_VKSCALLINSTANCES primary key (idr)
);

-- set table ownership
alter table dbo.vksCallInstances owner to dbo
;

/*==============================================================*/
/* Table: vksCallInstancesConfig                                */
/*==============================================================*/
create table dbo.vksCallInstancesConfig (
   idr                  SERIAL not null,
   CallInstanceID       INT4                        not null,
   Name                 varchar(100)                not null,
   DefaultLayout        varchar(20)                 not null,
   ParticipantLabels    bool                        not null,
   PresentationContributionAllowed bool             not null,
   PresentationViewingAllowed bool                  not null,
   MuteOthersAllowed    bool                        not null,
   VideoMuteOthersAllowed bool                      not null,
   MuteSelfAllowed      bool                        not null,
   VideoMuteSelfAllowed bool                        not null,
   DisconnectOthersAllowed bool                     not null,
   TelepresenceCallsAllowed bool                    not null,
   sipPresentationChannelEnabled bool               not null,
   ChangeLayoutAllowed  bool                        not null,
   bfcpMode             varchar(20)                 not null,
   AllowAllMuteSelfAllowed bool                     not null,
   AllowAllPresentationContributionAllowed bool     not null,
   ChangeJoinAudioMuteOverrideAllowed bool          not null,
   RecordingControlAllowed bool                     not null,
   StreamingControlAllowed bool                     not null,
   DateLastRecord       timestamp without time zone not null,
   constraint PK_VKSCALLINSTANCESCONFIG primary key (idr)
);

-- set table ownership
alter table dbo.vksCallInstancesConfig owner to dbo
;

/*==============================================================*/
/* Table: vksCallInstancesStatus                                */
/*==============================================================*/
create table dbo.vksCallInstancesStatus (
   idr                  SERIAL not null,
   CallInstanceID       INT4                 not null,
   State                varchar(10)          not null,
   DurationSeconds      INT4                 null,
   Direction            varchar(10)          not null,
   sipCallID            VARCHAR(36)          null,
   GroupID              VARCHAR(36)          null,
   EncryptedMedia       varchar(5)           null,
   UnencryptedMedia     varchar(5)           null,
   Layout               varchar(20)          null,
   CameraControlAvailable varchar(5)         null,
   rxAudioCodec         varchar(10)          null,
   rxAudioCodecPacketLossPercentage numeric(5,2)         null,
   rxAudioJitter        numeric              null,
   rxAudioBitRate       numeric              null,
   rxAudioGainApplied   numeric(5,2)         null,
   txAudioCodec         varchar(10)          null,
   txAudioCodecPacketLossPercentage numeric(5,2)         null,
   txAudioJitter        numeric              null,
   txAudioBitRate       numeric              null,
   txAudioRoundTripTime numeric              null,
   txVideoRole          varchar(20)          null,
   txVideoCodec         varchar(20)          null,
   txVideoWidth         numeric              null,
   txVideoHeight        numeric              null,
   txVideoFrameRate     numeric(4,1)         null,
   txVideoBitRate       numeric              null,
   txVideoPacketLossPercentage numeric(5,2)  null,
   txVIdeoJitter        numeric              null,
   txVIdeoRoundTripTime numeric              null,
   DateLastRecord       timestamp without time zone not null,
   constraint PK_VKSCALLINSTANCESSTATUS primary key (idr)
);

-- set table ownership
alter table dbo.vksCallInstancesStatus owner to dbo
;

/*==============================================================*/
/* Table: vksConferenceCurrent                                  */
/*==============================================================*/
create table dbo.vksConferenceCurrent (
   idr                  SERIAL not null,
   GUID                 VARCHAR(36)          not null,
   OwnerID              INT4                 null,
   OwnerJID             varchar(100)         null,
   CallID               varchar(50)          null,
   URI                  varchar(100)         null,
   StreamURL            varchar(500)         null,
   SecondaryURI         varchar(100)         null,
   Name                 varchar(100)         null,
   Description          varchar(100)         null,
   DefaultLayout        varchar(100)         null,
   AutoGenerated        bool                 not null,
   NonMemberAccess      bool                 null,
   Secret               varchar(100)         null,
   CreateTime           timestamp without time zone null,
   StartTime            timestamp without time zone null,
   EndTime              timestamp without time zone null,
   DateLastRecord       timestamp without time zone not null,
   constraint PK_VKSCONFERENCECURRENT primary key (idr)
);

-- set table ownership
alter table dbo.vksConferenceCurrent owner to dbo
;

/*==============================================================*/
/* Table: vksConferenceHistory                                  */
/*==============================================================*/
create table dbo.vksConferenceHistory (
   idr                  SERIAL not null,
   GUID                 VARCHAR(36)          not null,
   OwnerID              INT4                 null,
   OwnerJID             varchar(100)         null,
   CallID               varchar(50)          null,
   URI                  varchar(100)         null,
   StreamURL            varchar(500)         null,
   SecondaryURI         varchar(100)         null,
   Name                 varchar(100)         null,
   Description          varchar(100)         null,
   DefaultLayout        varchar(100)         null,
   AutoGenerated        bool                 not null,
   NonMemberAccess      bool                 null,
   Secret               varchar(100)         null,
   CreateTime           timestamp without time zone null,
   StartTime            timestamp without time zone null,
   EndTime              timestamp without time zone null,
   DateLastRecord       timestamp without time zone not null,
   constraint PK_VKSCONFERENCEHISTORY primary key (idr)
);

-- set table ownership
alter table dbo.vksConferenceHistory owner to dbo
;

/*==============================================================*/
/* Table: vksLicensing                                          */
/*==============================================================*/
create table dbo.vksLicensing (
   idr                  SERIAL not null,
   PersonalLicenseLimit numeric              null,
   SharedLicenseLimit   numeric              null,
   CapacityUnitLimit    numeric              null,
   Users                numeric              null,
   PersonalLicenses     numeric              null,
   ParticipantsActive   numeric              null,
   CallsActive          numeric              null,
   WeightedCallsActive  numeric(6,3)         null,
   CallsWithoutPersonalLicense numeric       null,
   WeightedCallsWithoutPersonalLicense numeric(6,3)         null,
   CapacityUnitUsage    numeric(6,3)         null,
   CapacityUnitUsageWithoutPersonalLicense numeric(6,3)         null,
   DateLastRecord       timestamp without time zone not null,
   constraint PK_VKSLICENSING primary key (idr)
);

-- set table ownership
alter table dbo.vksLicensing owner to dbo
;

/*==============================================================*/
/* Table: vksListNode                                           */
/*==============================================================*/
create table dbo.vksListNode (
   Idr                  SERIAL not null,
   ServersID            INT4                        not null,
   DateRecord           timestamp without time zone not null,
   Name                 varchar(256)                not null,
   callBridgeId         VARCHAR(36)                 not null,
   Description          varchar(256)                null,
   ipv4                 varchar(50)                 null,
   IsDeleted            bool                        not null,
   constraint PK_VKSLISTNODE primary key (Idr)
);

-- set table ownership
alter table dbo.vksListNode owner to dbo
;

/*==============================================================*/
/* Table: vksParticipants                                       */
/*==============================================================*/
create table dbo.vksParticipants (
   idr                  SERIAL not null,
   GUID                 VARCHAR(36)     not null,
   CallGUID             VARCHAR(36)     null,
   CallBridgeGUID       VARCHAR(36)     null,
   UserJid              varchar(100)    null,
   NameUser             varchar(100)    null,
   URI                  varchar(100)    null,
   OriginalURI          varchar(100)    null,
   NumCallLegs          int2            null,
   isActivator          bool            null,
   CanMove              bool            null,
   DefaultLayout        varchar(20)     null,
   State                varchar(20)     null,
   CameraControlAvailable bool          null,
   DateLastRecord       timestamp without time zone not null,
   constraint PK_VKSPARTICIPANTS primary key (idr)
);

-- set table ownership
alter table dbo.vksParticipants owner to dbo
;

/*==============================================================*/
/* Table: vksServers                                            */
/*==============================================================*/
create table dbo.vksServers (
   idr                  SERIAL not null,
   Name                 varchar(100)         not null,
   RemoteIPAddress      varchar(64)          null,
   Port                 INT4                 null,
   Login                varchar(100)         null,
   Password             varchar(100)         null,
   Mode                 varchar(6)           null,
   APIVersion           varchar(6)           null,
   ResultsSiteName      varchar(1)           null,
   Version              varchar(256)         null,
   VersionSoftware      varchar(256)         null,
   VendorID             INT4                 null,
   ModelID              INT4                 null,
   Enable               bool                 null,
   serversgroupsid      int4                 null,
   constraint PK__vksservers__DC501A7E59E2A0BF primary key (idr)
);

-- set table ownership
alter table dbo.vksServers owner to dbo
;

/*==============================================================*/
/* Index: ix_sites_name                                         */
/*==============================================================*/
create unique index ix_vksservers_name on dbo.vksServers (
Name ASC
);

/*==============================================================*/
/* Table: vksServersCommands                                    */
/*==============================================================*/
create table dbo.vksServersCommands (
   idr                  SERIAL not null,
   serversid            INT4                 not null,
   enable               bool                 not null default true,
   idc                  INT4                 not null,
   serviceid            INT4                 null,
   namec                varchar(100)         not null,
   command              varchar(100)         not null,
   starttime            time                 null,
   collectionfrequency  varchar(50)          null,
   fileextension        varchar(100)         not null,
   folderdata           varchar(100)         not null,
   daterecord           timestamp without time zone not null,
   lastdurms            INT4                 null,
   lastcnt              INT4                 null,
   errorcount           INT4                 null,
   nodeid               INT4                 null,
   constraint PK__vksservcomm__DC501A7E991C45BE primary key (idr)
);

-- set table ownership
alter table dbo.vksServersCommands owner to dbo
;

/*==============================================================*/
/* Index: ix_sitescommands_id3                                  */
/*==============================================================*/
create unique index ix_vksservcomm_id3 on dbo.vksServersCommands (
serversid ASC,
idc ASC,
serviceid ASC,
nodeid ASC
);

/*==============================================================*/
/* Table: vksUsers                                              */
/*==============================================================*/
create table dbo.vksUsers (
   idr                  SERIAL not null,
   GUID                 VARCHAR(36)          null,
   JID                  varchar(100)         not null,
   Name                 varchar(100)         null,
   Phone                varchar(22)          null,
   Org                  varchar(100)         null,
   UserFunction         varchar(100)         null,
   Email                varchar(100)         null,
   UserActive           bool                 null,
   IsDeleted            bool                 null,
   DateLastRecord       timestamp without time zone null,
   ProfileGUID          VARCHAR(36)          null,
   hasLicense           bool                 null,
   constraint PK_VKSUSERS primary key (idr)
);

-- set table ownership
alter table dbo.vksUsers owner to dbo
;

/*==============================================================*/
/* Index: IX_jid                                                */
/*==============================================================*/
create unique index IX_jid on dbo.vksUsers (
JID ASC
);

/*==============================================================*/
/* Table: vksUsersConference                                    */
/*==============================================================*/
create table dbo.vksUsersConference (
   idr                  SERIAL not null,
   UserID               INT4                  not null,
   ConferenceID         INT4                  null,
   constraint PK_VKSUSERSCONFERENCE primary key (idr)
);

-- set table ownership
alter table dbo.vksUsersConference owner to dbo
;

/*==============================================================*/
/* Table: vksUsersOther                                         */
/*==============================================================*/
create table dbo.vksUsersOther (
   idr                  SERIAL not null,
   Name                 varchar(128)         null,
   uri                  varchar(64)          not null,
   email                varchar(128)         null,
   constraint PK_VKSUSERSOTHER primary key (idr)
);

-- set table ownership
alter table dbo.vksUsersOther owner to dbo
;

/*==============================================================*/
/* Table: vksUsersProfiles                                      */
/*==============================================================*/
create table dbo.vksUsersProfiles (
   idr                  SERIAL not null,
   GUID                 VARCHAR(36)           null,
   CanCreateCoSpaces    bool                  null,
   CanCreateCalls       bool                  null,
   CanUseExternalDevices bool                 null,
   CanMakePhoneCalls    bool                  null,
   CanReceiveCalls      bool                  null,
   UserToUserMessagingAllowed bool            null,
   HasLicense           bool                  null,
   DateLastRecord       timestamp without time zone null,
   name                 varchar(128)         null,
   description          varchar(256)         null,
   serversgroupsid      int4                 null,
   constraint PK_VKSUSERSPROFILES primary key (idr)
);

-- set table ownership
alter table dbo.vksUsersProfiles owner to dbo
;

/*==============================================================*/
/* Table: vksVendor                                             */
/*==============================================================*/
create table dbo.vksVendor (
   Idr                  SERIAL not null,
   Name                 varchar(150)         not null,
   Description          varchar(512)         null,
   constraint PK_VKSVENDOR primary key (Idr)
);

-- set table ownership
alter table dbo.vksVendor owner to dbo
;

/*==============================================================*/
/* Table: vksVendorModel                                        */
/*==============================================================*/
create table dbo.vksVendorModel (
   Idr                  SERIAL not null,
   VendorId             INT4                 not null,
   Name                 varchar(150)         not null,
   Description          varchar(512)         null,
   constraint PK_VKSVENDORMODEL primary key (Idr)
);

-- set table ownership
alter table dbo.vksVendorModel owner to dbo
;

/*==============================================================*/
/* Table: perActionList                                         */
/*==============================================================*/
create table dbo.perActionList (
   Idr                  SERIAL not null,
   ViewName             varchar(256)         null,
   PrivateName          varchar(256)         null,
   constraint PK_PERACTIONLIST primary key (Idr)
);

-- set table ownership
alter table dbo.perActionList owner to dbo
;

/*==============================================================*/
/* Table: perObjectToAction                                     */
/*==============================================================*/
create table dbo.perObjectToAction (
   ActionId             int                  not null,
   ObjectId             int                  not null,
   Mask                 int                  not null,
   constraint PK_PEROBJECTTOACTION primary key (ActionId, ObjectId)
);

-- set table ownership
alter table dbo.perObjectToAction owner to dbo
;

/*==============================================================*/
/* Table: perObjectList                                         */
/*==============================================================*/
create table dbo.perObjectList (
   Idr                  SERIAL not null,
   ViewName             varchar(256)         null,
   PrivateName          varchar(128)         null,
   constraint PK_PEROBJECTLIST primary key (Idr)
);

-- set table ownership
alter table dbo.perObjectList owner to dbo
;

/*==============================================================*/
/* Table: perRoleActions                                        */
/*==============================================================*/
create table dbo.perRoleActions (
   RoleMainId           int                  not null,
   ObjectId             int                  not null,
   ActionId             int                  null,
   constraint PK_PERROLEACTIONS primary key (RoleMainId, ObjectId)
);

-- set table ownership
alter table dbo.perRoleActions owner to dbo
;

/*==============================================================*/
/* Table: perRoleMainList                                       */
/*==============================================================*/
create table dbo.perRoleMainList (
   Idr                  SERIAL not null,
   Name                 varchar(128)         null,
   Description          varchar(1024)        null,
   constraint PK_PERROLEMAINLIST primary key (Idr)
);

-- set table ownership
alter table dbo.perRoleMainList owner to dbo
;

/*==============================================================*/
/* Table: perUserToRole                                         */
/*==============================================================*/
create table dbo.perUserToRole (
   RoleMainId           int                  not null,
   AspNetUserId         int                  not null,
   constraint PK_PERUSERTOROLE primary key (RoleMainId, AspNetUserId)
);

-- set table ownership
alter table dbo.perUserToRole owner to dbo
;

/*==============================================================*/
/* Table: AspNetUsers                                           */
/*==============================================================*/
create table dbo.AspNetUsers (
   Id                   SERIAL not null,
   AccessFailedCount    int                 not null,
   ConcurrencyStamp     varchar(4000)       null,
   Email                varchar(256)        null,
   EmailConfirmed       bool                not null,
   LockoutEnabled       bool                not null,
   LockoutEnd           timestamp without time zone null,
   NormalizedEmail      varchar(256)        null,
   NormalizedUserName   varchar(256)        null,
   PasswordHash         varchar(4000)       null,
   PhoneNumber          varchar(4000)       null,
   PhoneNumberConfirmed bool                not null,
   SecurityStamp        varchar(4000)       null,
   TwoFactorEnabled     bool                not null,
   UserName             varchar(256)        null,
   Post                 varchar(256)        null,
   UserFullName         varchar(100)        null,
   SID                  varchar(256)        null,
   Provider             varchar(50)         null,
   APIKey               varchar(255)        null,
   NeedRls              int2                null,
   constraint PK_AspNetUsers primary key (Id)
);

-- set table ownership
alter table dbo.AspNetUsers owner to dbo
;

/*==============================================================*/
/* Index: EmailIndex                                            */
/*==============================================================*/
create index EmailIndex on dbo.AspNetUsers (
NormalizedEmail ASC
);

/*==============================================================*/
/* Index: UserNameIndex                                         */
/*==============================================================*/
create unique index UserNameIndex on dbo.AspNetUsers (
NormalizedUserName ASC
);

/*==============================================================*/
/* Table: AspNetRoles                                           */
/*==============================================================*/
create table dbo.AspNetRoles (
   Id                   SERIAL not null,
   ConcurrencyStamp     varchar(4000)       null,
   Name                 varchar(256)        null,
   NormalizedName       varchar(256)        null,
   ParentId             int                 null,
   ViewName             varchar(256)        null,
   constraint PK_AspNetRoles primary key (Id)
);

-- set table ownership
alter table dbo.AspNetRoles owner to dbo
;

/*==============================================================*/
/* Index: RoleNameIndex                                         */
/*==============================================================*/
create index RoleNameIndex on dbo.AspNetRoles (
NormalizedName ASC
);

/*==============================================================*/
/* Table: AspNetTreePages                                       */
/*==============================================================*/
create table dbo.AspNetTreePages (
   idr                  SERIAL not null,
   ViewName             varchar(300)         null,
   ParentId             int                  null,
   RoleId               int                  null,
   constraint PK_ASPNETTREEPAGES primary key (idr)
);

-- set table ownership
alter table dbo.AspNetTreePages owner to dbo
;

/*==============================================================*/
/* Table: AspNetUserRoles                                       */
/*==============================================================*/
create table dbo.AspNetUserRoles (
   UserId               INT4                 not null,
   RoleId               INT4                 not null,
   constraint PK_AspNetUserRoles primary key (UserId, RoleId)
);

-- set table ownership
alter table dbo.AspNetUserRoles owner to dbo
;
/*==============================================================*/
/* Index: IX_AspNetUserRoles_RoleId                             */
/*==============================================================*/
create  index IX_AspNetUserRoles_RoleId on  dbo.AspNetUserRoles (
RoleId
);

/*==============================================================*/
/* Index: IX_AspNetUserRoles_UserId                             */
/*==============================================================*/
create  index IX_AspNetUserRoles_UserId on  dbo.AspNetUserRoles (
UserId
);

/*==============================================================*/
/* Table: rlsSettingList                                        */
/*==============================================================*/
create table dbo.rlsSettingList (
   Id                   int                  not null,
   Name                 varchar(64)          not null,
   PrivateName          varchar(32)          not null,
   Visible              bool                 null,
   constraint PK_RLSSETTINGLIST primary key (Id)
);

-- set table ownership
alter table dbo.rlsSettingList owner to dbo
;

/*==============================================================*/
/* Table: rlsSettingObjects                                     */
/*==============================================================*/
create table dbo.rlsSettingObjects (
   Id                   int                  not null,
   Name                 varchar(128)         not null,
   PrivateName          varchar(32)          not null,
   SettingListId        int                  not null,
   Visible              bool                 not null default true,
   constraint PK_RLSSETTINGOBJECTS primary key (Id)
);

-- set table ownership
alter table dbo.rlsSettingObjects owner to dbo
;

/*==============================================================*/
/* Index: ix_rlsSettingObjects_SettingListId_PrivateName        */
/*==============================================================*/
create index ix_rlsSettingObjects_SettingListId_PrivateName on dbo.rlsSettingObjects (
PrivateName,
SettingListId
);

/*==============================================================*/
/* Table: rlsLinkUserToObject                                   */
/*==============================================================*/
create table dbo.rlsLinkUserToObject (
   Id                   serial               not null,
   UserId               int                  not null,
   SettingObjectId      int                  not null,
   constraint PK_RLSLINKUSERTOOBJECT primary key (Id)
);

-- set table ownership
alter table dbo.rlsLinkUserToObject owner to dbo
;

/*==============================================================*/
/* Index: ix_rlsLinkUserToObject_SettingObjectId_userId         */
/*==============================================================*/
create index ix_rlsLinkUserToObject_SettingObjectId_userId on dbo.rlsLinkUserToObject (
UserId,
SettingObjectId
);

/*==============================================================*/
/* Table: rlsPermissionToSpace                             */
/*==============================================================*/
create table dbo.rlsPermissionToSpace (
   Id                   serial               not null,
   LinkId               int                  not null,
   SpaceId              int                  not null,
   constraint PK_RLSPERMISSIONTOSPACE primary key (Id)
);

-- set table ownership
alter table dbo.rlsPermissionToSpace owner to dbo
;

/*==============================================================*/
/* Table: rlsSpace                                            */
/*==============================================================*/
create table dbo.rlsSpace (
   UserId               int                  not null,
   SpaceId              int                  not null
);

-- set table ownership
alter table dbo.rlsSpace owner to dbo
;

/*==============================================================*/
/* Index: ix_rlsspace_spaceId_userId                         */
/*==============================================================*/
create index ix_rlsspace_spaceId_userId on dbo.rlsSpace (
UserId,
SpaceId
);

/*==============================================================*/
/* Table: LinkBookingTovksUsersOther                            */
/*==============================================================*/
create table dbo.LinkBookingTovksUsersOther (
   idr                  serial               not null,
   bookingid            int4                 null,
   vksusersotherid      int4                 null,
   constraint PK_LINKBOOKINGTOVKSUSERSOTHER primary key (idr)
);

-- set table ownership
alter table dbo.LinkBookingTovksUsersOther owner to dbo
;

/*==============================================================*/
/* Table: LinkSpaceToParticipant                                */
/*==============================================================*/
create table dbo.LinkSpaceToParticipant (
   id                   serial               not null,
   spaceid              int                  null,
   vksuserid            int                  null,
   calllegprofileguid   varchar(36)          null,
   candestroy           bool                 null,
   canaddremovemember   bool                 null,
   canchangename        bool                 null,
   canchangenonmemberaccessallowed bool      null,
   canchangeuri         bool                 null,
   canchangecallid      bool                 null,
   canchangepasscode    bool                 null,
   canremoveself        bool                 null,
   constraint PK_LINKSPACETOPARTICIPANT primary key (id)
);

-- set table ownership
alter table dbo.LinkSpaceToParticipant owner to dbo
;

/*==============================================================*/
/* Index: ix_linkst_spaceid_vksuserid                           */
/*==============================================================*/
create unique index ix_linkst_spaceid_vksuserid on dbo.LinkSpaceToParticipant (
spaceid ASC,
vksuserid ASC
);

/*==============================================================*/
/* Table: ntfEvents                                             */
/*==============================================================*/
create table dbo.ntfEvents (
   idr                  serial               not null,
   UploadDate           timestamp without time zone null,
   ServiceId            int                  null,
   ProcessingDate       timestamp without time zone null,
   SubscriptionId       int                  null,
   WebPageName          varchar(256)         null,
   OperationInfo        varchar(128)         null,
   Param1               varchar(256)         null,
   Param2               varchar(8000)        null,
   ProcedureName        varchar(50)          null,
   Param3               timestamp without time zone null,
   constraint PK_NTFEVENTS primary key (idr)
);

-- set table ownership
alter table dbo.ntfEvents owner to dbo
;

/*==============================================================*/
/* Table: ntfNotifyLog                                          */
/*==============================================================*/
create table dbo.ntfNotifyLog (
   idr                  serial               not null,
   DateRecord           timestamp without time zone null,
   SubscriptionId       int                  not null,
   Info                 text                 not null,
   EmployeeId           int                  null,
   NotifyTransportTypeId int                 not null,
   ProcessingDate       timestamp without time zone null,
   ErrorMsg             varchar(500)         null,
   AttemptCount         int                  null,
   AttemptDate          timestamp without time zone null,
   Address              varchar(2048)        null,
   NotifyEmail          varchar(4000)        null,
   NotifyPhone          varchar(50)          null,
   ntfEventsId          int                  null,
   InfoSubject          varchar(256)         null,
   constraint PK_NTFNOTIFYLOG primary key (idr)
);

-- set table ownership
alter table dbo.ntfNotifyLog owner to dbo
;

/*==============================================================*/
/* Table: ntfNotifyTemplate                                     */
/*==============================================================*/
create table dbo.ntfNotifyTemplate (
   idr                  serial               not null,
   Name                 varchar(100)          not null,
   PrivateName          varchar(50)         not null,
   Info                 varchar(500)         null,
   ServiceId            int                  not null,
   ProcName             varchar(128)         null,
   ProcedureName        varchar(50)          null,
   WebPageName          varchar(50)          null,
   TemplateHTML         varchar(64)          null,
   constraint PK_NTFNOTIFYTEMPLATE primary key (idr)
);

-- set table ownership
alter table dbo.ntfNotifyTemplate owner to dbo
;

/*==============================================================*/
/* Table: ntfNotifyParam                                        */
/*==============================================================*/
create table dbo.ntfNotifyParam (
   idr                  serial               not null,
   TemplateId           int                  not null,
   Name                 varchar(64)          null,
   DefaultValue         varchar(512)         null,
   Info                 varchar(512)         null,
   ParamName            varchar(32)          null,
   constraint PK_NTFNOTIFYPARAM primary key (idr)
);

-- set table ownership
alter table dbo.ntfNotifyParam owner to dbo
;

/*==============================================================*/
/* Table: ntfSubscription                                       */
/*==============================================================*/
create table dbo.ntfSubscription (
   idr                  serial               not null,
   TemplateId           int                  not null,
   Name                 varchar(50)          not null,
   IsActive             bool                 null,
   constraint PK_NTFSUBSCRIPTION primary key (idr)
);

-- set table ownership
alter table dbo.ntfSubscription owner to dbo
;

/*==============================================================*/
/* Table: ntfSubscriptionParamValue                             */
/*==============================================================*/
create table dbo.ntfSubscriptionParamValue (
   idr                  serial               not null,
   SubscriptionId       int                  not null,
   NotifyParamId        int                  not null,
   Value                varchar(512)         null,
   constraint PK_NTFSUBSCRIPTIONPARAMVALUE primary key (idr)
);

-- set table ownership
alter table dbo.ntfSubscriptionParamValue owner to dbo
;

/*==============================================================*/
/* Table: NotifyTransportType                                   */
/*==============================================================*/
create table dbo.NotifyTransportType (
   Id                   serial               not null,
   Name                 varchar(128)         null,
   PrivateName          varchar(32)          null,
   constraint PK_NOTIFYTRANSPORTTYPE primary key (Id)
);

-- set table ownership
alter table dbo.NotifyTransportType owner to dbo
;

/*==============================================================*/
/* Table: recordingvksusers                                     */
/*==============================================================*/
create table dbo.recordingvksusers (
   idr                  serial               not null,
   recordingid          int                  not null,
   userid               int                  not null,
   daterecord           timestamp without time zone null,
   isplay               bool                 not null,
   isdownload           bool                 not null,
   description          varchar(512)         null,
   constraint PK_RECORDINGVKSUSERS primary key (idr)
);

-- set table ownership
alter table dbo.recordingvksusers owner to dbo
;

/*==============================================================*/
/* Index: ix_recordingvksusers_recordingid_userid               */
/*==============================================================*/
create unique index ix_recordingvksusers_recordingid_userid on dbo.recordingvksusers (
recordingid ASC,
userid ASC
);

/*==============================================================*/
/* Table: recording                                             */
/*==============================================================*/
create table dbo.recording (
   idr                  serial               not null,
   bookingid            int                  not null,
   url                  varchar(256)         not null,
   datestart            timestamp without time zone null,
   dateend              timestamp without time zone null,
   constraint PK_RECORDING primary key (idr)
);

-- set table ownership
alter table dbo.recording owner to dbo
;

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
);

-- set table ownership
alter table dbo.APISMSSettings owner to dbo
;

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
   isusepin             bool                 null,
   schedule             varchar(128)         null,
   spaceid              varchar(256)         null,
   connectiontypeid     varchar(256)         null,
   attemptscount        int                  null,
   delay                int                  null,
   issendnotification   bool                 null,
   issynctoexchange     bool                 null,
   openconferencebefore int                  null,
   isneverusepin        bool                 null,
   dateend              timestamp without time zone null,
   repeatcount          int                  null,
   pinpoliticsid        int                  null,
   pinschedule          varchar(128)         null,
   pindatestart         timestamp without time zone null,
   pincode              varchar(128)         null,
   typeid               int                  null,
   scheduletab          varchar(4)           null,
   pinscheduletab       varchar(4)           null,
   laststart            timestamp without time zone null,
   pinlaststart         timestamp without time zone null,
   rtffiletemplate      text                 null,
   constraint PK_OUTLOOKBOOKINGDEFAULT primary key (id)
);

-- set table ownership
alter table dbo.OutlookBookingDefault owner to dbo
;

/*==============================================================*/
/* Table: nfsServers                                            */
/*==============================================================*/
create table dbo.nfsServers (
   idr                  serial               not null,
   ip                   varchar(32)          not null,
   mount                varchar(128)         not null,
   constraint PK_NFSSERVERS primary key (idr)
);

-- set table ownership
alter table dbo.nfsServers owner to dbo
;
-----------------------------------------------------------------------------
alter table dbo.rlsSettingObjects
   add constraint FK_RLSSETTI_FK_RLSSET_RLSSETTI foreign key (SettingListId)
      references dbo.rlsSettingList (Id)
         on delete cascade;

alter table dbo.UserRoles
   add constraint FK_UserRoles_Role foreign key (RoleId)
      references dbo.Role (Id);

alter table dbo.UserRoles
   add constraint FK_UserRoles_Users foreign key (UserId)
      references dbo.Users (Id);

alter table dbo.vksCallCurrent
   add constraint FK_vksCallCurrent_vksConferenceCurrent foreign key (ConferenceID)
      references dbo.vksConferenceCurrent (idr);

alter table dbo.vksCallHistory
   add constraint FK_vksCallHistory_vksConferenceHistory foreign key (ConferenceID)
      references dbo.vksConferenceHistory (idr);

alter table dbo.vksConferenceCurrent
   add constraint FK_vksConferenceCurrent_vksUsers foreign key (OwnerID)
      references dbo.vksUsers (idr);

alter table dbo.vksConferenceHistory
   add constraint FK_vksConferenceHistory_vksUsers foreign key (OwnerID)
      references dbo.vksUsers (idr);

alter table dbo.vksListNode
   add constraint FK_vksListNode_vksServers foreign key (ServersID)
      references dbo.vksServers (idr);

alter table dbo.vksServersCommands
   add constraint FK_vksServersCommands_Services foreign key (serviceid)
      references dbo.Services (id);

alter table dbo.vksServersCommands
   add constraint FK_vksServersCommands_vksListNode foreign key (nodeid)
      references dbo.vksListNode (Idr);

alter table dbo.vksServersCommands
   add constraint FK_vksServersCommands_vksServer foreign key (serversid)
      references dbo.vksServers (idr);

alter table dbo.vksUsersConference
   add constraint FK_vksUsersConference_vksConferenceCurrent foreign key (ConferenceID)
      references dbo.vksConferenceCurrent (idr);

alter table dbo.vksUsersConference
   add constraint FK_vksUsersConference_vksUsers foreign key (UserID)
      references dbo.vksUsers (idr);

alter table dbo.vksVendorModel
   add constraint FK_vksVendorModel_vksVendor foreign key (VendorId)
      references dbo.vksVendor (Idr);

alter table dbo.perObjectToAction
   add constraint FK_PEROBJEC_REF_PERACTIO2 foreign key (ActionId)
      references dbo.perActionList (Idr);

alter table dbo.perObjectToAction
   add constraint FK_PEROBJEC_REF_PEROBJEC2 foreign key (ObjectId)
      references dbo.perObjectList (Idr);

alter table dbo.perRoleActions
   add constraint FK_PERROLEA_REF_PEROBJEC3 foreign key (ActionId, ObjectId)
      references dbo.perObjectToAction (ActionId, ObjectId);

alter table dbo.perRoleActions
   add constraint FK_ROLEACTION_REF_ROLEMAIN3 foreign key (RoleMainId)
      references dbo.perRoleMainList (Idr)
         on delete cascade;

alter table dbo.perUserToRole
   add constraint FK_PERUSERT_REF_ASPNETUS foreign key (AspNetUserId)
      references dbo.AspNetUsers (Id)
         on delete cascade;

alter table dbo.perUserToRole
   add constraint FK_PERUSERT_REF_PERROLEM foreign key (RoleMainId)
      references dbo.perRoleMainList (Idr)
         on delete cascade;

alter table dbo.AspNetTreePages
   add constraint FK_ASPNETTR_FK_ASPNET_ASPNETTR foreign key (ParentId)
      references dbo.AspNetTreePages (idr);

alter table dbo.AspNetTreePages
   add constraint FK_aspnettreepages_aspnetroles foreign key (RoleId)
      references dbo.AspNetRoles (Id)
         on delete cascade;

alter table dbo.ServersGroups
   add constraint FK_SERVERSG_FK_SERVER_BALANCER foreign key (balanceralgid)
      references dbo.BalancerList (id);

alter table dbo.ServersGroups
   add constraint FK_serversgroups_timezone foreign key (timezoneid)
      references dbo.ServersGroups (id);

alter table dbo.Space
   add constraint FK_space_serversgroups foreign key (serversgroupsid)
      references dbo.ServersGroups (id)
         on delete cascade;

alter table dbo.Space
   add constraint FK_Space_Spacegroups foreign key (spacegroupsid)
      references dbo.Spacegroups (id);

alter table dbo.Booking
   add constraint FK_BOOKING_FK_BOOKIN_CONNECTI foreign key (connectiontypeid)
      references dbo.ConnectionType (id);

alter table dbo.Booking
   add constraint FK_BOOKING_FK_SPACE__SPACE foreign key (spaceid)
      references dbo.Space (id)
         on delete cascade;
		 
alter table dbo.Booking
   add constraint FK_Booking_Vksusers foreign key (ownerid)
      references dbo.vksUsers (idr);
	  
alter table dbo.Booking
   add constraint FK_pinpolitics_booking foreign key (pinpoliticsid)
      references dbo.pinpolitics (idr);

alter table dbo.LinkBookingToParticipant
   add constraint FK_LINKBOOK_FK_LINKBO_BOOKING foreign key (bookingid)
      references dbo.Booking (id)
         on delete cascade;

alter table dbo.LinkBookingToParticipant
   add constraint FK_LINKBOOK_FK_LINKBO_VKSUSERS foreign key (vksparticipantid)
      references dbo.vksUsers (idr);
	  
alter table dbo.LinkBookingTovksUsersOther
   add constraint FK_LINKBOOKINGTOVKSUSERSOTHER_BOOKING foreign key (bookingid)
      references dbo.Booking (id)
         on delete cascade;

alter table dbo.LinkBookingTovksUsersOther
   add constraint FK_LINKBOOKINGTOVKSUSERSOTHER_VKSUSERSOTHER foreign key (vksusersotherid)
      references dbo.vksUsersOther (idr);

alter table dbo.vksUsersProfiles
   add constraint FK_vksUsersProfiles_Serversgroups foreign key (serversgroupsid)
      references dbo.ServersGroups (id);

alter table  dbo.AspNetUserRoles
   add constraint FK_AspNetUserRoles_AspNetRoles_RoleId foreign key (RoleId)
      references  dbo.AspNetRoles (Id)
      on delete cascade;

alter table  dbo.AspNetUserRoles
   add constraint FK_AspNetUserRoles_AspNetUsers_UserId foreign key (UserId)
      references  dbo.AspNetUsers (Id)
      on delete cascade;

alter table dbo.Booking
   add constraint FK_BOOKING_FK_BOOKIN_BOOKINGT foreign key (typeid)
      references dbo.bookingtype (idr);

alter table dbo.Booking
   add constraint FK_BOOKING_FK_BOOKIN_TIMEZONE foreign key (timezone)
      references dbo.timezone (idr);

alter table dbo.Space
   add constraint FK_SPACE_FK_SPACE__VKSUSERS foreign key (ownerid)
      references dbo.vksUsers (idr);

alter table dbo.BookingStatus
   add constraint FK_BookingStatus_Booking foreign key (BookingId)
      references dbo.Booking (id);

alter table dbo.BookingStatusPin
   add constraint FK_BookingStatusPin_Booking foreign key (BookingId)
      references dbo.Booking (id)
         on delete cascade;

alter table dbo.rlsLinkUserToObject
   add constraint FK_RLSLINKU_FK_RLSLIN_RLSSETTI foreign key (SettingObjectId)
      references dbo.rlsSettingObjects (Id)
         on delete cascade;

alter table dbo.rlsLinkUserToObject
   add constraint FK_RLSLINKU_FK_RLSLIN_ASPNETUS foreign key (UserId)
      references dbo.AspNetUsers (Id)
         on delete cascade;

alter table dbo.rlsPermissionToSpace
   add constraint FK_rlspermissiontospace_rlslinkusertoobject foreign key (LinkId)
      references dbo.rlsLinkUserToObject (Id)
         on delete cascade;

alter table dbo.rlsPermissionToSpace
   add constraint FK_rlspermissiontospace_space foreign key (SpaceId)
      references dbo.Space (id);

alter table dbo.LinkSpaceToParticipant
   add constraint FK_LINKSPAC_FK_LINKSP_SPACE foreign key (spaceid)
      references dbo.Space (id)
         on delete cascade;

alter table dbo.LinkSpaceToParticipant
   add constraint FK_LINKSPAC_FK_LINKSP_VKSUSERS foreign key (vksuserid)
      references dbo.vksUsers (idr);

alter table dbo.ntfEvents
   add constraint FK_ntfevents_services foreign key (ServiceId)
      references dbo.Services (id);

alter table dbo.ntfNotifyLog
   add constraint FK_NTFNOTIFYLOG_NTFEVENTS foreign key (ntfEventsId)
      references dbo.ntfEvents (idr);

alter table dbo.ntfNotifyParam
   add constraint FK_ntfrnotifyparam_ntfnotifytemplate foreign key (TemplateId)
      references dbo.ntfNotifyTemplate (idr)
         on delete cascade;

alter table dbo.ntfSubscription
   add constraint FK_ntfsubscription_ntfnotifytemplate foreign key (TemplateId)
      references dbo.ntfNotifyTemplate (idr)
         on delete cascade;
		 
alter table dbo.ntfSubscriptionParamValue
   add constraint FK_ntfsubscriptionparamvalue_ntfnotifyparam foreign key (NotifyParamId)
      references dbo.ntfNotifyParam (idr);

alter table dbo.ntfSubscriptionParamValue
   add constraint FK_ntfsubscriptionparamvalue_ntfsubscription foreign key (SubscriptionId)
      references dbo.ntfSubscription (idr)
         on delete cascade;

alter table dbo.recording
   add constraint FK_recording_booking foreign key (bookingid)
      references dbo.Booking (id);

alter table dbo.recordingvksusers
   add constraint FK_recordingvksuser_aspnetusers foreign key (userid)
      references dbo.AspNetUsers (Id);

alter table dbo.recordingvksusers
   add constraint FK_recordingvksuser_recording foreign key (recordingid)
      references dbo.recording (idr);
