export const defaultTimeHhmmss = '01:00:00'
export const defaultTimeHhmm = '01:00'

export enum datePickerType {
  date = 'date',
  time = 'time',
  dateTime = 'datetime',
  iso = 'iso',
}

export enum timeFormat {
  hhmmss = 'HH:mm:ss',
  hhmm = 'HH:mm',
}

export const datePickerTypes = {
  time: datePickerType.time,
  date: datePickerType.date,
  dateTime: datePickerType.dateTime,
}

export const timeFormats = {
  hhmmss: timeFormat.hhmmss,
  hhmm: timeFormat.hhmm,
}
