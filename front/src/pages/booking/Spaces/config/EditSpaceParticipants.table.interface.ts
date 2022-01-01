import {
  IComputedPanelTable,
  IDataPanelTable,
  IMethodsPanelTable,
  IPropsPanelTable,
} from '@/modules/Panel/IPanelTable.interface'
import { VksSpaceEntity } from '@/modules/ApiDataValidation/ResponseDto/Space/VksSpace.entity'

export interface IComputedEditSpaceParticipantsTable
  extends IComputedPanelTable {}

export interface IDataEditSpaceParticipantsTable extends IDataPanelTable {
  height: string
  searchString: string
}

export interface IPropsEditSpaceParticipantsTable extends IPropsPanelTable {
  selected: VksSpaceEntity
  tableRows: Array<any>
}

export interface IMethodsEditSpaceParticipantsTable
  extends IMethodsPanelTable {}
