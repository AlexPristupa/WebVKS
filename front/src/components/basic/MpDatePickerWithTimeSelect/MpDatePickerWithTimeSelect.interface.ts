import {
  datePickerType,
  timeFormat,
} from '@/components/basic/MpDatePicker/MpDatePicker.const'
import { DateTime } from '@/modules/DateTime/DateTime'

export interface IDateMpDatePickerWithTimeSelect {
  datePickerTypes: { [key: string]: datePickerType }
  timeFormat: timeFormat
  entityModel: {
    date: string
    time: string
  }
}

export interface IPropsMpDatePickerWithTimeSelect {
  initial: string
  datePickerSettings: {
    placeholder: string
    editable: boolean
    disablePreviousDates: boolean
    disableFromDate: string
    disableFromTime: string
  }
}

export interface IMethodsMpDatePickerWithTimeSelect {
  entityChanged(entity: string, value: string): void
  emitChanges(): void
  compareTime(string): void
  setInitialData(): void
  setMinutes(time): string
}

export interface IComputedMpDatePickerWithTimeSelect {
  disableFromDate: DateTime
  disableFromTime: DateTime
}
