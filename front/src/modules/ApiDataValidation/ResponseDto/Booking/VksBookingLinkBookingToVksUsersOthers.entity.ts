import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsOptional, IsString } from 'class-validator'
import { Expose, Transform } from 'class-transformer'
import i18n from '@/i18n'

export class VksBookingLinkBookingToVksUsersOthers implements IEntity {
  toValidationName(): string {
    return 'VksBookingLinkBookingToVksUsersOthers'
  }

  constructor(data?) {
    if (data) {
      this.id = data.id || new Date().getTime()
      this.name = data.name || i18n.t('forms.editBookingListParticipant.noName')
      this.uri = data.uri || ''
      this.email = data.email || ''
      this.isFromOtherList = true
    }
  }

  @Expose({ name: 'vksUsersOtherId' })
  @Transform(({ value }) => {
    return value + 'other'
  })
  public id: number | string = 0

  @Expose({ name: 'vksUserOtherName' })
  public name: string = ''

  @Expose()
  @IsOptional()
  @IsString()
  public uri: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public email: string = ''

  public checked: boolean = false

  public isFromOtherList: boolean = true
}
