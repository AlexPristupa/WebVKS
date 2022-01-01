import {
  IResponseGetColumnNameByFilter,
  IResponseGetSelectListFilterName,
  ISavedFilterHeader,
} from '@/modules/Filters/SavingFilter/SavingFilter.interface'
import { methods } from '@/api_services/httpMethods.enum'
import api from '@/api_services'
import { IFilterState } from '@/modules/Filters/Filters.interface'
import { FiltersEndpoints, FilterType } from '@/modules/Filters/Filters.const'

export default class SavingFilter {
  private _tableName = ''
  private _list: Array<ISavedFilterHeader> = []

  static async initAsync(tableName: string) {
    const instance = new SavingFilter()
    instance._tableName = tableName
    instance._list = await instance.getSavingFilterList(tableName)
    instance._list = await instance.getSavingFilterAllModels()
    return instance
  }

  get getList() {
    return this._list
  }

  private async getSavingFilterList(tableName: string) {
    const res: IResponseGetSelectListFilterName = await api.fetchSelectListFilterName(
      {
        method: methods.get,
        data: { tableName: tableName },
      },
    )
    const filterHeaderList: Array<ISavedFilterHeader> = []
    for (const filterHeader of res.selectFilterList) {
      filterHeaderList.push({
        title: filterHeader.text,
        disabled: filterHeader.disabled,
        group: filterHeader.group,
        selected: filterHeader.selected,
        id: +filterHeader.value,
        value: [],
      })
    }
    return filterHeaderList
  }

  private async getSavingFilterAllModels() {
    const list: Array<ISavedFilterHeader> = []
    for (const filter of this._list) {
      list.push(await this.getSavingFilterModel(filter))
    }
    return list
  }

  private async getSavingFilterModel(filter: ISavedFilterHeader) {
    const result: IResponseGetColumnNameByFilter = await api.getColumnNameByFilter(
      {
        method: methods.get,
        data: { filterId: filter.id },
      },
    )
    const arrFilterStateList: Array<IFilterState> = []
    if (result.value) {
      for (const filterValue of result.value) {
        const filterType = await this.getTypeFilter(
          filterValue.id,
          filterValue.columnName,
        )
        if (filterType !== false) {
          const valuesField = await this.getFilterValue(
            +filter.id,
            filterValue.columnName,
            filterType,
          )

          const filterState: IFilterState = {
            filterType: filterType,
            nameField: filterValue.columnName,
            tableName: this._tableName,
            valuesField: valuesField || [],
          }
          arrFilterStateList.push(filterState)
        }
      }
      filter.value = arrFilterStateList
    } else {
      filter.value = []
    }
    return filter
  }

  private async getFilterValue(
    filterId: number,
    columnName: string,
    filterType: FilterType,
  ) {
    const res: Array<any> = await api[FiltersEndpoints[filterType]]({
      method: methods.post,
      data: {
        filterId: filterId,
        columnName: columnName,
        tableName: this._tableName,
      },
    })
    return res
  }

  private async getTypeFilter(
    filterId: number,
    columnName: string,
  ): Promise<FilterType | false> {
    const res = await api.getTypeFilter({
      method: methods.post,
      data: {
        filterId: filterId,
        columnName: columnName,
        tableName: this._tableName,
      },
    })
    switch (res) {
      case 'select':
        return FilterType.select
      case 'string':
        return FilterType.string
      case 'integer':
        return FilterType.integer
      case 'date':
        return FilterType.date
      default:
        return false
    }
  }

  public async updateDataFilterById(filterHeaderId: number) {
    const filterHeader = this._list.find(item => item.id === filterHeaderId)
    if (filterHeader) {
      const newFilterHeaderState = await this.getSavingFilterModel(filterHeader)
      const list = this._list.filter(item => item.id !== filterHeaderId)
      list.push(newFilterHeaderState)
      this._list = list
      return this._list
    } else {
      return false
    }
  }
}
