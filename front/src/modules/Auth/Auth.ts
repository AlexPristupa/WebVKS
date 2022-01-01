import Cookies from 'js-cookie'
import store from '@/store'
import { authConstants } from './Auth.const'
import { SERVER_COOKIE_NAME } from '@/modules/Auth/ServerCookies.const'

export class Auth {
  public static getAccessToken(): string {
    const cookieAccessToken = Cookies.get(
      authConstants.COOKIE_NAME_ACCESS_TOKEN,
    )
    if (cookieAccessToken) {
      return cookieAccessToken
    } else {
      if (store) {
        const storeAccessToken = store.getters['user/accessToken']
        Cookies.set(
          authConstants.COOKIE_NAME_ACCESS_TOKEN,
          storeAccessToken || '',
        )
        return storeAccessToken || ''
      }
    }
    return ''
  }

  public static getRefreshToken(): string {
    const cookieAccessToken = Cookies.get(
      authConstants.COOKIE_NAME_REFRESH_TOKEN,
    )
    if (cookieAccessToken) {
      return cookieAccessToken
    } else {
      if (store) {
        const storeRefreshToken = store.getters['user/refreshToken']
        Cookies.set(
          authConstants.COOKIE_NAME_REFRESH_TOKEN,
          storeRefreshToken || '',
        )
        return storeRefreshToken || ''
      }
    }
    return ''
  }

  public static async setRefreshToken(refreshToken) {
    if (refreshToken) {
      Cookies.set(authConstants.COOKIE_NAME_REFRESH_TOKEN, refreshToken)
      await store.dispatch('user/setRefreshToken', refreshToken)
      return true
    } else {
      return false
    }
  }

  public static async removeRefreshToken() {
    await Cookies.remove(authConstants.COOKIE_NAME_REFRESH_TOKEN)
    await store.dispatch('user/setRefreshToken', '')
  }

  public static async setAccessToken(accessToken) {
    if (accessToken) {
      Cookies.set(authConstants.COOKIE_NAME_ACCESS_TOKEN, accessToken)
      await store.dispatch('user/setAccessToken', accessToken)
      return true
    } else {
      return false
    }
  }

  public static async removeAccessToken() {
    await Cookies.remove(authConstants.COOKIE_NAME_ACCESS_TOKEN)
    await store.dispatch('user/setAccessToken', '')
  }

  public static async clearServerCookie() {
    SERVER_COOKIE_NAME.forEach(cookieName => {
      Cookies.remove(cookieName)
    })
  }
}
