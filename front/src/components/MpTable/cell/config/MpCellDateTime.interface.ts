import { datePickerType } from '@/components/basic/MpDatePicker/MpDatePicker.const'

export interface IMethodsMpCellDateTime {
  modifiedValue(): string | Date
}

export interface IComputedMpCellDateTime {
  value: string
  settings: IFieldDateTimeSettings
}

export interface IMpCellDateTimeSettings {
  [key: string]: IFieldDateTimeSettings | IFieldCounterSettings
}

interface IFieldDateTimeSettings {
  showType: datePickerType
}

export interface IFieldCounterSettings {
  display: boolean
  fieldTo: string
}

export interface IDataMpCellDateTime {}
export interface IPropsmpCellDateTime {}
