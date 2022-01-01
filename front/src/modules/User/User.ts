import Cookies from 'js-cookie'
import store from '@/store'
import { userConstants } from './User.const'
import { Auth } from '@/modules/Auth/Auth'
import { Permissions } from '@/modules/Permission/Permissions'
import { TokenChecker } from '../TokenChecker/TokenChecker'

export class User {
  public static async login(data: {
    accessToken?: string
    refreshToken?: string
    userName?: string
    userFullName?: string
    roles?: Array<string>
    timeOutMinute: number
  }) {
    TokenChecker.init(data.timeOutMinute)
    await Auth.setAccessToken(data.accessToken || '')
    await Auth.setRefreshToken(data.refreshToken || '')
    await User.setUserCookieField(
      data,
      userConstants.COOKIE_NAME_USER_NAME,
      'user/setUserName',
    )
    await User.setUserCookieField(
      data,
      userConstants.COOKIE_NAME_USER_FULLNAME,
      'user/setUserFullName',
    )
    await Permissions.setPermissionList(data.roles || [])
  }

  public static getUserName(): string {
    const cookieUserName = Cookies.get(userConstants.COOKIE_NAME_USER_NAME)
    if (cookieUserName) {
      return cookieUserName
    } else {
      const storeUserName = store?.getters['user/userName']
      Cookies.set(userConstants.COOKIE_NAME_USER_NAME, storeUserName || '')
      return storeUserName || ''
    }
  }

  public static getUserFullName(): string {
    const cookieUserFullName = Cookies.get(
      userConstants.COOKIE_NAME_USER_FULLNAME,
    )
    if (cookieUserFullName) {
      return cookieUserFullName
    } else {
      const storeUserFullName = store?.getters['user/userFullName']
      Cookies.set(
        userConstants.COOKIE_NAME_USER_FULLNAME,
        storeUserFullName || '',
      )
      return storeUserFullName || ''
    }
  }

  private static async removeUserCookieField(
    field: string,
    storeActionName: string,
  ) {
    await Cookies.remove(field)
    await store.dispatch(storeActionName, '')
  }

  public static async setUserCookieField(
    obj: Record<string, any>,
    field: string,
    storeActionName: string,
  ) {
    const value: string | undefined = obj[field]
    if (value === undefined) {
      return false
    }
    Cookies.set(field, value)

    await store.dispatch(storeActionName, value)
    return true
  }

  public static async logout() {
    TokenChecker.dispose()

    await User.removeUserCookieField(
      userConstants.COOKIE_NAME_USER_NAME,
      'user/setUserName',
    )
    await User.removeUserCookieField(
      userConstants.COOKIE_NAME_USER_FULLNAME,
      'user/setUserFullName',
    )
    await Auth.removeAccessToken()
    await Auth.removeRefreshToken()
    await Permissions.removePermissionList()
    await Auth.clearServerCookie()
  }
}
