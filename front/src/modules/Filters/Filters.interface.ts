import {
  ISelectFilterState,
  valuesFieldSelectFilter,
} from '@/modules/Filters/SelectFilter/SelectFilter.interface'
import {
  IStringFilterState,
  IValuesFieldStringFilter,
} from '@/modules/Filters/StringFilter/StringFilter.interface'
import {
  INumberFilterState,
  valuesFieldNumberFilter,
} from '@/modules/Filters/NumberFilter/NumberFilter.interface'
import {
  IDateFilterState,
  valuesFieldDateFilterFirst,
  valuesFieldDateFilterSecond,
} from '@/modules/Filters/DateFilter/DateFilter.interface'
import Vue from 'vue'

export type IFilterState =
  | ISelectFilterState
  | IStringFilterState
  | INumberFilterState
  | IDateFilterState

export type IValuesFieldFilter =
  | Array<valuesFieldSelectFilter>
  | Array<IValuesFieldStringFilter>
  | Array<valuesFieldNumberFilter>
  | Array<valuesFieldDateFilterFirst | valuesFieldDateFilterSecond>

export interface IComponentFilter extends Vue {
  clearState(): void
  emitState(): void
}
