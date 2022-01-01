import { dateMasks } from './dateMasks'
import { layoutName } from './layoutName'
import { pagination, paginationSmallTable } from './pagination'
import { matchClasses } from './matchFilter'
import { matchParams } from './matchFilter'
import { typeFilters } from './typeFilters'
import { typeDocuments } from './typeDocuments'
import { ajaxResult } from './ajaxResult'
import { dateFormat } from './dateFormat'
import { debounce } from './debounce'

/**
 * @description Константы для повсеместного использования
 */

export default {
  dateMasks,
  layoutName,
  pagination,
  paginationSmallTable,
  dateFormat,
  matchClasses,
  matchParams,
  typeDocuments,
  typeFilters,
  ajaxResult,
  duration: 1500,
  debounce,
}
