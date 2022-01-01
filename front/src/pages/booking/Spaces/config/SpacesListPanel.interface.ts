import {
  IComputedPanelTable,
  IDataPanelTable,
  IMethodsPanelTable,
  IPropsPanelTable,
} from '@/modules/Panel/IPanelTable.interface'

export interface IComputedSpacesListPanel extends IComputedPanelTable {}

export interface IDataSpacesListPanel extends IDataPanelTable {}

export interface IPropsSpacesListPanel extends IPropsPanelTable {}

export interface IMethodsSpacesListPanel extends IMethodsPanelTable {
  setActiveFiltersForTable(ITableHeaderEffects): void
}
