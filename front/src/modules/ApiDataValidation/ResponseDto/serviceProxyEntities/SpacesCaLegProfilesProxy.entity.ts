import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { Expose } from 'class-transformer'
import { IsDefined, IsNumber, IsOptional, IsString } from 'class-validator'

export class SpacesCaLegProfilesProxyEntity implements IEntity {
  toValidationName(): string {
    return 'SpacesCaLegProfilesProxyEntity'
  }

  @Expose()
  @IsDefined()
  @IsString()
  public callLegProfilesId: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public callLegProfilesName: string = ''

  @Expose()
  @IsOptional()
  @IsNumber()
  public serversgroupsid: number | null = null
}
