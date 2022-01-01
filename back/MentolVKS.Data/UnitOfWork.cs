using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;

namespace MentolVKS.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Инициализация экземпляра класса UnitOfWork
        /// </summary>
        public UnitOfWork(
            IAgGridVueRepository agGridVueRepository,
            IAspNetRoleRepository aspNetRoleRepository,
            IAspNetTreePageRepository aspNetTreePageRepository,
            IAspNetUserRepository aspNetUserRepository,
            IAspNetUserRoleRepository aspNetUserRoleRepository,
            IBookingRepository bookingRepository,
            IBookingTypeRepository bookingTypeRepository,
            IBookingViewRepository bookingViewRepository,
            IColumnForIntegerFilterRepository columnForIntegerFilterRepository,
            IColumnForStringFilterRepository columnForStringFilterRepository,
            IConnectionTypeRepository connectionTypeRepository,
            IFilterColumnRepository filterColumnRepository,
            IFilterColumnsListRepository filterColumnsListRepository,
            IFilterForColumnTypeListRepository filterForColumnTypeListRepository,
            IFilterNameRepository filterNameRepository,
            IFilterOperationsListRepository filterOperationsListRepository,
            IFiltersListRepository filtersListRepository,
            IFiltersToUserLinkRepository filtersToUserLinkRepository,
            IFilterTablesListRepository filterTablesListRepository,
            IFilterValueRepository filterValueRepository,
            ILicenseXmlRepository licenseXmlRepository,
            ILinkBookingToParticipantRepository linkBookingToParticipantRepository,
            ILinkBookingTovksUsersOthersRepository linkBookingTovksUsersOthersRepository,
            ILinkSpaceToParticipantRepository linkSpaceToParticipantRepository,
            ILogsRepository logsRepository,
            ILogsTypeRepository logsTypeRepository,
            INfsServersRepository nfsServersRepository,
            INtfEventsRepository ntfEventsRepository,
            IOutlookBookingDefaultRepository outlookBookingDefaultRepository,
            IPinPoliticsRepository pinPoliticsRepository,
            IProductsRepository productsRepository,
            IRecordingRepository recordingRepository,
            IRecordingsViewRepository recordingsViewRepository,
            IRecordingVksUsersRepository recordingVksUsersRepository,
            IRecordingVksUsersViewRepository recordingVksUsersViewRepository,
            IRefreshLogRepository refreshLogRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IServersGroupsRepository serversGroupsRepository,
            IServersGroupViewRepository serversGroupViewRepository,
            IServicesRepository servicesRepository,
            ISpaceGroupsRepository spaceGroupsRepository,
            ISpaceRepository spaceRepository,
            ISpacesViewRepository spacesViewRepository,
            ISpaceUserRightsViewRepository spaceUserRightsViewRepository,
            ITableColumnSettingsRepository tableColumnSettingsRepository,
            ITimeZoneRepository timeZoneRepository,
            IUserTableColumnRepository userTableColumnRepository,
            IVksCallCurrentRepository vksCallCurrentRepository,
            IVksCallHistoryRepository vksCallHistoryRepository,
            IVksCallInstanceRepository vksCallInstanceRepository,
            IVksCallInstancesConfigRepository vksCallInstancesConfigRepository,
            IVksCallInstancesStatusRepository vksCallInstancesStatusRepository,
            IVksConferenceCurrentRepository vksConferenceCurrentRepository,
            IVksConferenceHistoryRepository vksConferenceHistoryRepository,
            IVksLicensingRepository vksLicensingRepository,
            IVksListNodeRepository vksListNodeRepository,
            IVksParticipantRepository vksParticipantRepository,
            IVksServerRepository vksServerRepository,
            IVksServersCommandRepository vksServersCommandRepository,
            IVksServersViewRepository vksServersViewRepository,
            IVksUserProfilesViewRepository vksUserProfilesViewRepository,
            IVksUserRepository vksUserRepository,
            IVksUsersConferenceRepository vksUsersConferenceRepository,
            IVksUsersOtherRepository vksUsersOtherRepository,
            IVksUsersProfileRepository vksUsersProfileRepository,
            IVksVendorModelRepository vksVendorModelRepository,
            IVksVendorRepository vksVendorRepository
            )
        {
            AgGridVueRepository = agGridVueRepository;
            AspNetRoleRepository = aspNetRoleRepository;
            AspNetTreePageRepository = aspNetTreePageRepository;
            AspNetUserRepository = aspNetUserRepository;
            AspNetUserRoleRepository = aspNetUserRoleRepository;
            BookingRepository = bookingRepository;
            BookingTypeRepository = bookingTypeRepository;
            BookingViewRepository = bookingViewRepository;
            ColumnForIntegerFilterRepository = columnForIntegerFilterRepository;
            ColumnForStringFilterRepository = columnForStringFilterRepository;
            ConnectionTypeRepository = connectionTypeRepository;
            FilterColumnRepository = filterColumnRepository;
            FilterColumnsListRepository = filterColumnsListRepository;
            FilterForColumnTypeListRepository = filterForColumnTypeListRepository;
            FilterNameRepository = filterNameRepository;
            FilterOperationsListRepository = filterOperationsListRepository;
            FiltersListRepository = filtersListRepository;
            FiltersToUserLinkRepository = filtersToUserLinkRepository;
            FilterTablesListRepository = filterTablesListRepository;
            FilterValueRepository = filterValueRepository;
            LicenseXmlRepository = licenseXmlRepository;
            LinkBookingToParticipantRepository = linkBookingToParticipantRepository;
            LinkBookingTovksUsersOthersRepository = linkBookingTovksUsersOthersRepository;
            LinkSpaceToParticipantRepository = linkSpaceToParticipantRepository;
            LogsRepository = logsRepository;
            LogsTypeRepository = logsTypeRepository;
            NfsServersRepository = nfsServersRepository;
            NtfEventsRepository = ntfEventsRepository;
            OutlookBookingDefaultRepository = outlookBookingDefaultRepository;
            PinPoliticsRepository = pinPoliticsRepository;
            ProductsRepository = productsRepository;
            RecordingRepository = recordingRepository;
            RecordingsViewRepository = recordingsViewRepository;
            RecordingVksUsersRepository = recordingVksUsersRepository;
            RecordingVksUsersViewRepository = recordingVksUsersViewRepository;
            RefreshLogRepository = refreshLogRepository;
            RefreshTokenRepository = refreshTokenRepository;
            ServersGroupsRepository = serversGroupsRepository;
            ServersGroupViewRepository = serversGroupViewRepository;
            ServicesRepository = servicesRepository;
            SpaceGroupsRepository = spaceGroupsRepository;
            SpaceRepository = spaceRepository;
            SpacesViewRepository = spacesViewRepository;
            SpaceUserRightsViewRepository = spaceUserRightsViewRepository;
            TableColumnSettingsRepository = tableColumnSettingsRepository;
            TimeZoneRepository = timeZoneRepository;
            UserTableColumnRepository = userTableColumnRepository;
            VksCallCurrentRepository = vksCallCurrentRepository;
            VksCallHistoryRepository = vksCallHistoryRepository;
            VksCallInstanceRepository = vksCallInstanceRepository;
            VksCallInstancesConfigRepository = vksCallInstancesConfigRepository;
            VksCallInstancesStatusRepository = vksCallInstancesStatusRepository;
            VksConferenceCurrentRepository = vksConferenceCurrentRepository;
            VksConferenceHistoryRepository = vksConferenceHistoryRepository;
            VksLicensingRepository = vksLicensingRepository;
            VksListNodeRepository = vksListNodeRepository;
            VksParticipantRepository = vksParticipantRepository;
            VksServerRepository = vksServerRepository;
            VksServersCommandRepository = vksServersCommandRepository;
            VksServersViewRepository = vksServersViewRepository;
            VksUserProfilesViewRepository = vksUserProfilesViewRepository;
            VksUserRepository = vksUserRepository;
            VksUsersConferenceRepository = vksUsersConferenceRepository;
            VksUsersOtherRepository = vksUsersOtherRepository;
            VksUsersProfileRepository = vksUsersProfileRepository;
            VksVendorModelRepository = vksVendorModelRepository;
            VksVendorRepository = vksVendorRepository;
        }

        public IAgGridVueRepository AgGridVueRepository { get; }
        public IAspNetRoleRepository AspNetRoleRepository { get; }
        public IAspNetTreePageRepository AspNetTreePageRepository { get; }
        public IAspNetUserRepository AspNetUserRepository { get; }
        public IAspNetUserRoleRepository AspNetUserRoleRepository { get; }
        public IBookingRepository BookingRepository { get; }
        public IBookingTypeRepository BookingTypeRepository { get; }
        public IBookingViewRepository BookingViewRepository { get; }
        public IColumnForIntegerFilterRepository ColumnForIntegerFilterRepository { get; }
        public IColumnForStringFilterRepository ColumnForStringFilterRepository { get; }
        public IConnectionTypeRepository ConnectionTypeRepository { get; }
        public IFilterColumnRepository FilterColumnRepository { get; }
        public IFilterColumnsListRepository FilterColumnsListRepository { get; }
        public IFilterForColumnTypeListRepository FilterForColumnTypeListRepository { get; }
        public IFilterNameRepository FilterNameRepository { get; }
        public IFilterOperationsListRepository FilterOperationsListRepository { get; }
        public IFiltersListRepository FiltersListRepository { get; }
        public IFiltersToUserLinkRepository FiltersToUserLinkRepository { get; }
        public IFilterTablesListRepository FilterTablesListRepository { get; }
        public IFilterValueRepository FilterValueRepository { get; }
        public ILicenseXmlRepository LicenseXmlRepository { get; }
        public ILinkBookingToParticipantRepository LinkBookingToParticipantRepository { get; }
        public ILinkBookingTovksUsersOthersRepository LinkBookingTovksUsersOthersRepository { get; }
        public ILinkSpaceToParticipantRepository LinkSpaceToParticipantRepository { get; }
        public ILogsRepository LogsRepository { get; }
        public ILogsTypeRepository LogsTypeRepository { get; }
        public INfsServersRepository NfsServersRepository { get; }
        public INtfEventsRepository NtfEventsRepository { get; }
        public IOutlookBookingDefaultRepository OutlookBookingDefaultRepository { get; }
        public IPinPoliticsRepository PinPoliticsRepository { get; }
        public IProductsRepository ProductsRepository { get; }
        public IRecordingRepository RecordingRepository { get; }
        public IRecordingsViewRepository RecordingsViewRepository { get; }
        public IRecordingVksUsersRepository RecordingVksUsersRepository { get; }
        public IRecordingVksUsersViewRepository RecordingVksUsersViewRepository { get; }
        public IRefreshLogRepository RefreshLogRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IServersGroupsRepository ServersGroupsRepository { get; }
        public IServersGroupViewRepository ServersGroupViewRepository { get; }
        public IServicesRepository ServicesRepository { get; }
        public ISpaceGroupsRepository SpaceGroupsRepository { get; }
        public ISpaceRepository SpaceRepository { get; }
        public ISpacesViewRepository SpacesViewRepository { get; }
        public ISpaceUserRightsViewRepository SpaceUserRightsViewRepository { get; }
        public ITableColumnSettingsRepository TableColumnSettingsRepository { get; }
        public ITimeZoneRepository TimeZoneRepository { get; }
        public IUserTableColumnRepository UserTableColumnRepository { get; }
        public IVksCallCurrentRepository VksCallCurrentRepository { get; }
        public IVksCallHistoryRepository VksCallHistoryRepository { get; }
        public IVksCallInstanceRepository VksCallInstanceRepository { get; }
        public IVksCallInstancesConfigRepository VksCallInstancesConfigRepository { get; }
        public IVksCallInstancesStatusRepository VksCallInstancesStatusRepository { get; }
        public IVksConferenceCurrentRepository VksConferenceCurrentRepository { get; }
        public IVksConferenceHistoryRepository VksConferenceHistoryRepository { get; }
        public IVksLicensingRepository VksLicensingRepository { get; }
        public IVksListNodeRepository VksListNodeRepository { get; }
        public IVksParticipantRepository VksParticipantRepository { get; }
        public IVksServerRepository VksServerRepository { get; }
        public IVksServersCommandRepository VksServersCommandRepository { get; }
        public IVksServersViewRepository VksServersViewRepository { get; }
        public IVksUserProfilesViewRepository VksUserProfilesViewRepository { get; }
        public IVksUserRepository VksUserRepository { get; }
        public IVksUsersConferenceRepository VksUsersConferenceRepository { get; }
        public IVksUsersOtherRepository VksUsersOtherRepository { get; }
        public IVksUsersProfileRepository VksUsersProfileRepository { get; }
        public IVksVendorModelRepository VksVendorModelRepository { get; }
        public IVksVendorRepository VksVendorRepository { get; }
    }
}
