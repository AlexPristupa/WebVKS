/**
 * @description Основные константы проекта
 */
import {
  BOOKING_TABS,
  CMS_TABS,
  CONFERENCES_TABS,
  ERROR_TABS,
  REPORTS_TABS,
  SETTINGS_TABS,
  NO_ACCESS_TAB,
} from '@/components/TabsView/CONST_MAIN_LAYOUT'
import { RoutePage } from '@/router/RoutePage.enum'
import { RouteName } from '@/router/RouteName.enum'

const TIMEOUT = 25000
const TIMEOUT_SERVICE = 5000
const TIMEOUT_SERVICE_SHORT = 3000
const EXPORT_TIMEOUT = 3600000
const PAGE_NUM = 15
const UPLOAD_PREFIX = 'upload/'
const SUCCESS_STATUS = 20000
// Tabs
const activeTab = {
  [RoutePage.bookings]: RouteName.bookings,
  [RoutePage.conferences]: RouteName.conferences,
  [RoutePage.settings]: RouteName.settings,
  [RoutePage.cms]: RouteName.cms,
  [RoutePage.reports]: RouteName.reports,
  [RoutePage.notFound]: RouteName.notFound,
}
const tabsSetting = {
  [RoutePage.noAccess]: NO_ACCESS_TAB,
  [RoutePage.bookings]: BOOKING_TABS,
  [RoutePage.conferences]: CONFERENCES_TABS,
  [RoutePage.settings]: SETTINGS_TABS,
  [RoutePage.cms]: CMS_TABS,
  [RoutePage.reports]: REPORTS_TABS,
  [RoutePage.notFound]: ERROR_TABS,
}

const dateMasks = {
  withoutTime: {
    ru: 'DD.MM.YYYY',
    en: 'YYYY-MM-DD',
  },
  withTime: {
    withoutSeconds: {
      ru: 'DD.MM.YYYY HH:mm',
      en: 'YYYY-MM-DDTHH:mm',
    },
    withSeconds: {
      ru: 'DD.MM.YYYY HH:mm:ss',
      en: 'YYYY-MM-DDTHH:mm:ss',
    },
  },
  forParseTimeFunc: {
    withoutTime: {
      ru: '{d}.{m}.{y}',
      en: '{y}-{m}-{d}',
    },
    withTime: {
      withoutSeconds: {
        ru: '{d}.{m}.{y} {h}:{i}',
        en: '{y}-{m}-{d} {h}:{i}',
      },
      withSeconds: {
        ru: '{d}.{m}.{y} {h}:{i}:{s}',
        en: '{y}-{m}-{d} {h}:{i}:{s}',
      },
    },
  },
}

export {
  TIMEOUT,
  EXPORT_TIMEOUT,
  TIMEOUT_SERVICE,
  TIMEOUT_SERVICE_SHORT,
  PAGE_NUM,
  UPLOAD_PREFIX,
  SUCCESS_STATUS,
  activeTab,
  tabsSetting,
  dateMasks,
}
