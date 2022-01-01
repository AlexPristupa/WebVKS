import { ITab } from '@/types/tabs/interface.tabs'
import { PermissionValidValue } from '@/modules/Permission/PermissionValidValue.enum'
import { RouteName } from '@/router/RouteName.enum'

export const NO_ACCESS_TAB: Array<ITab> = [
  {
    id: RouteName.noAccess,
    label: 'routing.noAccess.accessError',
    name: RouteName.noAccess,
    routeName: RouteName.noAccess,
    role: '',
  },
]

export const BOOKING_TABS: Array<ITab> = [
  {
    id: RouteName.bookingList,
    label: 'bookings.tabLabels.bookings',
    name: RouteName.bookingList,
    routeName: RouteName.bookingList,
    role: PermissionValidValue.MMS_BOOKING_BOOKING,
  },
  {
    id: RouteName.bookingListRestricted,
    label: 'bookings.tabLabels.bookings',
    name: RouteName.bookingListRestricted,
    routeName: RouteName.bookingListRestricted,
    role: PermissionValidValue.MMS_BOOKING_BOOKING_RESTRICTED,
  },
  {
    id: RouteName.spacesList,
    label: 'bookings.tabLabels.spaces',
    name: RouteName.spacesList,
    routeName: RouteName.spacesList,
    role: PermissionValidValue.MMS_BOOKING_SPACES,
  },
  {
    id: RouteName.recordingsList,
    label: 'bookings.tabLabels.records',
    name: RouteName.recordingsList,
    routeName: RouteName.recordingsList,
    role: PermissionValidValue.MMS_BOOKING_RECORDS,
  },
]

export const CONFERENCES_TABS: Array<ITab> = [
  {
    id: RouteName.activeConferences,
    label: 'conferences.tabLabels.activeConferences',
    name: RouteName.activeConferences,
    routeName: RouteName.activeConferences,
    role: PermissionValidValue.MMS_CONFERENCES_ACTIVE,
  },
  {
    id: RouteName.historyConferences,
    label: 'conferences.tabLabels.historyConferences',
    name: RouteName.historyConferences,
    routeName: RouteName.historyConferences,
    role: PermissionValidValue.MMS_CONFERENCES_HISTORY,
  },
]

export const SETTINGS_TABS: Array<ITab> = [
  {
    id: RouteName.userProfile,
    label: 'settings.tabLabels.userProfile',
    name: RouteName.userProfile,
    routeName: RouteName.userProfile,
    role: PermissionValidValue.MMS_SETTINGS_USER_PROFILES,
  },
  {
    id: RouteName.spaceGroups,
    label: 'settings.tabLabels.spaceGroups',
    name: RouteName.spaceGroups,
    routeName: RouteName.spaceGroups,
    role: PermissionValidValue.MMS_SETTINGS_ROOMS_GROUPS,
  },
  {
    id: RouteName.recordStorage,
    label: 'settings.tabLabels.recordStorage',
    name: RouteName.recordStorage,
    routeName: RouteName.recordStorage,
    role: PermissionValidValue.MMS_SETTINGS_RECORD_STORES,
  },
  {
    id: RouteName.exchange,
    label: 'settings.tabLabels.exchange',
    name: RouteName.exchange,
    routeName: RouteName.exchange,
    role: PermissionValidValue.MMS_SETTINGS_EXCHANGE,
  },
]

export const CMS_TABS: Array<ITab> = [
  {
    id: RouteName.cmsServers,
    label: 'cms.tabLabels.cmsServers',
    name: RouteName.cmsServers,
    routeName: RouteName.cmsServers,
    role: PermissionValidValue.MMS_CMS_SERVERS,
  },
  {
    id: RouteName.cmsServerGroups,
    label: 'cms.tabLabels.serversGroups',
    name: RouteName.cmsServerGroups,
    routeName: RouteName.cmsServerGroups,
    role: PermissionValidValue.MMS_CMS_SERVERS_GROUPS,
  },
]

export const REPORTS_TABS: Array<ITab> = [
  {
    id: RouteName.reportList,
    label: 'reports.tabLabels.reports',
    name: RouteName.reportList,
    routeName: RouteName.reportList,
    role: PermissionValidValue.MMS_REPORTS_REPORTS,
  },
  {
    id: RouteName.distributionReports,
    label: 'reports.tabLabels.distributionReports',
    name: RouteName.distributionReports,
    routeName: RouteName.distributionReports,
    role: PermissionValidValue.MMS_REPORTS_REPORTS_DISTRIBUTION,
  },
]

export const ERROR_TABS: Array<ITab> = [
  {
    id: RouteName.notFound,
    label: 'routing.notFound',
    name: RouteName.notFound,
    routeName: RouteName.notFound,
    role: '',
  },
]
