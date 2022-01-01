import { FilterType } from '@/modules/Filters/Filters.const'

export interface ISelectFilterState {
  filterType: FilterType.select
  nameField: string
  tableName: string
  valuesField: Array<valuesFieldSelectFilter>
}

export type valuesFieldSelectFilter = string | number

export interface ISelectFilterOption {
  id: number
  value: string
  translatedValue: string
  valueMember: 'id' | 'value'
}
