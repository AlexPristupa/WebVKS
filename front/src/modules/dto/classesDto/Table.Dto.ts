import CONSTANTS from '@/constants'
import { sortType } from '@/modules/Sort/Sort.const'
import { IFilterState } from '@/modules/Filters/Filters.interface'
import { TableName } from '@/modules/table_grid/TableName.const'
import { IPaginationConstants } from '@/modules/pagination/Pagination.interface'

export class TableDto {
  public tableName: TableName = TableName.unknown
  public page: IPaginationConstants['page'] = CONSTANTS.pagination.page
  public limit: IPaginationConstants['limit'] = CONSTANTS.pagination.limit
  public tableSearchBy: string = ''
  public orderBy: sortType = sortType.asc
  public sortField: string = 'id'
  public dateStart: string | null = null
  public dateFinish: string | null = null
  public filters: Array<IFilterState> = []
  public chkCurrentPageExport: boolean = true
  public extensionFilters: Array<IFilterState> = []
}
