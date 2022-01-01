import {
  IComputedPanelTable,
  IDataPanelTable,
  IMethodsPanelTable,
  IPropsPanelTable,
} from '@/modules/Panel/IPanelTable.interface'

export interface IComputedUserProfileListPanel extends IComputedPanelTable {}

export interface IDataUserProfileListPanel extends IDataPanelTable {}

export interface IPropsUserProfileListPanel extends IPropsPanelTable {}

export interface IMethodsUserProfileListPanel extends IMethodsPanelTable {
  setActiveFiltersForTable(ITableHeaderEffects): void
}
