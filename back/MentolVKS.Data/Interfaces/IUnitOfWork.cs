using MentolVKS.Data.Interfaces.Repository;

namespace MentolVKS.Data.Interfaces
{
    /// <summary>
    /// Интерфейс для паттерна Unit of Work
    /// </summary>
    public interface IUnitOfWork
    {
        IAgGridVueRepository AgGridVueRepository { get; }
        IAspNetRoleRepository AspNetRoleRepository { get; }
        IAspNetTreePageRepository AspNetTreePageRepository { get; }
        IAspNetUserRepository AspNetUserRepository { get; }
        IAspNetUserRoleRepository AspNetUserRoleRepository { get; }
        IBookingRepository BookingRepository { get; }
        IBookingTypeRepository BookingTypeRepository { get; }
        IBookingViewRepository BookingViewRepository { get; }
        IColumnForIntegerFilterRepository ColumnForIntegerFilterRepository { get; }
        IColumnForStringFilterRepository ColumnForStringFilterRepository { get; }
        IConnectionTypeRepository ConnectionTypeRepository { get; }
        IFilterColumnRepository FilterColumnRepository { get; }
        IFilterColumnsListRepository FilterColumnsListRepository { get; }
        IFilterForColumnTypeListRepository FilterForColumnTypeListRepository { get; }
        IFilterNameRepository FilterNameRepository { get; }
        IFilterOperationsListRepository FilterOperationsListRepository { get; }
        IFiltersListRepository FiltersListRepository { get; }
        IFiltersToUserLinkRepository FiltersToUserLinkRepository { get; }
        IFilterTablesListRepository FilterTablesListRepository { get; }
        IFilterValueRepository FilterValueRepository { get; }
        ILicenseXmlRepository LicenseXmlRepository { get; }
        ILinkBookingToParticipantRepository LinkBookingToParticipantRepository { get; }
        ILinkBookingTovksUsersOthersRepository LinkBookingTovksUsersOthersRepository { get; }
        ILinkSpaceToParticipantRepository LinkSpaceToParticipantRepository { get; }
        ILogsRepository LogsRepository { get; }
        ILogsTypeRepository LogsTypeRepository { get; }
        INfsServersRepository NfsServersRepository { get; }
        INtfEventsRepository NtfEventsRepository { get; }
        IOutlookBookingDefaultRepository OutlookBookingDefaultRepository { get; }
        IPinPoliticsRepository PinPoliticsRepository { get; }
        IProductsRepository ProductsRepository { get; }
        IRecordingRepository RecordingRepository { get; }
        IRecordingsViewRepository RecordingsViewRepository { get; }
        IRecordingVksUsersRepository RecordingVksUsersRepository { get; }
        IRecordingVksUsersViewRepository RecordingVksUsersViewRepository { get; }
        IRefreshLogRepository RefreshLogRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        IServersGroupsRepository ServersGroupsRepository { get; }
        IServersGroupViewRepository ServersGroupViewRepository { get; }
        IServicesRepository ServicesRepository { get; }
        ISpaceGroupsRepository SpaceGroupsRepository { get; }
        ISpaceRepository SpaceRepository { get; }
        ISpacesViewRepository SpacesViewRepository { get; }
        ISpaceUserRightsViewRepository SpaceUserRightsViewRepository { get; }
        ITableColumnSettingsRepository TableColumnSettingsRepository { get; }
        ITimeZoneRepository TimeZoneRepository { get; }
        IUserTableColumnRepository UserTableColumnRepository { get; }
        IVksCallCurrentRepository VksCallCurrentRepository { get; }
        IVksCallHistoryRepository VksCallHistoryRepository { get; }
        IVksCallInstanceRepository VksCallInstanceRepository { get; }
        IVksCallInstancesConfigRepository VksCallInstancesConfigRepository { get; }
        IVksCallInstancesStatusRepository VksCallInstancesStatusRepository { get; }
        IVksConferenceCurrentRepository VksConferenceCurrentRepository { get; }
        IVksConferenceHistoryRepository VksConferenceHistoryRepository { get; }
        IVksLicensingRepository VksLicensingRepository { get; }
        IVksListNodeRepository VksListNodeRepository { get; }
        IVksParticipantRepository VksParticipantRepository { get; }
        IVksServerRepository VksServerRepository { get; }
        IVksServersCommandRepository VksServersCommandRepository { get; }
        IVksServersViewRepository VksServersViewRepository { get; }
        IVksUserProfilesViewRepository VksUserProfilesViewRepository { get; }
        IVksUserRepository VksUserRepository { get; }
        IVksUsersConferenceRepository VksUsersConferenceRepository { get; }
        IVksUsersOtherRepository VksUsersOtherRepository { get; }
        IVksUsersProfileRepository VksUsersProfileRepository { get; }
        IVksVendorModelRepository VksVendorModelRepository { get; }
        IVksVendorRepository VksVendorRepository { get; }
    }
}
