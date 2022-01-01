import i18n from '@/i18n'
import { RouteName } from '@/router/RouteName.enum'
import { RoutePage } from '@/router/RoutePage.enum'
import { RouteSection } from '@/router/RouteSection.enum'
import { PermissionValidValue } from '@/modules/Permission/PermissionValidValue.enum'
import { IRouteRedirect } from '@/router/IRouteRedirect.interface'
import { getRedirectRouteByPermission } from '@/router/getRedirectRouteByPermission/getRedirectRouteByPermission'
import { IRoutePermission } from '@/router/IRoutePermission.interface'

const SELECTION_SEQUENCE_ROUTE: Array<IRoutePermission> = [
  {
    route: RouteName.cmsServers,
    permission: PermissionValidValue.MMS_CMS_SERVERS,
  },
  {
    route: RouteName.cmsServerGroups,
    permission: PermissionValidValue.MMS_CMS_SERVERS_GROUPS,
  },
]

export const cmsRoute = [
  {
    path: '/cms',
    name: RouteName.cms,
    redirect: (): IRouteRedirect => {
      return getRedirectRouteByPermission(SELECTION_SEQUENCE_ROUTE)
    },
    meta: {
      permission: PermissionValidValue.MMS_CMS,
      section: RouteSection.app,
      page: RoutePage.cms,
      title: i18n.t('routing.cms.cmsServers'),
    },
  },
  {
    path: '/cms/servers',
    name: RouteName.cmsServers,
    component: () => {
      return import('@/pages/cms/cmsServers/CmsServers.vue')
    },
    meta: {
      permission: PermissionValidValue.MMS_CMS_SERVERS,
      section: RouteSection.app,
      page: RoutePage.cms,
      title: i18n.t('routing.cms.cmsServers'),
    },
    children: [],
  },
  {
    path: '/cms/server-group',
    name: RouteName.cmsServerGroups,
    component: () => {
      return import('@/pages/cms/serversGroups/ServersGroups.vue')
    },
    meta: {
      permission: PermissionValidValue.MMS_CMS_SERVERS_GROUPS,
      section: RouteSection.app,
      page: RoutePage.cms,
      title: i18n.t('routing.cms.serversGroups'),
    },
  },
]
