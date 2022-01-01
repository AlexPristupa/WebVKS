import {
  voIpCallStatusEnum,
  voIpCallsStatusValue,
} from './SelectFilterFactory.const'

export interface ISelectFilterQuery {
  filterId: number | null
  columnName: string
  tableName: string
  search: null | string
  checkList: Array<string>
  addParameters: Array<any>
}

export interface IResponseOptionSelectFilter {
  id: number
  state: 'check' | null
  value: string
  voIpCallStatusEnum: voIpCallStatusEnum
  voIpCallsStatusValue: voIpCallsStatusValue
}

export interface ISelectFilterResponse {
  checkAll: 'check' | null
  columnForStringFilter: Array<IResponseOptionSelectFilter>
}
