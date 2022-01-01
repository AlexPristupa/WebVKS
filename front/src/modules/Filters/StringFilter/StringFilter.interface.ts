import { operandValue } from '@/modules/Filters/Operands/Operands.const'
import { FilterType } from '@/modules/Filters/Filters.const'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'

export interface IStringFilterState {
  filterType: FilterType.string
  nameField: string
  tableName: string
  valuesField: Array<IValuesFieldStringFilter>
}

export interface IValuesFieldStringFilter {
  operand: operandValue
  compareValue: string
}

export type ButtonNameStringFilter = MpTypeButton.add | MpTypeButton.remove

export interface IDataMpStringFilter {
  valueList: Array<IValuesFieldStringFilter>
}
