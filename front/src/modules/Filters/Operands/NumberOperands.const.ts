import { IOperand } from './Operands.interface'
import {
  operandName,
  operandValue,
} from '@/modules/Filters/Operands/Operands.const'

export const numberOperandsConstants: Array<IOperand> = [
  {
    id: operandName.more,
    value: operandValue.more,
    name: operandName.more,
  },
  {
    id: operandName.smaller,
    value: operandValue.smaller,
    name: operandName.smaller,
  },
  {
    id: operandName.moreOrEqual,
    value: operandValue.moreOrEqual,
    name: operandName.moreOrEqual,
  },
  {
    id: operandName.smallerOrEqual,
    value: operandValue.smallerOrEqual,
    name: operandName.smallerOrEqual,
  },
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
]
