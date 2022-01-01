import { dateFormat } from '@/constants/dateFormat'
import { propertyPrivateName } from '@/domain'
import { TableName } from '@/modules/table_grid/TableName.const'
import { RowNode } from '@ag-grid-community/core'

export interface IDataMpCellEditValue {
  cellDataString: string
  cellDataCatalog: {
    // todo исправить, когда начну работать с этим компонентом
    [key: string]: any
  }
  cellDataDate: string | Date
  // todo исправить, когда начну работать с этим компонентом
  catalogList: Array<any>
  loadingCatalog: boolean
  format: dateFormat
}

export interface IMethodsMpCellEditValue {
  changeCatalog(item): void
  inputValue(value): void
  changeDatePicker(value): void
  searchInCatalog(input): void
  focusCatalog(): void
  getCatalog(search?: string): void
}

export interface IComputedMpCellEditValue {
  editing: boolean
  cellData: string
  idForEmpty: number
  showInput: boolean
  showCatalog: boolean
  showDatePicker: boolean
  isTitleGroup: boolean
  privateName: propertyPrivateName | string
  tableName: TableName
  nodeList: Array<RowNode>
  // todo исправить, когда начну работать с этим компонентом
  setting: any // тут должен быть набор классов | null
  disabled: boolean
  // todo исправить, когда начну работать с этим компонентом
  filteredCatalogList: Array<any>
}
export interface IPropsMpCellEditValue {}
