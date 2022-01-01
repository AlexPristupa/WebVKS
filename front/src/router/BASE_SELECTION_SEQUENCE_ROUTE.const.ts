import { RouteName } from '@/router/RouteName.enum'
import { PermissionValidValue } from '@/modules/Permission/PermissionValidValue.enum'
import { IRoutePermission } from '@/router/IRoutePermission.interface'

export const BASE_SELECTION_SEQUENCE_ROUTE: Array<IRoutePermission> = [
  {
    route: RouteName.bookings,
    permission: PermissionValidValue.MMS_BOOKING,
  },
  {
    route: RouteName.conferences,
    permission: PermissionValidValue.MMS_CONFERENCES,
  },
  {
    route: RouteName.settings,
    permission: PermissionValidValue.MMS_SETTINGS,
  },
  {
    route: RouteName.cms,
    permission: PermissionValidValue.MMS_CMS,
  },
  {
    route: RouteName.reports,
    permission: PermissionValidValue.MMS_REPORTS,
  },
]
