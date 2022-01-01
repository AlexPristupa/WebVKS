import i18n from '@/i18n'
import { PaginationGapCalculating } from '@/modules/pagination/GapCalculating.interface'

const getGapText = function(total, from, to) {
  return `${i18n.t('pagination.titleFrom')} ${from} ${i18n.t(
    'pagination.titleTo',
  )} ${to} ${i18n.t('pagination.titleOf')} ${total} ${i18n.t(
    'pagination.titleTail',
  )}`
}

const addSpaces = function(value) {
  return value.toString().replace(/(\d)(?=(\d{3})+(\D|$))/g, '$1 ')
}

const paginationGapCalculating: PaginationGapCalculating = data => {
  let from = 1
  let to = 0
  let total = ''
  try {
    if (data.page) {
      from = addSpaces(
        (data.page - 1) * data.pageSize === 0
          ? 1
          : (from = (data.page - 1) * data.pageSize + 1),
      )
      to =
        (data.page - 1) * data.pageSize === 0
          ? data.pageSize
          : data.page * data.pageSize
    }
  } catch (err) {
    console.log(err)
  }

  if (data.total) {
    total = addSpaces(data.total)
    to = to > data.total ? total : addSpaces(to)
    return getGapText(total, from, to)
  }
  return getGapText(0, 0, 0)
}

export default paginationGapCalculating
