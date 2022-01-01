import {
  IComputedPanelTable,
  IDataPanelTable,
  IMethodsPanelTable,
  IPropsPanelTable,
} from '@/modules/Panel/IPanelTable.interface'
import { VksSpaceEntity } from '@/modules/ApiDataValidation/ResponseDto/Space/VksSpace.entity'

export interface IComputedEditSpaceBookingTable extends IComputedPanelTable {}

export interface IDataEditSpaceBookingTable extends IDataPanelTable {
  height: string
}

export interface IPropsEditSpaceBookingTable extends IPropsPanelTable {
  selected: VksSpaceEntity
}

export interface IMethodsEditSpaceBookingTable extends IMethodsPanelTable {}
