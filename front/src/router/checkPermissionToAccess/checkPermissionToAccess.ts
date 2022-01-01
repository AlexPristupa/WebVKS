import { Permissions } from '@/modules/Permission/Permissions'
import { RouteName } from '@/router/RouteName.enum'

export function checkPermissionToAccess(to, from) {
  const validPermits = Permissions.getPermission()
  return to.meta.permission &&
    !validPermits.find(permission => permission === to.meta.permission)
    ? from.name !== RouteName.noAccess
    : false
}
