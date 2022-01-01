import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsDefined, IsOptional, IsString } from 'class-validator'
import { Expose } from 'class-transformer'

export class VksBookingLinkToParticipant implements IEntity {
  toValidationName(): string {
    return 'VksBookingLinkToParticipant'
  }

  constructor(data?) {
    if (data) {
      this.id = data.id || 0
      this.name = data.name || ''
      this.uri = data.uri || ''
      this.email = data.email || ''
      this.callLegProfileGuid = data.callLegProfileGuid || ''
      this.isFromOtherList = false
    }
  }

  @Expose({ name: 'vksParticipantId' })
  public id: number = 0

  @Expose({ name: 'vksUserName' })
  public name: string = ''

  @Expose()
  @IsOptional()
  @IsString()
  public uri: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public email: string = ''

  @Expose()
  @IsOptional()
  @IsString()
  public callLegProfileGuid: string = ''

  public checked: boolean = false
  public isFromOtherList: boolean = false
}
