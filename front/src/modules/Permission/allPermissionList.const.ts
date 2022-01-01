import { PermissionValidValue } from '@/modules/Permission/PermissionValidValue.enum'

export const allPermissionList = [
  {
    id: 1,
    value: PermissionValidValue.MMS,
    description: 'Управление конференциями',
  },
  {
    id: 10,
    value: PermissionValidValue.MMS_BOOKING,
    description: 'Бронирование',
  },
  {
    id: 100,
    value: PermissionValidValue.MMS_BOOKING_BOOKING,
    description: 'Бронирования',
  },
  {
    id: 100,
    value: PermissionValidValue.MMS_BOOKING_BOOKING_RESTRICTED,
    description: 'Бронирования',
  },
  {
    id: 101,
    value: PermissionValidValue.MMS_BOOKING_SPACES,
    description: 'Комнаты',
  },
  {
    id: 102,
    value: PermissionValidValue.MMS_BOOKING_RECORDS,
    description: 'Записи',
  },
  {
    id: 12,
    value: PermissionValidValue.MMS_CMS,
    description: 'CMS',
  },
  {
    id: 300,
    value: PermissionValidValue.MMS_CMS_SERVERS,
    description: 'Сервера',
  },
  {
    id: 301,
    value: PermissionValidValue.MMS_CMS_SERVERS_GROUPS,
    description: 'Группы серверов',
  },
  {
    id: 13,
    value: PermissionValidValue.MMS_REPORTS,
    description: 'Отчеты',
  },
  {
    id: 400,
    value: PermissionValidValue.MMS_REPORTS_REPORTS,
    description: 'Отчеты',
  },
  {
    id: 401,
    value: PermissionValidValue.MMS_REPORTS_REPORTS_DISTRIBUTION,
    description: 'Рассылка отчетов',
  },
  {
    id: 11,
    value: PermissionValidValue.MMS_SETTINGS,
    description: 'Настройки',
  },
  {
    id: 14,
    value: PermissionValidValue.MMS_USERS,
    description: 'Пользователи',
  },
  {
    id: 500,
    value: PermissionValidValue.MMS_USERS_USERS,
    description: 'Пользователи -> пользователи',
  },
  {
    id: 501,
    value: PermissionValidValue.MMS_USERS_ROLES,
    description: 'Пользователи -> роли',
  },
  {
    id: 502,
    value: PermissionValidValue.MMS_USERS_LOGS,
    description: 'Пользователи -> журнал',
  },
  {
    id: 200,
    value: PermissionValidValue.MMS_SETTINGS_USER_PROFILES,
    description: 'Профили пользователей',
  },
  {
    id: 201,
    value: PermissionValidValue.MMS_SETTINGS_ROOMS_GROUPS,
    description: 'Группы комнат',
  },
  {
    id: 202,
    value: PermissionValidValue.MMS_SETTINGS_RECORD_STORES,
    description: 'Хранилища записей',
  },
  {
    id: 203,
    value: PermissionValidValue.MMS_SETTINGS_EXCHANGE,
    description: 'Exchange',
  },
  {
    id: null,
    value: PermissionValidValue.MMS_CONFERENCES,
    description: 'Конференции',
  },
  {
    id: null,
    value: PermissionValidValue.MMS_CONFERENCES_ACTIVE,
    description: 'Активные конференции',
  },
  {
    id: null,
    value: PermissionValidValue.MMS_CONFERENCES_HISTORY,
    description: 'Завершенные конференции',
  },
]
