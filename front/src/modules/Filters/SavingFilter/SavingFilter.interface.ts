import VueI18n, { TranslateResult } from 'vue-i18n'
import { IFilterState } from '@/modules/Filters/Filters.interface'

export interface IResponseGetSelectListFilterName {
  filtersList: null
  selectFilterList: Array<ISavedFilterHeaderBackend>
  tableName: string
}

export interface ISavedFilterHeaderBackend {
  text: string | TranslateResult
  disabled: boolean
  group: null | boolean // null прилетает с бека, нужно чтобы прилетал boolean
  selected: boolean
  value: string
}

export interface ISavedFilterHeader {
  title: string | TranslateResult | VueI18n.TranslateResult
  disabled: boolean
  group: null | boolean // null прилетает с бека, нужно чтобы прилетал boolean
  selected: boolean
  value: Array<IFilterState>
  id: number
}

export interface IColumnNameByFilter {
  columnName: string
  id: number
  typeId: number // 1
  typeName: string
}

export interface IResponseGetColumnNameByFilter {
  contentType: null
  serializerSettings: null
  statusCode: null
  value: Array<IColumnNameByFilter>
}
