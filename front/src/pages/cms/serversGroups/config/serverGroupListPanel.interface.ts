import {
  IComputedPanelTable,
  IDataPanelTable,
  IMethodsPanelTable,
  IPropsPanelTable,
} from '@/modules/Panel/IPanelTable.interface'
import { VksServersGroupEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServersGroup.entity'

export interface IDataServerGroupListPanel extends IDataPanelTable {
  selectedEntity: VksServersGroupEntity
}

export interface IMethodsServerGroupListPanel extends IMethodsPanelTable {}

export interface IComputedServerGroupListPanel extends IComputedPanelTable {}

export interface IPropsServerGroupListPanel extends IPropsPanelTable {}
