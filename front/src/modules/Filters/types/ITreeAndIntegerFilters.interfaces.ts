// todo переименовать и разделить в два файла при переносе остальных фильтров
export interface IIntegerResponse {
  contentType: null
  serializerSettings: null
  statusCode: null
  value: Array<IIntegerResponseValue>
}

export interface IIntegerResponseValue {
  id?: number
  value: string
  operand: string
  compareValue?: string
  isEmptyDate?: number | boolean
}

export interface IIntegerFilerValue {
  operand: string
  compareValue: any
}
