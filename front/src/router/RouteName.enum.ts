export enum RouteName {
  notFound = 'notFound',
  noAccess = 'noAccess',
  main = 'main',
  login = 'login',

  /** Бронирования */
  bookings = 'bookings',
  bookingList = 'bookingList',
  bookingListRestricted = 'bookingListRestricted',
  spacesList = 'spacesList',
  recordingsList = 'recordingsList',

  /** Сервера CMS */
  cms = 'cms',
  cmsServers = 'cmsServers',
  cmsServerGroups = 'cmsServerGroups',

  /** Конференции */
  conferences = 'Conferences',
  activeConferences = 'activeConferences',
  historyConferences = 'historyConferences',

  /** Настройки */
  settings = 'settings',
  userProfile = 'userProfile',
  spaceGroups = 'spaceGroups',
  recordStorage = 'recordStorage',
  exchange = 'exchange',

  /** Отчеты */
  reports = 'reports',
  reportList = 'reportList',
  distributionReports = 'distributionReports',
}
