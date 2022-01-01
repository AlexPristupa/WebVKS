using System;
using MentolVKS.Data.EF.Classes;
using MentolVKS.Data.EF.Configuration.ColumnMapping;
using MentolVKS.Data.EF.Repository;
using MentolVKS.Data.EF.Settings;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MentolVKS.Data.EF.DependencyInjection
{
    public static class EFServiceCollectionExtension
    {
        public static void AddEFRepositories(this IServiceCollection services, DatabaseSettings settings)
        {
            services.AddDbContext<DataContext>(options =>
            {
                switch (settings.DbType)
                {
                    case DbType.MsSql:
                        options.UseSqlServer(settings.ConnectionString, builder => builder.CommandTimeout(settings.CommandTimeout == 0 ? (int?)null : settings.CommandTimeout));

                        // TODO!!! .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                        break;
                    case DbType.PostgreSQL:
                        options.UseNpgsql(settings.ConnectionString, builder => builder.CommandTimeout(settings.CommandTimeout == 0 ? (int?)null : settings.CommandTimeout));

                        // TODO!!! .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                        break;
                    default:
                        throw new ArgumentException($"Не правильно задан тип БД в конфиг файле appsettings.json. Допустимые значения: {string.Join(", ", typeof(DbType).GetEnumNames())}");
                }

                options.EnableSensitiveDataLogging(settings.EnableSensitiveDataLogging);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddTransient<IAgGridVueRepository, AgGridVueRepository>();
            services.AddTransient<IAspNetRoleRepository, AspNetRoleRepository>();
            services.AddTransient<IAspNetTreePageRepository, AspNetTreePageRepository>();
            services.AddTransient<IAspNetUserRepository, AspNetUserRepository>();
            services.AddTransient<IAspNetUserRoleRepository, AspNetUserRoleRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IBookingTypeRepository, BookingTypeRepository>();
            services.AddTransient<IBookingViewRepository, BookingViewRepository>();
            services.AddTransient<IColumnForIntegerFilterRepository, ColumnForIntegerFilterRepository>();
            services.AddTransient<IColumnForStringFilterRepository, ColumnForStringFilterRepository>();
            services.AddTransient<IColumnMappingConfiguration, ColumnMappingConfiguration>();
            services.AddTransient<IConnectionTypeRepository, ConnectionTypeRepository>();
            services.AddTransient<IFilterColumnRepository, FilterColumnRepository>();
            services.AddTransient<IFilterColumnsListRepository, FilterColumnsListRepository>();
            services.AddTransient<IFilterForColumnTypeListRepository, FilterForColumnTypeListRepository>();
            services.AddTransient<IFilterNameRepository, FilterNameRepository>();
            services.AddTransient<IFilterOperationsListRepository, FilterOperationsListRepository>();
            services.AddTransient<IFiltersListRepository, FiltersListRepository>();
            services.AddTransient<IFiltersToUserLinkRepository, FiltersToUserLinkRepository>();
            services.AddTransient<IFilterTablesListRepository, FilterTablesListRepository>();
            services.AddTransient<IFilterValueRepository, FilterValueRepository>();
            services.AddTransient<ILicenseXmlRepository, LicenseXmlRepository>();
            services.AddTransient<ILinkBookingToParticipantRepository, LinkBookingToParticipantRepository>();
            services.AddTransient<ILinkBookingTovksUsersOthersRepository, LinkBookingTovksUsersOthersRepository>();
            services.AddTransient<ILinkSpaceToParticipantRepository, LinkSpaceToParticipantRepository>();
            services.AddTransient<ILogsRepository, LogsRepository>();
            services.AddTransient<ILogsTypeRepository, LogsTypeRepository>();
            services.AddTransient<INfsServersRepository, NfsServersRepository>();
            services.AddTransient<INtfEventsRepository, NtfEventsRepository>();
            services.AddTransient<IOutlookBookingDefaultRepository, OutlookBookingDefaultRepository>();
            services.AddTransient<IPinPoliticsRepository, PinPoliticsRepository>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<IQueryLinqFactory, QueryLinqFactory>();
            services.AddTransient<IRecordingRepository, RecordingRepository>();
            services.AddTransient<IRecordingsViewRepository, RecordingsViewRepository>();
            services.AddTransient<IRecordingVksUsersRepository, RecordingVksUsersRepository>();
            services.AddTransient<IRecordingVksUsersViewRepository, RecordingVksUsersViewRepository>();
            services.AddTransient<IRefreshLogRepository, RefreshLogRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IServersGroupsRepository, ServersGroupsRepository>();
            services.AddTransient<IServersGroupViewRepository, ServersGroupViewRepository>();
            services.AddTransient<IServicesRepository, ServicesRepository>();
            services.AddTransient<ISpaceGroupsRepository, SpaceGroupsRepository>();
            services.AddTransient<ISpaceRepository, SpaceRepository>();
            services.AddTransient<ISpacesViewRepository, SpacesViewRepository>();
            services.AddTransient<ISpaceUserRightsViewRepository, SpaceUserRightsViewRepository>();
            services.AddTransient<ITableColumnSettingsRepository, TableColumnSettingsRepository>();
            services.AddTransient<ITimeZoneRepository, TimeZoneRepository>();
            services.AddTransient<IUserTableColumnRepository, UserTableColumnRepository>();
            services.AddTransient<IVksCallCurrentRepository, VksCallCurrentRepository>();
            services.AddTransient<IVksCallHistoryRepository, VksCallHistoryRepository>();
            services.AddTransient<IVksCallInstanceRepository, VksCallInstanceRepository>();
            services.AddTransient<IVksCallInstancesConfigRepository, VksCallInstancesConfigRepository>();
            services.AddTransient<IVksCallInstancesStatusRepository, VksCallInstancesStatusRepository>();
            services.AddTransient<IVksConferenceCurrentRepository, VksConferenceCurrentRepository>();
            services.AddTransient<IVksConferenceHistoryRepository, VksConferenceHistoryRepository>();
            services.AddTransient<IVksLicensingRepository, VksLicensingRepository>();
            services.AddTransient<IVksListNodeRepository, VksListNodeRepository>();
            services.AddTransient<IVksParticipantRepository, VksParticipantRepository>();
            services.AddTransient<IVksServerRepository, VksServerRepository>();
            services.AddTransient<IVksServersCommandRepository, VksServersCommandRepository>();
            services.AddTransient<IVksServersViewRepository, VksServersViewRepository>();
            services.AddTransient<IVksUserProfilesViewRepository, VksUserProfilesViewRepository>();
            services.AddTransient<IVksUserRepository, VksUserRepository>();
            services.AddTransient<IVksUsersConferenceRepository, VksUsersConferenceRepository>();
            services.AddTransient<IVksUsersOtherRepository, VksUsersOtherRepository>();
            services.AddTransient<IVksUsersProfileRepository, VksUsersProfileRepository>();
            services.AddTransient<IVksVendorModelRepository, VksVendorModelRepository>();
            services.AddTransient<IVksVendorRepository, VksVendorRepository>();
        }
    }
}
