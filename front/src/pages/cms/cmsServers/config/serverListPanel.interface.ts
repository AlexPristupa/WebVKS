import {
  IComputedPanelTable,
  IDataPanelTable,
  IMethodsPanelTable,
  IPropsPanelTable,
} from '@/modules/Panel/IPanelTable.interface'
import { VksServerEntity } from '@/modules/ApiDataValidation/ResponseDto/ColumnFilter/VksServers.entity'

/**
 * @description интерфейсы компонента ServerListPanel
 */
export interface IDataServerListPanel extends IDataPanelTable {
  selectedEntity: VksServerEntity
  externalFilter: any
}

export interface IMethodsServerListPanel extends IMethodsPanelTable {}

export interface IComputedServerListPanel extends IComputedPanelTable {}

export interface IPropsServerListPanel extends IPropsPanelTable {}
