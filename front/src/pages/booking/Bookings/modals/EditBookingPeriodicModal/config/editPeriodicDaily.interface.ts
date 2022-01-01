import {
  IComputedBookingEditPeriodic,
  IDataBookingEditPeriodic,
  IMethodsBookingEditPeriodic,
  IPropsBookingEditPeriodic,
} from '@/pages/booking/Bookings/modals/EditBookingPeriodicModal/config/editPeriodic.interface'
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
  constructor(data?) {
    if (data) {
      Object.keys(data).forEach(key => {
        this[key] = data[key]
      })
      return this
    }
  }

  public quantity1: number = 1
  public quantity2: number = 1
  public frequency2: number = 1
  public duration1: string = defaultTimeHhmmss
  public duration3: string = defaultTimeHhmmss
}
