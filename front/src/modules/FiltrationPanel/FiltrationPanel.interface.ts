import { FilterType } from '@/modules/Filters/Filters.const'
import { LocaleMessages } from 'vue-i18n'

export interface IFiltrationPanelItem {
  id: number
  filterTitle: string | LocaleMessages
  columnName: string
  profileId: number
  profileName: string
  isCommon: boolean
  filterType: FilterType
  privateName: string
  tableName: string
  isMainTable: boolean
}
