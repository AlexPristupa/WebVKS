
using MentolVKS.Model.Enums;

namespace MentolVKS.Data.EF.Settings
{
    /// <summary>
    /// Настройки БД
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Таймаут запроса
        /// </summary>
        public int CommandTimeout { get; set; }

        /// <summary>
        /// Включать сообщения об исключениях
        /// </summary>
        public bool EnableSensitiveDataLogging { get; set; }

        /// <summary>
        /// Тип БД MsSql/Postgres
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// RLS
        /// </summary>
        public bool EnableRls { get; set; } = false;
    }
}