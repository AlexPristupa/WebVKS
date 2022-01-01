export interface IDtoCheckValue {
  columnName: string
  tableName: string
  checkList: Array<string>
}

export interface IResponseCheckValue {
  notFound: Array<string>
  found: Array<string>
}
