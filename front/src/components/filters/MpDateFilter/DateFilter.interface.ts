import {
  IDateFilterState,
  valuesFieldDateFilterFirst,
  valuesFieldDateFilterSecond,
} from '@/modules/Filters/DateFilter/DateFilter.interface'
import { operandValue } from '@/modules/Filters/Operands/Operands.const'

export interface IDataDateFilter {
  filterBody: IDateFilterState
  firstSelectsState: IInitialDateFilterState
  secondSelectsState: IInitialDateFilterState
  notSpecifiedValue: Array<
    valuesFieldDateFilterFirst | valuesFieldDateFilterSecond
  >
}

export interface IInitialDateFilterState {
  operand: operandValue | null
  date: string | null
  time: string | null
}
