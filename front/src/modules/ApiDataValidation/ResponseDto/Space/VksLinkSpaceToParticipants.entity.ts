import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { IsBoolean, IsDefined, IsInt, IsString } from 'class-validator'
import { Expose } from 'class-transformer'
import { TranslateResult } from 'vue-i18n'

export class VksLinkSpaceToParticipantsEntity implements IEntity {
  toValidationName(): string {
    return 'VksLinkSpaceToParticipantsEntity'
  }

  constructor() {
    return this
  }

  @Expose()
  @IsDefined()
  @IsInt()
  public vksUserId: number | null = null

  @IsString()
  public vksUserName: string | TranslateResult = ''

  @Expose()
  @IsDefined()
  @IsString()
  public callLegProfileGuid: string = ''

  @Expose()
  @IsDefined()
  @IsBoolean()
  public canDestroy: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public canAddRemoveMember: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public canChangeName: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public canChangeNonMemberAccessAllowed: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public canChangeUri: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public canChangeCallId: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public canChangePassCode: boolean = false

  @Expose()
  @IsDefined()
  @IsBoolean()
  public canRemoveSelf: boolean = false

  @IsDefined()
  @IsBoolean()
  public found: boolean = false
}
