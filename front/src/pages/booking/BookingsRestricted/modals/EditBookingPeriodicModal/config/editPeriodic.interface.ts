import {
  datePickerType,
  timeFormat,
} from '@/components/basic/MpDatePicker/MpDatePicker.const'
import VueI18n from 'vue-i18n'
import TranslateResult = VueI18n.TranslateResult
import { InitPeriodicDataType } from '@/pages/booking/BookingsRestricted/modals/config/editBookingPeriodicModal.interface'

export interface IListItem {
  id: number
  name: TranslateResult
}

export interface IDataBookingEditPeriodic {
  list: Array<IListItem>
  frequencyList?: Array<IListItem>
  weekdaysList?: Array<IListItem>
  weekdaysButtons?: Array<IListItem>
  datePickerTypes: {
    [key: string]: datePickerType
  }
  timeFormats: {
    [key: string]: timeFormat
  }
}
export interface IMethodsBookingEditPeriodic {
  setModel(): void
  setTab(tab: number): void
  entityChanged(
    entity: string,
    tab: number,
    value: number | string | Array<string>,
  ): void
  getWidthTitle(id: number): string
  getWidthSettings(id: number): string
}
export interface IPropsBookingEditPeriodic {
  date: string
  tabs: string
  initData: InitPeriodicDataType
}
export interface IComputedBookingEditPeriodic {
  tab: number
}
