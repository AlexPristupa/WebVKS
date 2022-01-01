import { RouteName } from '@/router/RouteName.enum'
import { Permissions } from '@/modules/Permission/Permissions'
import { IRoutePermission } from '@/router/IRoutePermission.interface'
import { IRouteRedirect } from '@/router/IRouteRedirect.interface'
import { PermissionValidValue } from '@/modules/Permission/PermissionValidValue.enum'

export const getRedirectRouteByPermission = (
  config: Array<IRoutePermission>,
): IRouteRedirect => {
  const validPermits = Permissions.getPermission()
  const result = config.find(item => {
    return Permissions.checkPermission([item.permission])
  })
  const onlyWrapper = validPermits.join() === [PermissionValidValue.MMS].join()
  const defaultRoute = RouteName.noAccess
  return {
    name: result?.route && !onlyWrapper ? result.route : defaultRoute,
  }
}
