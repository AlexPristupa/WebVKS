import { IPaginationConstants } from '@/modules/pagination/Pagination.interface'
import { PaginationLayout } from '@/modules/pagination/Pagination.const'

/**
 * @description Настройка компонента пагинации.
 */
export const pagination: IPaginationConstants = {
  page: 1,
  limit: 20,
  pageSizes: [20, 50, 100, 250, 500],
  perPage: 20,
  total: 0,
  downLayout: PaginationLayout.bottom,
}

/**
 * @description Настройка компонента пагинации.
 */
export const paginationSmallTable: IPaginationConstants = {
  page: 1,
  limit: 5,
  pageSizes: [5, 10, 15],
  perPage: 5,
  total: 0,
  downLayout: PaginationLayout.bottom,
}
