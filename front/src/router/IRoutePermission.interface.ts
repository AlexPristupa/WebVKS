import { RouteName } from '@/router/RouteName.enum'
import { PermissionValidValue } from '@/modules/Permission/PermissionValidValue.enum'

export interface IRoutePermission {
  route: RouteName
  permission: PermissionValidValue
}
