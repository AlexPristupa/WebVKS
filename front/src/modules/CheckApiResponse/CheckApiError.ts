import { CheckApiError, CheckError } from './CheckApiError.interface'
import i18n from '@/i18n'
import { message } from './CheckApiError.lang'
import { warningMessage } from '@/modules/Messages/Messages.plugin'
import { User } from '@/modules/User/User'
import router from '@/router'

i18n.mergeLocaleMessage('en', message.en)
i18n.mergeLocaleMessage('ru', message.ru)

export const checkUnauthorized: CheckError = async (error: Error) => {
  if (error.message?.includes('status code 401')) {
    await User.logout()
    if (router.currentRoute.name !== 'login') {
      warningMessage(String(i18n.t('module.CheckApiError.warning')), [
        String(i18n.t('module.CheckApiError.unauthorized')),
        String(i18n.t('module.CheckApiError.login')),
      ])
      await router.push({ name: 'login' })
    }
    return true
  }
  return false
}

export const checkTimeout: CheckError = (error: Error) => {
  if (
    error.message?.includes('timeout of') &&
    error.message?.includes('exceeded')
  ) {
    warningMessage(String(i18n.t('module.CheckApiError.warning')), [
      String(i18n.t('module.CheckApiError.timeout')),
      String(i18n.t('module.CheckApiError.checkConnect')),
    ])
    return true
  }
  return false
}

export const checkNetworkError: CheckError = (error: Error) => {
  if (error.message?.includes('Network Error')) {
    warningMessage(String(i18n.t('module.CheckApiError.warning')), [
      String(i18n.t('module.CheckApiError.networkError')),
    ])
    return true
  }
  return false
}

export const checkApiError: CheckApiError = error => {
  checkUnauthorized(error)
  checkTimeout(error)
  checkNetworkError(error)
  return false
}
