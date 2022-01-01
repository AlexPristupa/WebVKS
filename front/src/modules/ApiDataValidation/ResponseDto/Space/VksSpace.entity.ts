import { ITableRowData } from '@/modules/table_grid/ITableRowData.interface'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsBoolean, IsDefined, IsInt, IsString } from 'class-validator'
import { Expose, Type } from 'class-transformer'
import { VksLinkSpaceToParticipantsEntity } from '@/modules/ApiDataValidation/ResponseDto/Space/VksLinkSpaceToParticipants.entity'

export class VksSpaceEntity implements ITableRowData, IEntity {
  toValidationName(): string {
    return 'VksSpaceEntity'
  }

  constructor(data?) {
    if (data) {
      this.serversGroupsId = data.serversGroupsId || 1
      this.id = data.id || 0
      this.name = data.name || ''
      this.uri = data.uri || ''
      this.tagCdr = data.tagCdr || ''
      this.ownerId = data.ownerId || null
      this.callLegProfileGuid = data.callLegProfileGuid || ''
      this.callBrandingProfileGuid = data.callBrandingProfileGuid || ''
      this.password = data.password || ''
      this.uriAlt = data.uriAlt || ''
      this.passwordGuest = data.passwordGuest || ''
      this.uriVideo = data.uriVideo || ''
      this.isGuestAccessible = data.isGuestAccessible || false
      this.isAvailableForBooking = data.isAvailableForBooking || false
      this.callId = data.callId || ''
      this.linkSpaceToParticipants = data.linkSpaceToParticipants || []

      this.guid = data.guid || ''
      this.spaceGroupsId = data.spaceGroupsId || null

      this.guestPasswordGeneration = data.guestPasswordGeneration || false
      this.callIdGeneration = false
    }
  }

  @Expose()
  @IsDefined()
  @IsInt()
  public id: number = 0

  @Expose()
  @IsDefined()
  @IsInt()
  public serversGroupsId: number | null = null

  @Expose()
  @IsDefined()
  @IsInt()
  public ownerId: number | null = null

  @Expose()
  @IsDefined()
  @IsString()
  public name: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public uri: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public tagCdr: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public guid: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public callLegProfileGuid: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public callBrandingProfileGuid: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public password: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public uriAlt: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public passwordGuest: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public uriVideo: string = ''

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isGuestAccessible: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public isAvailableForBooking: boolean = false

  @Expose()
  public spaceGroupsId: number | null = null

  @Expose()
  @IsDefined()
  @IsString()
  public callId: string = ''

  @Expose()
  public callIdGeneration: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public guestPasswordGeneration: boolean = true

  @Expose()
  @Type(() => VksLinkSpaceToParticipantsEntity)
  public linkSpaceToParticipants: Array<VksLinkSpaceToParticipantsEntity> = []
}
