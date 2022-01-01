using System;
using System.Data;
using System.Text.RegularExpressions;
using MentolVKS.Data.EF.Configuration;
using MentolVKS.Data.EF.Settings;
using MentolVKS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Npgsql;
using Npgsql.NameTranslation;

namespace MentolVKS.Data.EF
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// Настройки БД
        /// </summary>
        private readonly DatabaseSettings _databaseSettings;

        /// <summary>
        /// Тип БД
        /// </summary>
        public Model.Enums.DbType DataBaseType => _databaseSettings.DbType;

        /// <summary>
        /// Имя пользователя для RLS
        /// </summary>
        private readonly IUserInterface _userName;


        /// <summary>
        /// Безопасность на уровне строк
        /// </summary>
        public bool EnableRls => _databaseSettings.EnableRls;

        public DataContext(DbContextOptions<DataContext> options, IOptions<DatabaseSettings> databaseSettings, IUserInterface userName)
            : base(options)
        {
            _databaseSettings = databaseSettings.Value;
            _userName = userName;
            Init();
        }

        private void Init()
        {
            if (EnableRls)
            {
                Database.GetDbConnection().StateChange += ConnectionStateChange;
            }
        }

        /// <summary>
        /// Событие изменение статуса подключения для RSL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionStateChange(object sender, StateChangeEventArgs e)
        {
            if (e.CurrentState != ConnectionState.Open) return;

            var userName = _userName.GetUserName();

            if (string.IsNullOrEmpty(userName)) return;

            using var cmd = Database.GetDbConnection().CreateCommand();

            var sql = $@"declare @id INTEGER;
                         set @id = (select TOP 1 Id from dbo.AspNetUsers where upper(UserName) like UPPER('{userName}'))
                         EXEC sp_set_session_context @key=N'UserId', @value = @id";

            if (Database.IsNpgsql())
            {
                sql = $@"DO $$
                            declare
                                v_id INT = null;
                            BEGIN
                                select Id into v_id from dbo.AspNetUsers where upper(UserName) like UPPER('{userName}') limit 1;
                                perform dbo.set_session_context_userid(v_id::text);
                            END;
                        $$;";
            }

            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AspNetRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AspNetTreePageConfiguration());
            modelBuilder.ApplyConfiguration(new AspNetUserConfiguration());
            modelBuilder.ApplyConfiguration(new AspNetUserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new BookingTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookingViewConfiguration());
            modelBuilder.ApplyConfiguration(new ColumnForIntegerFilterConfiguration());
            modelBuilder.ApplyConfiguration(new ColumnForStringFilterConfiguration());
            modelBuilder.ApplyConfiguration(new ConnectionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FilterColumnConfiguration());
            modelBuilder.ApplyConfiguration(new FilterColumnsListConfiguration());
            modelBuilder.ApplyConfiguration(new FilterForColumnTypeListConfiguration());
            modelBuilder.ApplyConfiguration(new FilterNameConfiguration());
            modelBuilder.ApplyConfiguration(new FilterOperationsListConfiguration());
            modelBuilder.ApplyConfiguration(new FiltersListConfiguration());
            modelBuilder.ApplyConfiguration(new FiltersToUserLinkConfiguration());
            modelBuilder.ApplyConfiguration(new FilterTablesListConfiguration());
            modelBuilder.ApplyConfiguration(new FilterValueConfiguration());
            modelBuilder.ApplyConfiguration(new LicenseXmlConfiguration());
            modelBuilder.ApplyConfiguration(new LinkBookingToParticipantConfiguration());
            modelBuilder.ApplyConfiguration(new LinkBookingTovksUsersOtherConfiguration());
            modelBuilder.ApplyConfiguration(new LinkSpaceToParticipantConfiguration());
            modelBuilder.ApplyConfiguration(new LogsConfiguration());
            modelBuilder.ApplyConfiguration(new LogsTypeConfiguration());
            modelBuilder.ApplyConfiguration(new NfsServersConfiguration());
            modelBuilder.ApplyConfiguration(new NtfEventsConfiguration());
            modelBuilder.ApplyConfiguration(new OutlookBookingDefaultConfiguration());
            modelBuilder.ApplyConfiguration(new PinPoliticsConfiguration());
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            modelBuilder.ApplyConfiguration(new RecordingConfiguration());
            modelBuilder.ApplyConfiguration(new RecordingsViewConfiguration());
            modelBuilder.ApplyConfiguration(new RecordingVksUsersConfiguration());
            modelBuilder.ApplyConfiguration(new RecordingVksUsersViewConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshLogConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new ServersGroupsConfiguration());
            modelBuilder.ApplyConfiguration(new ServersGroupViewConfiguration());
            modelBuilder.ApplyConfiguration(new ServicesConfiguration());
            modelBuilder.ApplyConfiguration(new SpaceConfiguration());
            modelBuilder.ApplyConfiguration(new SpaceGroupsConfiguration());
            modelBuilder.ApplyConfiguration(new SpacesViewConfiguration());
            modelBuilder.ApplyConfiguration(new SpaceUserRightsViewConfiguration());
            modelBuilder.ApplyConfiguration(new TableColumnSettingsConfiguration());
            modelBuilder.ApplyConfiguration(new TimeZoneConfiguration());
            modelBuilder.ApplyConfiguration(new UserTableColumnConfiguration());
            modelBuilder.ApplyConfiguration(new VksCallCurrentConfiguration());
            modelBuilder.ApplyConfiguration(new VksCallHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new VksCallInstanceConfiguration());
            modelBuilder.ApplyConfiguration(new VksCallInstancesConfigConfiguration());
            modelBuilder.ApplyConfiguration(new VksCallInstancesStatusConfiguration());
            modelBuilder.ApplyConfiguration(new VksConferenceCurrentConfiguration());
            modelBuilder.ApplyConfiguration(new VksConferenceHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new VksLicensingConfiguration());
            modelBuilder.ApplyConfiguration(new VksListNodeConfiguration());
            modelBuilder.ApplyConfiguration(new VksParticipantConfiguration());
            modelBuilder.ApplyConfiguration(new VksServerConfiguration());
            modelBuilder.ApplyConfiguration(new VksServersCommandConfiguration());
            modelBuilder.ApplyConfiguration(new VksServersViewConfiguration());
            modelBuilder.ApplyConfiguration(new VksUserConfiguration());
            modelBuilder.ApplyConfiguration(new VksUserProfilesConfiguration());
            modelBuilder.ApplyConfiguration(new VksUserProfilesViewConfiguration());
            modelBuilder.ApplyConfiguration(new VksUsersConferenceConfiguration());
            modelBuilder.ApplyConfiguration(new VksUsersOtherConfiguration());
            modelBuilder.ApplyConfiguration(new VksVendorConfiguration());
            modelBuilder.ApplyConfiguration(new VksVendorModelConfiguration());

            base.OnModelCreating(modelBuilder);

            if (Database.IsNpgsql())
            {
                FixSnakeCaseNames(modelBuilder);
            }
        }

        /// <summary>
        /// Вызвать в DLINQ для LIKE оператора
        /// </summary>
        /// <param name="matchExpression">Значние для поиска</param>
        /// <param name="pattern">wildcard</param>
        /// <returns></returns>
        public static bool Like(string matchExpression, string pattern) => Microsoft.EntityFrameworkCore.EF.Functions.Like(matchExpression, pattern);

        private void FixSnakeCaseNames(ModelBuilder modelBuilder)
        {
            var mapper = new NpgsqlSnakeCaseNameTranslator();
            foreach (var table in modelBuilder.Model.GetEntityTypes())
            {
                    ConvertToSnake(mapper, table);
                    foreach (var property in table.GetProperties())
                    {
                        ConvertToSnake(mapper, property);
                    }

                    foreach (var primaryKey in table.GetKeys())
                    {
                        ConvertToSnake(mapper, primaryKey);
                    }

                    foreach (var foreignKey in table.GetForeignKeys())
                    {
                        ConvertToSnake(mapper, foreignKey);
                    }

                    foreach (var indexKey in table.GetIndexes())
                    {
                        ConvertToSnake(mapper, indexKey);
                    }                
            }
        }

        private void ConvertToSnake(INpgsqlNameTranslator mapper, object entity)
        {
            switch (entity)
            {
                case IMutableEntityType table:
                    //Config -> ToTable
                    if (!String.IsNullOrEmpty(table.GetTableName()))
                    {
                        table.SetTableName(ConvertGeneralToSnake(mapper, table.GetTableName()).Replace("_", string.Empty));
                        table.SetSchema("dbo");
                    }
                    //Config -> ToView
                    if (!String.IsNullOrEmpty(table.GetViewName()))
                    {
                        table.SetViewName(ConvertGeneralToSnake(mapper,table.GetViewName()).Replace("_", string.Empty));
                        table.SetSchema("dbo");
                        table.SetViewSchema("dbo");
                    }
                    break;
                case IMutableProperty property:
                    property.SetColumnName(ConvertGeneralToSnake(mapper, property.GetColumnName()).Replace("_", string.Empty));
                    break;
                case IMutableKey primaryKey:
                    //Если нет PrimaryKey GetName вызывает исключение. Для View
                    try
                    {
                        primaryKey.SetName(ConvertKeyToSnake(mapper, primaryKey.GetName()));
                    }
                    catch { }
                    break;
                case IMutableForeignKey foreignKey:
                    foreignKey.SetConstraintName(ConvertKeyToSnake(mapper, foreignKey.GetConstraintName()));
                    break;
                case IMutableIndex indexKey:
                    indexKey.SetDatabaseName(ConvertKeyToSnake(mapper, indexKey.GetDatabaseName()));
                    break;
                default:
                    throw new NotSupportedException("Unexpected type was provided to snake case converter");
            }
        }

        private static readonly Regex KeysRegex = new Regex("^(PK|FK|IX)_", RegexOptions.Compiled);

        private string ConvertKeyToSnake(INpgsqlNameTranslator mapper, string keyName)
        {
            return ConvertGeneralToSnake(mapper, KeysRegex.Replace(keyName, match => match.Value.ToLower()));
        }

        private string ConvertGeneralToSnake(INpgsqlNameTranslator mapper, string entityName)
        {
            return mapper.TranslateMemberName(ModifyNameBeforeConvertion(mapper, entityName));
        }

        protected virtual string ModifyNameBeforeConvertion(INpgsqlNameTranslator mapper, string entityName)
        {
            return entityName;
        }
    }
}
