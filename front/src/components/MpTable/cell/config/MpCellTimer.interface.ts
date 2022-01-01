import { IFieldCounterSettings } from '@/components/MpTable/cell/config/MpCellDateTime.interface'
import { TranslateResult } from 'vue-i18n'

export interface IDataMpCellTimer {
  value: string
  interval?: number
  text: string | TranslateResult
}
export interface IMethodsMpCellTimer {
  updateTimer(): void
}
export interface IPropsMpCellTimer {}
export interface IComputedMpCellTimer {
  fieldData: any
  fieldsSetting: IFieldCounterSettings
  initialDate: Date | string
  isPreviousDate: boolean
}
