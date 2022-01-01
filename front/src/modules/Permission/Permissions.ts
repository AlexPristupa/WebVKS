import Cookies from 'js-cookie'
import { COOKIE_NAME_PERMISSION } from '@/modules/Permission/Permission.const'
import store from '@/store'
import { allPermissionList } from '@/modules/Permission/allPermissionList.const'
import { PermissionsType } from '@/modules/Permission/permissionsType.enum'

export class Permissions {
  public static getPermission(): Array<string> {
    const cookiePermission = Cookies.get(COOKIE_NAME_PERMISSION)
    if (cookiePermission) {
      return JSON.parse(cookiePermission)
    }
    const storePermission = store.getters['user/roles']
    if (storePermission) {
      return storePermission
    }
    return []
  }

  public static async setPermissionList(permissionList: Array<string>) {
    /**
     * Временно убрано разделение ролей между dev и prod,
     * пока бэк не начнет для user`а роли отдавать.
     */
    const validPermissionList = permissionList.filter(item => {
      return Permissions.validation(item)
    })
    if (process.env.NODE_ENV === 'development') {
      const allPermissionDevelop = allPermissionList.map(item => item.value)
      Cookies.set(COOKIE_NAME_PERMISSION, JSON.stringify(allPermissionDevelop))
      await store.dispatch('user/changeRoles', allPermissionDevelop)
    } else {
      Cookies.set(COOKIE_NAME_PERMISSION, JSON.stringify(validPermissionList))
      await store.dispatch('user/changeRoles', validPermissionList)
    }
  }

  public static async removePermissionList() {
    Cookies.remove(COOKIE_NAME_PERMISSION)
    await store.dispatch('user/changeRoles', [])
  }

  private static validation(permission: string) {
    return !!allPermissionList.find(item => item.value === permission)
  }

  /**
   * @description Возвращает true если пользователь имеет хотя бы одно разрешение
   *              из требуемых.
   */
  public static checkPermission(
    permissions: Array<string>,
    permissionsType: PermissionsType = PermissionsType.single,
  ): boolean {
    const validPermits = Permissions.getPermission()
    const result = permissions.reduce((acc, item) => {
      if (validPermits.includes(item)) {
        acc = --acc
      }
      return acc
    }, permissions.length)
    return permissionsType === PermissionsType.single
      ? !result
      : result <= permissions.length
  }
}
