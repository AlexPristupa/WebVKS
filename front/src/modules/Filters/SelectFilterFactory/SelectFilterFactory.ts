/**
 * @description Модуль формирования фильтров по запросу
 */
import { ISelectFilterQuery } from './SelectFilterFactory.interface'
import { selectFilterListQuery } from './SelectFilterFactory.const'

export function selectFilterFactory(): ISelectFilterQuery {
  return Object.assign({}, selectFilterListQuery)
}
