import { periodicTypes } from '@/pages/booking/Bookings/modals/config/editBookingModal.const'
import { methods } from '@/api_services/httpMethods.enum'
import api from '@/api_services'
import { FormValidation } from '@/modules/FormValidation/FormValidation'
import { errorMessage } from '@/modules/Messages/Messages.plugin'
import { VksSelectOptionEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOption.entity'
import { initialTimeZone } from '@/modules/DateTime/InitialTime.const'
import { DateTime } from '@/modules/DateTime/DateTime'
import { datePickerType } from '@/components/basic/MpDatePicker/MpDatePicker.const'
import { VksSelectOptionWithPrivateEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOptionWithPrivate.entity'
import { VksTimeZoneEntity } from '@/modules/ApiDataValidation/ResponseDto/TimeZone/VksTimeZone.entity'
import { VksBookingEntity } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBooking.entity'

export async function getOptionsSpaces(
  search?: string,
): Promise<Array<VksSelectOptionEntity>> {
  const res = await api.dictionarySpaces({
    method: methods.get,
    data: {
      limit: 300,
      search: search || '',
    },
  })
  return res || []
}

export async function getOptionsParticipantsAndOwner(
  search?: string,
): Promise<Array<VksSelectOptionEntity>> {
  const res = await api.dictionaryVksUser({
    method: methods.get,
    data: {
      limit: 300,
      search: search || '',
    },
  })
  return res || []
}

export async function getOptionsParticipantsOthers(
  search?: string,
): Promise<Array<VksSelectOptionEntity>> {
  const res = await api.dictionaryVksUserOthers({
    method: methods.get,
    data: {
      limit: 300,
      search: search || '',
    },
  })
  return res || []
}

export async function getOptionsOwnerByLogin(
  search?: string,
): Promise<Array<VksSelectOptionEntity>> {
  const res = await api.bookingGetUserByLogin({
    method: methods.get,
    data: {
      jid: search || '',
    },
  })
  return res || []
}

export async function getConnectionTypeOptions(
  search?: string,
): Promise<Array<VksSelectOptionWithPrivateEntity>> {
  const res = await api.dictionaryConnectionType({
    method: methods.get,
    data: {
      limit: 300,
      search: search || '',
    },
  })
  return res || []
}

export async function getTimeZoneOptions(
  search?: string,
): Promise<{ list: Array<VksTimeZoneEntity>; initial: number }> {
  const res = await api.dictionaryTimeZone({
    method: methods.get,
    data: {
      limit: 300,
      search: search || '',
    },
  })
  const initialZone = res.find(
    timeZone => timeZone.privateName === initialTimeZone,
  )
  return { list: res || [], initial: initialZone?.id }
}

export async function getBookingTypeOptions(
  periodic: boolean,
  permanent: boolean,
): Promise<number | null> {
  const search = periodic ? periodicTypes.periodic : periodicTypes.onetime
  const res = await api.dictionaryBookingType({
    method: methods.get,
    data: {
      limit: 300,
      search: !permanent ? search : periodicTypes.constant,
    },
  })
  return res?.length ? res[0].id : null
}

export async function findIntersection(
  model: VksBookingEntity,
  id: number,
): Promise<boolean> {
  const res = await api.bookingFindIntersection({
    method: methods.post,
    data: {
      schedule: model.schedule,
      dateStart: model.dateStart,
      dateEnd: model.dateEnd,
      timeZone: model.timeZone,
      duration: model.duration,
      repeatCount: model.repeatCount,
      openConferenceBefore: model.openConferenceBefore,
      spaceId: model.spaceId,
      bookingId: id || undefined,
    },
  })
  if (res && !res.validation) {
    return false
  } else {
    const validationWithoutField = FormValidation.backValidationWithoutField(
      res.validation,
    )
    if (validationWithoutField) {
      errorMessage('', validationWithoutField)
    }
    return true
  }
}

export function getTimezoneOffset(
  options: Array<VksTimeZoneEntity>,
  id: number,
): number {
  const timeZone = options.find(timeZone => timeZone.id === id)
  return timeZone?.offsetMinute || 0
}

export function setCheckedFirst(list: Array<any>, checked: any) {
  const checkedIdList = checked.map(item => item.id)
  const checkedList = checked
  const listWithoutChecked: Array<VksSelectOptionEntity> = list.filter(
    item => !checkedIdList.includes(item.id),
  )
  return checkedList.concat(listWithoutChecked as any)
}

export async function getPeriodicText(
  schedule: string,
  dateStart: string,
): Promise<string> {
  if (schedule && dateStart) {
    const res = await api.bookingScheduleText({
      method: methods.post,
      data: {
        nameSchedule: 'Conference',
        schedule: schedule,
        dateStart: new DateTime({ dateTime: dateStart }).toGlobalTime(
          datePickerType.iso,
        ),
        timeZone: 0,
      },
    })
    if (res) {
      const dateStart = new DateTime({
        dateTime: res.dateStart,
      }).toCurrentTimeZone(datePickerType.time)
      return res.scheduleText.replace(/#date#/g, `${dateStart} `)
    }
  }
  return ''
}
