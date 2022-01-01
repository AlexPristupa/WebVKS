import {
  operandName,
  operandValue,
} from '@/modules/Filters/Operands/Operands.const'
import { TranslateResult } from 'vue-i18n'

export interface IOperand {
  id: operandName
  value: operandValue | null
  name: operandName | TranslateResult
}
