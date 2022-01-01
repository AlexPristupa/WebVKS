import { ISelectFilterQuery } from '@/modules/Filters/SelectFilterFactory/SelectFilterFactory.interface'
import { ISelectFilterOption } from '@/modules/Filters/SelectFilter/SelectFilter.interface'

export interface IDataMpSelectTableFilterInterface {
  forTranslate: Array<string>
  listQueryForOptions: ISelectFilterQuery
  checkedItems: Array<ISelectFilterOption>
  options: Array<ISelectFilterOption>
  checkAll: boolean
  loading: boolean
}
