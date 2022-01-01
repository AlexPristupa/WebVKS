export enum URLs {
  login = '/api/Login',
  refreshToken = '/api/Login/Refresh',
  // Настройки таблицы
  // Получить имена колонок
  fetchNameFieldAgGridVue = '/api/AgGridVue/getNameFieldsAgGridVue',
  fetchFoundColumnValue = '/api/AgGridVue/GetNotFoundColumnValue',
  // Установить видимость колонок
  postSettingVisibleColumnsAgGridVue = '/api/AgGridVue/postSettingVisibleColumnsAgGridVue',
  // Установить ширину колонок
  postSettingWidthColumnsAgGridVue = '/api/AgGridVue/postSettingWidthColumnsAgGridVue',
  // Получение данных из общего end-point ColumnFilter
  fetchData = '/api/ColumnFilter',
  // Получение дефолтных значений для модалок
  invGetDefaultValue = '/api/Inventory/DefaultValue',
  // дерево
  invTreeStructuresTree = '/api/inventory/Structure/Tree',

  /**
   *  для ячейки с встроенным редактированием
   */
  invExtensionPropertyCatalog = '/api/inventory/ExtensionProperty/Catalog',

  //----------------- Панель фильтрации + Экспорт ------------------
  filtrationPanelList = '/api/inventory/ExtensionProperty/FieldList',

  //---------------------------- Экспорт ---------------------------
  export = '/api/export/xlsx',
  exportDownload = '/api/export/xlsx/Download',

  //-------------- Справочники для селектов и списков --------------
  dictionarySpaces = '/api/Directory/Space',
  dictionaryServersGroups = '/api/Directory/ServerGroups',
  dictionaryPinPolitics = '/api/Directory/PinPolitics',
  dictionaryConnectionType = '/api/Directory/ConnectionType',
  dictionaryTimeZone = '/api/Directory/TimeZone',
  dictionaryVksServer = '/api/Directory/VksServer',
  dictionaryVksUser = '/api/Directory/VksUser',
  dictionaryVksUserOthers = '/api/Directory/VksUserOther',
  dictionaryAspNetUsers = '/api/Directory/AspNetUsers',
  dictionaryBookingType = '/api/Directory/BookingType',

  //----------------------------- Сервер ---------------------------
  vksCmsServer = '/api/servers',
  vksCmsServerGroup = '/api/serversGroup',

  //----------------------------- Booking ----------------------------
  booking = '/api/Booking',
  bookingScheduleText = '/api/Schedule/ScheduleText',
  bookingGetUserByLogin = '/api/VksUser/GetByJid',
  bookingFindIntersection = '/api/Schedule/ScheduleCheck',

  //----------------------------- Spaces ----------------------------
  spaces = '/api/Space',

  //-------------------------- Recordings ---------------------------
  recordingsUsers = '/api/recordingVksUsers',
  recordingFile = '/api/File/recording',

  //------------------------------ User ----------------------------
  vksUser = '/api/VksUser',
  vksUserOther = '/api/VksUserOther',
  //----------------------------- Proxy ----------------------------
  proxyBooking = '/proxy/booking/info',
  proxyBookingSpaceAdd = '/proxy/booking/addspaces',
  proxyBookingSpaceEdit = '/proxy/booking/editspaces',
  proxyBookingSpaceDelete = '/proxy/booking/deletespaces',
  proxyBookingRefresh = '/proxy/booking/refreshlog',
  proxyBookingStart = '/proxy/booking/start',
  proxyBookingStop = '/proxy/booking/stop',
  proxySpacesCallLegProfiles = '/proxy/booking/CallLegProfiles/info',
  proxySpacesCallBrandingProfiles = '/proxy/Booking/callBrandingProfiles/info',

  recordings = '/api/recording',

  //----------------------------- Отчеты ----------------------------
  fetchDataReport = '/api/ReportsInv/reportslist',
  fetchDataReportDetail = '/api/ReportsInv/reportDetail',
  jasperCreateXlsx = '/api/ReportsInv/jasperCreate/xlsx',
  jasperCheck = '/api/ReportsInv/jasperasyncReport/check',
  jasperasyncReport = '/api/ReportsInv/jasperasyncReport/report',
  jasperasyncReportXlsx = '/api/ReportsInv/jasperasyncReport/xlsx/export',
  downloadfileReport = '/api/ReportsInv/downloadFileReport',
  fetchJasperPdf = '/api/ReportsInv/jasper/pdf',
  cancelReport = '/api/ReportsInv/cancelReport',

  //----------------------------Непроверенно----------------------
  fetchSelectFilter = '/api/SelectFilter',
  fetchSelectListFilterName = '/api/SelectFilter/getSelectListFilterName',
  saveFilter = '/api/SelectFilter/saveFilter',
  getColumnNameByFilter = '/api/SelectFilter/getColumNameByFilter',
  getTypeFilter = '/api/SelectFilter/getTypeFilter',
  getActiveStringFilter = '/api/SelectFilter/getActiveStringFilter',
  getActiveSelectFilter = '/api/SelectFilter/getActiveSelectFilter',
  getActiveIntegerFilter = '/api/SelectFilter/getActiveIntegerFilter',
  getActiveDateFilter = '/api/SelectFilter/getActiveDateFilter',
  getActiveTimeFilter = '/api/SelectFilter/getActiveTimeFilter',
  deleteFilter = '/api/SelectFilter/deleteFilter',
  renameFilter = '/api/SelectFilter/renameFilter',
}
