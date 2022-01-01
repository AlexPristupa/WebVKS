import { operandValue } from '@/modules/Filters/Operands/Operands.const'
import { FilterType } from '@/modules/Filters/Filters.const'

export interface IDateFilterState {
  filterType: FilterType.date
  nameField: string
  tableName: string
  valuesField: Array<valuesFieldDateFilterFirst | valuesFieldDateFilterSecond>
}

export interface valuesFieldDateFilterFirst {
  operand?: operandValue
  compareValue?: string
  // Строка или массив строк задающие операнд. Список операндов см. в описании строкового фильтра
  selectFirstContains: operandValue | null
  // Строка или массив строк задающие дату в формате dd.mm.yyyy
  valueFirstDateFilter: string | null
  // Строка или массив строк задающие время в формате hh:mm
  timerFirstFilter: string | null
}

export interface valuesFieldDateFilterSecond {
  operand?: operandValue
  compareValue?: string
  selectSecondContains: operandValue | null
  valueSecondDateFilter: string | null
  timeSecondFilter: string | null
}
