import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import {
  IsBoolean,
  IsDefined,
  IsInt,
  IsNumber,
  IsString,
} from 'class-validator'
import { Expose, Type } from 'class-transformer'
import { DateTime } from '@/modules/DateTime/DateTime'
import { datePickerType } from '@/components/basic/MpDatePicker/MpDatePicker.const'
import { VksBookingLinkToParticipant } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkToParticipant.entity.ts'
import { VksBookingLinkBookingToVksUsersOthers } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkBookingToVksUsersOthers.entity'

export class VksBookingEntity implements ITableRowData, IEntity {
  toValidationName(): string {
    return 'VksBookingEntity'
  }

  constructor(data?, timeOffset?: number) {
    if (data) {
      Object.keys(data).forEach(key => {
        if (
          timeOffset &&
          data[key] &&
          (key === 'dateStart' || key === 'pinDateStart' || key === 'dateEnd')
        ) {
          this[key] = new DateTime({
            dateTime: data[key],
          }).toChosenTimeZone(timeOffset, datePickerType.dateTime)
        } else {
          const seconds = data.duration * 60
          const hour = Math.floor(seconds / 60 / 60)
          const minute = Math.floor(seconds / 60) - hour * 60
          this.hour = hour * 60
          this.minute = minute
          this[key] = data[key]
        }
      })
      this.periodic = data.typeId === 2
    }
  }

  @Expose()
  @IsDefined()
  @IsInt()
  public id: number = 0

  @Expose()
  @IsDefined()
  @IsString()
  public name: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public description: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public location: string = ''

  @Expose()
  @IsDefined()
  @IsNumber()
  public ownerId: number | null = null

  @Expose()
  @IsDefined()
  @IsNumber()
  public typeId: number | null = null

  @Expose()
  public dateStart: string = new DateTime({
    date: new Date(),
  }).getDateAndTimeToString()

  @Expose()
  public timeZone: number = 75

  @Expose()
  public duration: number = 60

  public hour: number = 60
  public minute: number = 0

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isUsePin: boolean = true

  @Expose()
  @IsDefined()
  @IsString()
  public pinCode: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public schedule: string | null = '1day'

  @Expose()
  public connectionTypeId: number | null = 1

  @Expose()
  public attemptsCount: number | null = 2

  @Expose()
  public delay: number | null = 60

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isSendNotification: boolean = true

  @Expose()
  public openConferenceBefore: number | null = 5

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isSyncToExchange: boolean = true

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isNeverUsePin: boolean = false

  @Expose()
  public dateEnd: string = new DateTime({
    date: new Date(new Date().setFullYear(new Date().getFullYear() + 1)),
  }).getDateAndTimeToString()

  @Expose()
  public repeatCount: number | null = 0

  @Expose()
  public pinPoliticsId: number | null = 3

  @Expose()
  public pinSchedule: string | null = 'days:Friday'

  @Expose()
  @IsDefined()
  @IsString()
  public pinDateStart: string =
    new DateTime({
      date: new Date(),
    }).getDateToString() + ' 03:00'

  @Expose()
  public spaceId: number | null = 0

  @Expose()
  @IsDefined()
  @Type(() => VksBookingLinkToParticipant)
  public linkBookingToParticipants: Array<VksBookingLinkToParticipant> = []

  @Expose()
  @IsDefined()
  @Type(() => VksBookingLinkBookingToVksUsersOthers)
  public linkBookingToVksUsersOthers: Array<
    VksBookingLinkBookingToVksUsersOthers
  > = []

  @Expose()
  @IsDefined()
  @IsString()
  public scheduleTab: string = '1,1'

  @Expose()
  @IsDefined()
  @IsString()
  public pinScheduleTab: string = '2'

  public periodic: boolean = false
}
