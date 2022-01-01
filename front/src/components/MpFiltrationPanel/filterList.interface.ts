import { IFilterState } from '@/modules/Filters/Filters.interface'

export interface IStateFilterList {
  main: {
    [key: number]: IFilterState
  }
  extension: {
    [key: number]: IFilterState
  }
}

export interface IFilterListItem {
  id: number | null
  tableId: number | null
  columnName: columnName | undefined
  filterTypeId: FilterTypeId
  dataQuery: string | null
  conditionColumn: string | null
  displayMember: string | null
  valueMember: string | null
  title: string
  filterSql: string | null
  whereColumn: string | null
  isTableColumn: boolean
  filterType: null
  table: null
  filterValue: []
}

export enum FilterTypeId {
  string = 4,
  select = 1,
  number = 3,
  boolean = 10,
}

export enum columnName {
  EMAIL = 'EMAIL',
  EMPLOYEE_NOTIFY_EMAIL = 'EMPLOYEE_NOTIFY_EMAIL',
  EMPLOYEE_LIMIT = 'EMPLOYEE_LIMIT',
  EMPLOYEE_GOSB = 'EMPLOYEE_GOSB',
  EMPLOYEE_FIRE_DATE = 'EMPLOYEE_FIRE_DATE',
  post = 'post',
  EMPLOYEE_FILE_ON_HOLD = 'EMPLOYEE_FILE_ON_HOLD',
  EMPLOYEE_B2B_CNT = 'EMPLOYEE_B2B_CNT',
  EMPLOYEE_M2M_CNT = 'EMPLOYEE_M2M_CNT',
  MOB_LIMIT_COLLECTIVE = 'MOB_LIMIT_COLLECTIVE',
  EMPLOYEE_NOTE = 'EMPLOYEE_NOTE',
  LOGIN = 'LOGIN',
  EMPLOYEE_LIMIT_TYPE = 'EMPLOYEE_LIMIT_TYPE',
  MANAGERNAME = 'MANAGERNAME',
  name = 'name',
  EMPLOYEE_NOTIFY_TRANSPORT = 'EMPLOYEE_NOTIFY_TRANSPORT',
  EMPLOYEE_STATUS = 'EMPLOYEE_STATUS',
  structure = 'structure',
  EMPLOYEE_YEAR_EXPENSES = 'EMPLOYEE_YEAR_EXPENSES',
  TABNUMBER = 'TABNUMBER',
  phone = 'phone',
  EMPLOYEE_NOTIFY_PHONE = 'EMPLOYEE_NOTIFY_PHONE',
  EMPLOYEE_TB = 'EMPLOYEE_TB',
  COSTCENTER = 'COSTCENTER',
}
