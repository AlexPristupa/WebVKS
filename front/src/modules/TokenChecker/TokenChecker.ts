import Cookies from 'js-cookie'
import { tokenCheckerConstants } from './TokenChecker.const'
import api from '@/api_services'
import { methods } from '@/api_services/httpMethods.enum'
import { Auth } from '@/modules/Auth/Auth'
import { User } from '@/modules/User/User'

export class TokenChecker {
  protected static intervalChecker: number | undefined = undefined

  private static reinit() {
    this.intervalChecker = undefined
    TokenChecker.init(
      TokenChecker.getExpireTimeoutMin() ||
        tokenCheckerConstants.defaultExpireTimeMin,
    )
  }

  public static init(timeoutMin: number): boolean {
    if (this.intervalChecker) {
      return false
    }
    TokenChecker.dispose()
    TokenChecker.setExpireTimeoutMin(timeoutMin)
    const timeout = TokenChecker.calcTimeoutCheckInterval()

    this.intervalChecker = setTimeout(this.checkToken, timeout)
    return true
  }

  public static setUserActivityTimestamp(): void {
    Cookies.set(
      tokenCheckerConstants.cookieNameUserActivityTimestamp,
      Date.now().toString(),
    )
  }
  public static getUserActivityTimestamp(): number {
    const timestampStr = Cookies.get(
      tokenCheckerConstants.cookieNameUserActivityTimestamp,
    )
    if (!timestampStr) {
      return 0
    }
    const timestampEpochTime = Number.parseInt(timestampStr)
    if (isNaN(timestampEpochTime)) {
      return 0
    }
    return timestampEpochTime
  }

  protected static clearCookies() {
    Cookies.remove(tokenCheckerConstants.cookieNameUserExpireTokenIntervalMin)
  }

  public static setExpireTimeoutMin(timeoutMin: number) {
    Cookies.set(
      tokenCheckerConstants.cookieNameUserExpireTokenIntervalMin,
      (timeoutMin || tokenCheckerConstants.defaultExpireTimeMin).toString(),
    )
  }

  public static getExpireTimeoutMin(): number | null {
    const timeoutMinStr = Cookies.get(
      tokenCheckerConstants.cookieNameUserExpireTokenIntervalMin,
    )
    if (!timeoutMinStr) {
      return null
    }
    const timeoutMin = Number.parseInt(timeoutMinStr)
    if (isNaN(timeoutMin)) {
      return null
    }
    return timeoutMin
  }

  public static getExpireTimeoutMs(): number {
    const timeoutMin = this.getExpireTimeoutMin()
    if (!timeoutMin) {
      return 0
    }
    return timeoutMin * 60 * 1_000 // 60 секунд * 1е3 мс
  }

  protected static calcTimeoutCheckInterval() {
    const timeoutMs = TokenChecker.getExpireTimeoutMs() - 60_000 // время жизни - 1 минута
    if (timeoutMs <= 0) {
      // невозможная ситуация, но стоит обработать
      console.warn(
        'the token check timeoutMs < 0. the token will be checked in 10s',
      )
      return 10_000
    }
    return timeoutMs
  }

  public static async update(): Promise<boolean> {
    try {
      if (!Auth.getRefreshToken()) {
        return false
      }
      const data = await api.refreshToken({
        method: methods.get,
        data: { refresh: Auth.getRefreshToken() },
      })

      if (data && data.accessToken) {
        await Auth.setAccessToken(data.accessToken)
        TokenChecker.reinit()
        return true
      } else {
        throw 'missing access token'
      }
    } catch {
      await User.logout()
      return false
    }
  }

  protected static async checkToken() {
    // проверка по активности
    const lastActivity = Math.abs(
      Date.now() - TokenChecker.getUserActivityTimestamp(),
    )
    const expireMs = TokenChecker.getExpireTimeoutMs()
    const delta = lastActivity / expireMs
    if (
      delta > tokenCheckerConstants.tokenActivityThreshold ||
      !(await TokenChecker.update())
    ) {
      location.reload() // за неимением роутера в зоне видимости
      return
    }
  }

  public static dispose(): void {
    TokenChecker.clearCookies()
    clearInterval(this.intervalChecker)
    this.intervalChecker = undefined
  }
}
