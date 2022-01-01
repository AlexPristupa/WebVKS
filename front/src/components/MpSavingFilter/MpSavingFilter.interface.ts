import { SavingFilterFunctionType } from '@/components/MpSavingFilter/MpSavingFilter.const'
import { LocaleMessages, TranslateResult } from 'vue-i18n'
import { TableDto } from '@/modules/dto/classesDto/Table.Dto'

export interface IDtoSavingFilter {
  listQuery: TableDto
  func: SavingFilterFunctionType
  newName: string | TranslateResult | LocaleMessages
  iscommon: '1' | '0'
  filterId: number | string // todo перепроверить. скорее всего string лишний
}

export interface IFormSavingFilterDialog {
  name: string | TranslateResult
  isCommon: 'personal' | 'general'
}
