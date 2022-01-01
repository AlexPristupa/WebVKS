import {
  IComputedBookingEditPeriodic,
  IDataBookingEditPeriodic,
  IMethodsBookingEditPeriodic,
  IPropsBookingEditPeriodic,
} from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodic.interface'
import { defaultTimeHhmmss } from '@/components/basic/MpDatePicker/MpDatePicker.const'

export interface IDataBookingEditPeriodicDaily
  extends IDataBookingEditPeriodic {
  inputModel: EditPeriodicDailyEntity
}
export interface IMethodsBookingEditPeriodicDaily
  extends IMethodsBookingEditPeriodic {}
export interface IPropsBookingEditPeriodicDaily
  extends IPropsBookingEditPeriodic {
  initData: EditPeriodicDailyEntity
}
export interface IComputedBookingEditPeriodicDaily
  extends IComputedBookingEditPeriodic {}

export class EditPeriodicDailyEntity {
  public quantity1: number = 1
  public quantity2: number = 1
  public frequency2: number = 1
  public duration1: string = defaultTimeHhmmss
  public duration3: string = defaultTimeHhmmss

  constructor(data?) {
    if (data) {
      Object.keys(data).forEach(key => {
        this[key] = data[key]
      })
      // this.quantity1 = data.quantity1 || 1
      // this.quantity2 = data.quantity2 || 1
      // this.frequency2 = data.frequency2 || 1
      // this.duration1 = data.duration1 || defaultTimeHhmmss
      // this.duration3 = data.duration3 || defaultTimeHhmmss
      return this
    }
  }
}
