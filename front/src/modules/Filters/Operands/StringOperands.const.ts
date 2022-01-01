import { IOperand } from './Operands.interface'
import {
  operandName,
  operandValue,
} from '@/modules/Filters/Operands/Operands.const'

export const stringOperandsConstants: Array<IOperand> = [
  {
    id: operandName.equal,
    value: operandValue.equal,
    name: operandName.equal,
  },
  {
    id: operandName.notEqual,
    value: operandValue.notEqual,
    name: operandName.notEqual,
  },
  {
    id: operandName.includesExactly,
    value: operandValue.includesExactly,
    name: operandName.includesExactly,
  },
  {
    id: operandName.notIncludesExactly,
    value: operandValue.notIncludesExactly,
    name: operandName.notIncludesExactly,
  },
  {
    id: operandName.includesLeft,
    value: operandValue.includesLeft,
    name: operandName.includesLeft,
  },
  {
    id: operandName.includesRight,
    value: operandValue.includesRight,
    name: operandName.includesRight,
  },
  {
    id: operandName.includes,
    value: operandValue.includes,
    name: operandName.includes,
  },
  {
    id: operandName.notIncludes,
    value: operandValue.notIncludes,
    name: operandName.notIncludes,
  },
]
