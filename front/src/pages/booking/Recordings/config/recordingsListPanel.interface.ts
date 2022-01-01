import {
  IComputedPanelTable,
  IDataPanelTable,
  IMethodsPanelTable,
  IPropsPanelTable,
} from '@/modules/Panel/IPanelTable.interface'
import { downloadTypes } from '@/api_services/downloadFile/downloadFile.const'
import { CancelTokenSource } from 'axios'
import { TableName } from '@/modules/table_grid/TableName.const'

export interface IComputedRecordingsListPanel extends IComputedPanelTable {
  getCancelTokenSource(
    tableName: TableName,
  ):
    | CancelTokenSource
    | {
        cancel: null
        token: null
      }
  source:
    | CancelTokenSource
    | {
        cancel: null
        token: null
      }
}

export interface IMethodsRecordingsListPanel extends IMethodsPanelTable {
  setActiveFiltersForTable(ITableHeaderEffects): void
  interruptDownloading(): void
  downloadRecord(id: number, type: downloadTypes): void
  deleteCancelTokenSource({ tableName: TableName }): void
  setCancelTokenSource({ tableName: TableName, token: CancelTokenSource }): void
}

export interface IDataRecordingsListPanel extends IDataPanelTable {
  visibleVideoPlayer: boolean
  visibleShareModal: boolean
  videoSrc: string
  loadingPath: boolean
}

export interface IPropsRecordingsListPanel extends IPropsPanelTable {}
