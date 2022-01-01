import { IPaginationConstants } from '@/modules/pagination/Pagination.interface'

export interface IPaginationGapCalculatingData {
  page: IPaginationConstants['page']
  pageSize: number
  total: IPaginationConstants['total']
}

export type PaginationGapCalculating = (
  data: IPaginationGapCalculatingData,
) => string
