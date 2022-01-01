import { ISeparator } from '@/modules/Separators/Separators.interface'
import { IOperand } from '@/modules/Filters/Operands/Operands.interface'

export interface IDataMpFilterParametersSelect {
  textarea: string
  separator: ISeparator
  separatorList: Array<ISeparator>
  selectedOperand: IOperand
  operands: Array<IOperand>
}
