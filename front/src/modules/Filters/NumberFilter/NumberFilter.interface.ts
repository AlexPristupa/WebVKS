import { FilterType } from '@/modules/Filters/Filters.const'

export interface INumberFilterState {
  filterType: FilterType.integer
  nameField: string
  tableName: string
  valuesField: Array<valuesFieldNumberFilter>
}

export type valuesFieldNumberFilter = number
