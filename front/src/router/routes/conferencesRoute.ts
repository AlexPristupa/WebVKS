import i18n from '@/i18n'
import { RouteName } from '@/router/RouteName.enum'
import { RouteSection } from '@/router/RouteSection.enum'
import { RoutePage } from '@/router/RoutePage.enum'
import { PermissionValidValue } from '@/modules/Permission/PermissionValidValue.enum'
import { getRedirectRouteByPermission } from '@/router/getRedirectRouteByPermission/getRedirectRouteByPermission'
import { IRouteRedirect } from '@/router/IRouteRedirect.interface'
import { IRoutePermission } from '@/router/IRoutePermission.interface'

const SELECTION_SEQUENCE_ROUTE: Array<IRoutePermission> = [
  {
    route: RouteName.activeConferences,
    permission: PermissionValidValue.MMS_CONFERENCES_ACTIVE,
  },
  {
    route: RouteName.historyConferences,
    permission: PermissionValidValue.MMS_CONFERENCES_HISTORY,
  },
]

export const conferencesRoute = [
  {
    path: '/conferences',
    name: RouteName.conferences,
    redirect: (): IRouteRedirect => {
      return getRedirectRouteByPermission(SELECTION_SEQUENCE_ROUTE)
    },
    meta: {
      permission: PermissionValidValue.MMS_CONFERENCES,
      section: RouteSection.app,
      page: RoutePage.conferences,
      title: i18n.t('routing.conferences.active'),
    },
  },
  {
    path: '/conferences/active',
    name: RouteName.activeConferences,
    component: () => {
      return import('@/pages/conferences/Active/ActiveConferences.vue')
    },
    meta: {
      permission: PermissionValidValue.MMS_CONFERENCES_ACTIVE,
      section: RouteSection.app,
      page: RoutePage.conferences,
      title: i18n.t('routing.conferences.active'),
    },
  },
  {
    path: '/conferences/history',
    name: RouteName.historyConferences,
    component: () => {
      return import('@/pages/conferences/History/HistoryConferences.vue')
    },
    meta: {
      permission: PermissionValidValue.MMS_CONFERENCES_HISTORY,
      section: RouteSection.app,
      page: RoutePage.conferences,
      title: i18n.t('routing.conferences.history'),
    },
  },
]
