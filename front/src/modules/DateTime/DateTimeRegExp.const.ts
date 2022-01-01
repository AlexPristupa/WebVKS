export const DateTimeRegExp = new RegExp(
  /([0][1-9]|[1][0-9]|[2][0-9]|[3][0-1])\.([0][1-9]|[1][0-2])\.([1-2][0-9][0-9][0-9])\s([0][0-9]|[1][0-9]|[2][0-4]):([0-5][0-9]):([0-5][0-9])/,
)
export const DayRegExp = new RegExp(/([0][1-9]|[1][0-9]|[2][0-9]|[3][0-1])/)
export const MonthRegExp = new RegExp(/([0][1-9]|[1][0-2])/)
export const YearRegExp = new RegExp(/([1-2][0-9][0-9][0-9])/)
export const HourRegExp = new RegExp(/([0][0-9]|[1][0-9]|[2][0-4])/)
export const MinuteRegExp = new RegExp(/([0-5][0-9])/)
export const SecondRegExp = new RegExp(/([0-5][0-9])/)
