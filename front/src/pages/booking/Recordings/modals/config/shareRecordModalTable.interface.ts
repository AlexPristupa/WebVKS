import {
  IComputedPanelTable,
  IDataPanelTable,
  IMethodsPanelTable,
  IPropsPanelTable,
} from '@/modules/Panel/IPanelTable.interface'
import { VksRecordingsEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksRecordings.entity'

export interface IDataShareRecordModalTable extends IDataPanelTable {}
export interface IMethodsShareRecordModalTable extends IMethodsPanelTable {}
export interface IComputedShareRecordModalTable extends IComputedPanelTable {}
export interface IPropsShareRecordModalTable extends IPropsPanelTable {
  selected: VksRecordingsEntity
}
