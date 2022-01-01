import {
  IComputedEditModal,
  IDataEditModal,
  IMethodsEditModal,
  IPropsEditModal,
} from '@/modules/EditEntityModal/IEditEntityModal.interface'
import { EditPeriodicDailyEntity } from '@/pages/booking/Bookings/modals/EditBookingPeriodicModal/config/editPeriodicDaily.interface'
import { EditPeriodicWeeklyEntity } from '@/pages/booking/Bookings/modals/EditBookingPeriodicModal/config/editPeriodicWeekly.interface'
import { EditPeriodicMonthlyEntity } from '@/pages/booking/Bookings/modals/EditBookingPeriodicModal/config/editPeriodicMonthly.interface'

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
  entityModel: BookingPeriodicEntity
}

export interface IPropsBookingPeriodicModal extends IPropsEditModal {}

export interface IMethodsBookingPeriodicModal extends IMethodsEditModal {
  setTab(tab: number): void
  setDefaults(setDefaults: string): void
  resetSchedule(): void
  update(value: BookingPeriodicEntity): void
  placeholderToUpper(string: string): string
}

export class BookingPeriodicEntity {
  constructor(data?) {
    if (data) {
      Object.keys(data).forEach(key => {
        this[key] = data[key]
      })
    }
    return this
  }

  public id: number = 0
  public tabs: string = '2'
  public text: string = ''
  public schedule: string = ''
  public dateStart: string = ''
}

interface IListEntity {
  id: number
  name: string
}
