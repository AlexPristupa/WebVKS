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
    route: RouteName.reportList,
    permission: PermissionValidValue.MMS_REPORTS_REPORTS,
  },
  {
    route: RouteName.distributionReports,
    permission: PermissionValidValue.MMS_REPORTS_REPORTS_DISTRIBUTION,
  },
]

export const reportsRoute = [
  {
    path: '/reports',
    name: RouteName.reports,
    redirect: (): IRouteRedirect => {
      return getRedirectRouteByPermission(SELECTION_SEQUENCE_ROUTE)
    },
    meta: {
      permission: PermissionValidValue.MMS_REPORTS,
      section: RouteSection.app,
      page: RoutePage.reports,
      title: i18n.t('routing.reports.reports'),
    },
  },
  {
    path: '/reports/reports',
    name: RouteName.reportList,
    component: () => {
      return import('@/pages/reports/reports/Reports.vue')
    },
    meta: {
      permission: PermissionValidValue.MMS_REPORTS_REPORTS,
      section: RouteSection.app,
      page: RoutePage.reports,
      title: i18n.t('routing.reports.reports'),
    },
  },
  {
    path: '/reports/distribution-reports',
    name: RouteName.distributionReports,
    component: () => {
      return import(
        '@/pages/reports/distributionReports/DistributionReports.vue'
      )
    },
    meta: {
      permission: PermissionValidValue.MMS_REPORTS_REPORTS_DISTRIBUTION,
      section: RouteSection.app,
      page: RoutePage.reports,
      title: i18n.t('routing.reports.distributionReports'),
    },
  },
]
