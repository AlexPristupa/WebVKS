import { IFilterState } from '@/modules/Filters/Filters.interface'

export interface ITableHeaderEffectsState {
  headersEffects: Array<ITableHeaderEffects>
}

export interface ITableHeaderEffects {
  tableName: string
  effects: IHeaderEffect
}

export interface IHeaderEffect {
  filters?: Array<IFilterState>
  whereMenuIsOpened?: string
  checkAll?: boolean
  sorted?: string
}
