import { FilterType } from '@/modules/Filters/Filters.const'
import { IFilterState } from '@/modules/Filters/Filters.interface'

export interface IRequestDataGetSavedFilter {
  filterId: number
  columnName: string
  tableName: string
}

export type GetSavedFilterByType = (
  filterType: FilterType,
  requestData: IRequestDataGetSavedFilter,
) => Promise<IFilterState | boolean>

export type GetSavedFilter = (
  data: IRequestDataGetSavedFilter,
) => Promise<IFilterState | boolean>
