import {
  IComputedBookingEditPeriodic,
  IDataBookingEditPeriodic,
  IMethodsBookingEditPeriodic,
  IPropsBookingEditPeriodic,
} from '@/pages/booking/Bookings/modals/EditBookingPeriodicModal/config/editPeriodic.interface'
import { defaultTimeHhmmss } from '@/components/basic/MpDatePicker/MpDatePicker.const'
import i18n from '@/i18n'

export interface IDataBookingEditPeriodicMonthly
  extends IDataBookingEditPeriodic {
  inputModel: EditPeriodicMonthlyEntity
}
export interface IMethodsBookingEditPeriodicMonthly
  extends IMethodsBookingEditPeriodic {}
export interface IPropsBookingEditPeriodicMonthly
  extends IPropsBookingEditPeriodic {
  initData: EditPeriodicMonthlyEntity
}
export interface IComputedBookingEditPeriodicMonthly
  extends IComputedBookingEditPeriodic {}

export class EditPeriodicMonthlyEntity {
  public defaultWeekday: string = i18n.t(`button.weekdays.monday`).toString()
  public monthDay1: number = 1
  public monthDay4: number = 1
  public weekday3: string = this.defaultWeekday
  public weekday4: string = this.defaultWeekday
  public duration1: string = defaultTimeHhmmss
  public duration2: string = defaultTimeHhmmss
  public duration3: string = defaultTimeHhmmss
  public duration4: string = defaultTimeHhmmss

  constructor(data?) {
    if (data) {
      Object.keys(data).forEach(key => {
        this[key] = data[key]
      })
    }
    return this
  }
}
