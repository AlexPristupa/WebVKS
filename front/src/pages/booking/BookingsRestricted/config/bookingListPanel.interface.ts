import {
  IComputedPanelTable,
  IDataPanelTable,
  IMethodsPanelTable,
  IPropsPanelTable,
} from '@/modules/Panel/IPanelTable.interface'
import { currentStatusEnum } from '@/pages/booking/Bookings/config/bookingListPanel.const'

export interface IComputedBookingListPanel extends IComputedPanelTable {}

export interface IDataBookingListPanel extends IDataPanelTable {
  bookPermanentConference: boolean
}

export interface IPropsBookingListPanel extends IPropsPanelTable {}

export interface IMethodsBookingListPanel extends IMethodsPanelTable {
  setActiveFiltersForTable(ITableHeaderEffects): void
  refreshServiceData(id: number): void
  switchStatusTo(status: currentStatusEnum, id: number): void
  toggleTimer(): void
}
