import api from '@/api_services'
import { methods } from '@/api_services/httpMethods.enum'
import { IFilterState } from '@/modules/Filters/Filters.interface'
import { FilterType } from '@/modules/Filters/Filters.const'
import {
  GetSavedFilter,
  GetSavedFilterByType,
} from '@/modules/Filters/GetSavedFilter.interface'

export const getSelectFilter: GetSavedFilter = async data => {
  try {
    const resultSelect: Array<string> = await api.getActiveStringFilter({
      method: methods.post,
      data: data,
    })
    let obModel: IFilterState | boolean = false
    if (resultSelect) {
      obModel = {
        tableName: data.tableName,
        nameField: data.columnName,
        filterType: FilterType.select,
        valuesField: resultSelect,
      }
    }
    return obModel
  } catch (e) {
    console.log(e)
    return false
  }
}

export const applyForFilter: GetSavedFilterByType = async (
  filterType,
  requestData,
) => {
  let obModel: IFilterState | boolean = false
  switch (filterType) {
    case FilterType.select:
      obModel = await getSelectFilter(requestData)
      break
  }
  return obModel
}
