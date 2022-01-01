import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { Expose } from 'class-transformer'
import { IsDefined, IsNumber, IsString } from 'class-validator'

export class SpacesCallBrandingProfilesProxyEntity implements IEntity {
  toValidationName(): string {
    return 'SpacesCallBrandingProfilesProxyEntity'
  }

  @Expose()
  @IsDefined()
  @IsString()
  public callBrandingProfileId: string = ''

  @Expose()
  @IsDefined()
  @IsNumber()
  public serversgroupsid: number = 1
}
