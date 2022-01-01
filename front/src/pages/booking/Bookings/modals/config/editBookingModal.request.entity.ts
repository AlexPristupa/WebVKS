import { VksBookingEntity } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBooking.entity'
import { DateTime } from '@/modules/DateTime/DateTime'
import { datePickerType } from '@/components/basic/MpDatePicker/MpDatePicker.const'
import { VksBookingLinkToParticipant } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkToParticipant.entity.ts'
import { VksBookingLinkBookingToVksUsersOthers } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkBookingToVksUsersOthers.entity'

export class EditBookingModalRequestEntity {
  public model: VksBookingEntity

  constructor(permanent, body, timeOffset) {
    const periodic = body.periodic
    const periodicSchedule = periodic ? body.schedule : ''
    const periodicRepeatCount = periodic ? body.repeatCount : 0

    this.model = {
      ...body,
      id: body.id || undefined,
      duration: permanent ? 1440 : body.duration,
      schedule: permanent ? '1day' : periodicSchedule,
      dateStart: EditBookingModalRequestEntity.toTimeZone(
        timeOffset,
        body.dateStart,
      ),
      dateEnd:
        permanent || periodic
          ? EditBookingModalRequestEntity.toTimeZone(timeOffset, body.dateEnd)
          : null,
      repeatCount: permanent ? 0 : periodicRepeatCount,
      delay: permanent ? 0 : body.delay,
      pinCode: body.isUsePin ? body.pinCode : '',
      isSendNotification: permanent ? false : body.isSendNotification,
      isSyncToExchange: permanent ? false : body.isSyncToExchange,
      connectionTypeId: permanent ? 2 : body.connectionTypeId,
      pinSchedule: body.pinPoliticsId === 3 ? body.pinSchedule : '',
      pinDateStart:
        body.pinPoliticsId === 3
          ? EditBookingModalRequestEntity.toTimeZone(
              timeOffset,
              body.pinDateStart,
            )
          : null,
      openConferenceBefore: permanent ? 0 : body.openConferenceBefore,
      periodic: undefined,
      linkBookingToParticipants: body.linkBookingToParticipants.map(
        (participant: VksBookingLinkToParticipant) => ({
          vksParticipantId: participant.id,
          vksUserName: participant.name,
          uri: participant.uri,
          email: participant.email,
          callLegProfileGuid: participant.callLegProfileGuid,
        }),
      ),
      linkBookingToVksUsersOthers: body.linkBookingToVksUsersOthers.map(
        (participant: VksBookingLinkBookingToVksUsersOthers) => ({
          vksUsersOtherId: participant.id.toString().includes('other')
            ? participant.id.toString().replace(/other/g, '')
            : 0,
          vksUserOtherName: participant.name,
          uri: participant.uri,
          email: participant.email,
        }),
      ),
    }
  }

  static toTimeZone(timeOffset: number, date: string): string {
    return new DateTime({ dateTime: date }).toGlobalTime(
      datePickerType.iso,
      timeOffset,
    )
  }
}
