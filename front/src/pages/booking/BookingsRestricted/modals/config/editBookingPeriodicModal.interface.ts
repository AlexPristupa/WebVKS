import {
  IComputedEditModal,
  IDataEditModal,
  IMethodsEditModal,
  IPropsEditModal,
} from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { EditPeriodicDailyEntity } from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodicDaily.interface'
import { EditPeriodicWeeklyEntity } from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodicWeekly.interface'
import { EditPeriodicMonthlyEntity } from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodicMonthly.interface'

export type InitPeriodicDataType =
  | EditPeriodicDailyEntity
  | EditPeriodicWeeklyEntity
  | EditPeriodicMonthlyEntity

export interface IComputedBookingPeriodicModal extends IComputedEditModal {
  frequencyList: Array<IListEntity>
  date: string
  time: string
  tab: number
  dependentTab: number
}

export interface IDataBookingPeriodicModal extends IDataEditModal {
  initData: InitPeriodicDataType
  entityModel: IEntityModelBookingPeriodicModal
}

export interface IPropsBookingPeriodicModal extends IPropsEditModal {}

export interface IMethodsBookingPeriodicModal extends IMethodsEditModal {
  setTab(tab: number): void
  setDefaults(setDefaults: string): void
  resetSchedule(): void
  update(value: IEntityModelBookingPeriodicModal): void
  placeholderToUpper(string: string): string
}

interface IEntityModelBookingPeriodicModal {
  id?: number
  tabs: string
  text: string
  schedule: string
  dateStart: string
}

interface IListEntity {
  id: number
  name: string
}
