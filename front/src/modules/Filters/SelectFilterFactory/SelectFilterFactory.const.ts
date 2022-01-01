import { ISelectFilterQuery } from './SelectFilterFactory.interface'

export const selectFilterListQuery: ISelectFilterQuery = {
  filterId: -1,
  columnName: '',
  tableName: '',
  search: null,
  checkList: [],
  addParameters: [],
}

export enum voIpCallStatusEnum {
  Unknown = 0,
  Bad = 1,
  Poor = 2,
  Fair = 3,
  Good = 4,
  Excellent = 5,
}

export enum voIpCallsStatusValue {
  Unknown = 'Неизвестно',
  Bad = 'Неприемлемое',
  Poor = 'Низкое',
  Fair = 'Среднее',
  Good = 'Хорошее',
  Excellent = 'Отличное',
}
