export interface IDateFilterResponse {
  contentType: null
  serializerSettings: null
  statusCode: null
  value: {
    id: number
    isEmptyDate: Array<number>
    operand: Array<string>
    value: Array<string>
  }
}

export interface IDateValueFirst {
  selectFirstContains: string
  valueFirstDateFilter: string
  timerFirstFilter: string
}

export interface IDateValueSecond {
  selectSecondContains: string
  valueSecondDateFilter: string
  timeSecondFilter: string
}
