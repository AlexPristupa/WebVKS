/**
 * Модуль общего функционала системы
 */
import i18n from '@/i18n'
import store from '@/store'
import Cookies from 'js-cookie'

/**
 * @description Сформировать строку описания пагинации
 * @param {Object} data {page: Number, perpage: Number, total: Number}
 */
export function textPagination(data: {
  page: number
  perpage: number
  total: number
}) {
  let from = 1
  let to = 0
  try {
    if ((data.page - 1) * data.perpage === 0) {
      from = 1
    } else {
      from = (data.page - 1) * data.perpage + 1
    }
    if ((data.page - 1) * data.perpage === 0) {
      to = data.perpage
    } else {
      to = data.page * data.perpage
    }
  } catch (err) {
    // eslint-disable-next-line no-console
    console.log(err)
  }

  if (data.total) {
    return `${i18n.t('pagination.titleFrom')} ${from} ${i18n.t(
      'pagination.titleTo',
    )} ${to > data.total ? data.total : to} ${i18n.t('pagination.titleOf')} ${
      data.total
    } ${i18n.t('pagination.titleTail')}`
  } else {
    return `${i18n.t('pagination.titleFrom')} ${from} ${i18n.t(
      'pagination.titleTo',
    )} ${to} ${i18n.t('pagination.titleOf')} 0 ${i18n.t(
      'pagination.titleTail',
    )}`
  }
}

/**
 * @description Перевести имена свойств объекта в PascalCase
 * @param {Object} v
 */
export function fieldNameToPascalCase(v: {
  [x: string]: any
  hasOwnProperty: (arg0: string) => any
}) {
  const o: { [key: string]: any } = {}
  for (const key in v) {
    // eslint-disable-next-line no-prototype-builtins
    if (v.hasOwnProperty(key)) {
      const newKey = key.substring(0, 1).toUpperCase() + key.substring(1)
      o[newKey] = v[key]
    }
  }
  return o
}

/**
 * @description Прочитать куки полученные с портала для Title страницы
 */
export function getCoockiePortalOptions() {
  store.dispatch('cookie/setPortalOptions', Cookies.get('portalOptions'))
  store.dispatch('user/setUserName', Cookies.get('user'))
}
