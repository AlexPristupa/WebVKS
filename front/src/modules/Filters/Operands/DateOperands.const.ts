import i18n from '@/i18n'
import { IOperand } from '@/modules/Filters/Operands/Operands.interface'
import {
  operandName,
  operandValue,
} from '@/modules/Filters/Operands/Operands.const'

const dateOperandsConstants: Array<IOperand> = [
  {
    id: operandName.more,
    value: operandValue.more,
    name: i18n.t('general.filtersOptions.more'),
  },
  {
    id: operandName.smaller,
    value: operandValue.smaller,
    name: i18n.t('general.filtersOptions.smaller'),
  },
  {
    id: operandName.moreOrEqual,
    value: operandValue.moreOrEqual,
    name: i18n.t('general.filtersOptions.moreOrEqual'),
  },
  {
    id: operandName.smallerOrEqual,
    value: operandValue.smallerOrEqual,
    name: i18n.t('general.filtersOptions.smallerOrEqual'),
  },
  {
    id: operandName.equal,
    value: operandValue.equal,
    name: i18n.t('general.filtersOptions.equal'),
  },
  {
    id: operandName.notEqual,
    value: operandValue.notEqual,
    name: i18n.t('general.filtersOptions.notEqual'),
  },
  {
    id: operandName.notSpecified,
    value: null,
    name: i18n.t('general.filtersOptions.notSpecified'),
  },
]

export default dateOperandsConstants
