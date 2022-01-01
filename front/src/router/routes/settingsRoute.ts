import i18n from '@/i18n'
import { RouteName } from '@/router/RouteName.enum'
import { RoutePage } from '@/router/RoutePage.enum'
import { RouteSection } from '@/router/RouteSection.enum'
import { PermissionValidValue } from '@/modules/Permission/PermissionValidValue.enum'
import { getRedirectRouteByPermission } from '@/router/getRedirectRouteByPermission/getRedirectRouteByPermission'
import { IRouteRedirect } from '@/router/IRouteRedirect.interface'
import { IRoutePermission } from '@/router/IRoutePermission.interface'

const SELECTION_SEQUENCE_ROUTE: Array<IRoutePermission> = [
  {
    route: RouteName.userProfile,
    permission: PermissionValidValue.MMS_SETTINGS_USER_PROFILES,
  },
  {
    route: RouteName.spaceGroups,
    permission: PermissionValidValue.MMS_SETTINGS_ROOMS_GROUPS,
  },
  {
    route: RouteName.recordStorage,
    permission: PermissionValidValue.MMS_SETTINGS_RECORD_STORES,
  },
  {
    route: RouteName.exchange,
    permission: PermissionValidValue.MMS_SETTINGS_EXCHANGE,
  },
]

export const settingsRoute = [
  {
    path: '/settings',
    name: RouteName.settings,
    redirect: (): IRouteRedirect => {
      return getRedirectRouteByPermission(SELECTION_SEQUENCE_ROUTE)
    },
    meta: {
      permission: PermissionValidValue.MMS_SETTINGS,
      section: RouteSection.app,
      page: RoutePage.settings,
      title: i18n.t('routing.settings.userProfile'),
    },
  },
  {
    path: '/settings/user-list',
    name: RouteName.userProfile,
    component: () => {
      return import('@/pages/settings/userProfile/UserProfile.vue')
    },
    meta: {
      permission: PermissionValidValue.MMS_SETTINGS_USER_PROFILES,
      section: RouteSection.app,
      page: RoutePage.settings,
      title: i18n.t('routing.settings.userProfile'),
    },
  },
  {
    path: '/settings/space-group',
    name: RouteName.spaceGroups,
    component: () => {
      return import('@/pages/settings/spaceGroups/SpaceGroups.vue')
    },
    meta: {
      permission: PermissionValidValue.MMS_SETTINGS_ROOMS_GROUPS,
      section: RouteSection.app,
      page: RoutePage.settings,
      title: i18n.t('routing.settings.spaceGroups'),
    },
  },
  {
    path: '/settings/record-storage',
    name: RouteName.recordStorage,
    component: () => {
      return import('@/pages/settings/recordStorage/RecordStorage.vue')
    },
    meta: {
      permission: PermissionValidValue.MMS_SETTINGS_RECORD_STORES,
      section: RouteSection.app,
      page: RoutePage.settings,
      title: i18n.t('routing.settings.recordStorage'),
    },
  },
  {
    path: '/settings/exchange',
    name: RouteName.exchange,
    component: () => {
      return import('@/pages/settings/exchange/Exchange.vue')
    },
    meta: {
      permission: PermissionValidValue.MMS_SETTINGS_EXCHANGE,
      section: RouteSection.app,
      page: RoutePage.settings,
      title: i18n.t('routing.settings.exchange'),
    },
  },
]
