import { FilterType } from '@/modules/Filters/Filters.const'
import { ISeparator } from '@/modules/Separators/Separators.interface'
import { IOperand } from '@/modules/Filters/Operands/Operands.interface'
import { FilterParameters } from '@/modules/Filters/FilterParameters/FilterParameters'
import { IResponseCheckValue } from '@/modules/Filters/FilterParameters/FilterParameters.interface'

export interface IDataMpFilterParametersWrapper {
  visible: boolean
  enumFilterType: {
    string: FilterType
    select: FilterType
  }
  formData: {
    string: IFilterParametersUpdateEventString | null
    select: IFilterParametersUpdateEvent | null
  }
  checkedData: IResponseCheckValue | null
  filterParameters: FilterParameters
}

export interface IFilterParametersUpdateEvent {
  text: string
  separator: ISeparator
}

export interface IFilterParametersUpdateEventString
  extends IFilterParametersUpdateEvent {
  operand: IOperand
}
