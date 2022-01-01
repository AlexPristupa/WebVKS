import { TableName } from '@/modules/table_grid/TableName.const'

export interface IUserSettings {
  twoHorizontalPanelsLayoutList: ILayoutItem
  openedStructure: IOpenedStructure | {}
}

export interface ILayoutItem {
  [key: string]: ILayoutItemData
}

export interface ILayoutItemPayload {
  name: string
  data: ILayoutItemData
}

export interface ILayoutItemData {
  sizes: ILayoutItemDataSizes
  isOpen: ILayoutItemDataIsOpen
}

export interface ILayoutItemDataSizes {
  top: number
  bottom: number
}

export interface ILayoutItemDataIsOpen {
  top: boolean
  bottom: boolean
}

export interface IOpenedStructurePayload {
  tableName: TableName
  dataToOpen: IDataToOpen
}

export type IOpenedStructure = {
  [key in TableName]: IDataToOpen
}

export interface IDataToOpen {
  [key: string]: Array<number>
}
