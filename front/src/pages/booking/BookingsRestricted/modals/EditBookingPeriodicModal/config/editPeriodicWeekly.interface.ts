import {
  IComputedBookingEditPeriodic,
  IDataBookingEditPeriodic,
  IMethodsBookingEditPeriodic,
  IPropsBookingEditPeriodic,
} from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodic.interface'
import VueI18n from 'vue-i18n'
import TranslateResult = VueI18n.TranslateResult
import { weekdays } from '@/pages/booking/BookingsRestricted/modals/EditBookingPeriodicModal/config/editPeriodicMonthly.const'
import { weekdaysEnum } from '@/domain'
import i18n from '@/i18n'
import { defaultTimeHhmmss } from '@/components/basic/MpDatePicker/MpDatePicker.const'

export interface IDataBookingEditPeriodicWeekly
  extends IDataBookingEditPeriodic {
  inputModel: EditPeriodicWeeklyEntity
}
export interface IMethodsBookingEditPeriodicWeekly
  extends IMethodsBookingEditPeriodic {
  setWeekdays(name: TranslateResult): void
  isButtonIncluded(name: TranslateResult): boolean
}
export interface IPropsBookingEditPeriodicWeekly
  extends IPropsBookingEditPeriodic {
  initData: EditPeriodicWeeklyEntity
}
export interface IComputedBookingEditPeriodicWeekly
  extends IComputedBookingEditPeriodic {}

export class EditPeriodicWeeklyEntity {
  public defaultWeekdays: Array<weekdaysEnum> = [
    weekdays[i18n.t(`button.weekdaysShortcuts.monday`).toString()],
  ]
  public duration: string = defaultTimeHhmmss
  public weekdays: Array<weekdaysEnum> = this.defaultWeekdays

  constructor(data?) {
    if (data) {
      Object.keys(data).forEach(key => {
        this[key] = data[key]
      })
    }
    return this
  }
}
