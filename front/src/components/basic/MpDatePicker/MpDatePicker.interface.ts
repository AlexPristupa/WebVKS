import {
  datePickerType,
  timeFormat,
} from '@/components/basic/MpDatePicker/MpDatePicker.const'
import { DateOrder } from '@/modules/DateTime/DateOrder.const'
import { DateTime } from '@/modules/DateTime/DateTime'

export interface IDateMpDatePicker {}

export interface IPropsMpDatePicker {
  value: string
  timePickerOptions: {
    start: string
    step: string
    end: string
    format: timeFormat
  } | null
  timeFormat: timeFormat
  datePickerType: datePickerType
  placeholder: string
  disabled: boolean
  disablePreviousDates: boolean
  disableFromDate: DateTime
  disableFromTime: DateTime
  editable: boolean
  append: boolean
}

export interface IMethodsMpDatePicker {
  getDisabledDate(date: Date): boolean
  getDisabledTime(date: Date): boolean
  changed(value: string): void
  setDefaults(): void
  setDefaultDate(): string
  setDefaultTime(): string
}

export interface IComputedMpDatePicker {
  langDatePicker: IDatePickerLocale

  format: 'DD.MM.YYYY' | 'YYYY-MM-DD' | timeFormat

  dateOrder: DateOrder
}

interface IDatePickerLocale {
  formatLocale: any
  yearFormat: string
  monthFormat: string
  monthBeforeYear: boolean
}
