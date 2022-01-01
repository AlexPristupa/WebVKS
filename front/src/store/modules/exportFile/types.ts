import { CancelTokenSource } from 'axios'
import { TableName } from '@/modules/table_grid/TableName.const'

export interface IExportState {
  cancelTokenSources: ITablesExportToken | {}
  hidden: IHiddenExport | {}
}

export type ITablesExportToken = {
  [key in TableName]: CancelTokenSource
}

export type IHiddenExport = {
  [key in TableName]: boolean
}

export interface IHiddenData {
  tableName: TableName
  isHidden: boolean
}

export interface ITokenData {
  tableName: TableName
  token?: CancelTokenSource
}
