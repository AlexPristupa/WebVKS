import { PaginationPosition } from '@/modules/pagination/Pagination.const.ts'
import { TableDto } from '@/modules/dto/classesDto/Table.Dto'

export interface IDataMpEditingHistory {
  loading: boolean
  paginationTotal: number
  paginationPositionBottom: PaginationPosition
  rowData: Array<Record<string, any>>
  listQuery: TableDto
}
