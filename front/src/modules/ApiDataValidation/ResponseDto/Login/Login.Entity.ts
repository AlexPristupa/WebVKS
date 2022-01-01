import { ApiResponseData } from '@/modules/ApiDataValidation/ResponseDto/ApiResponseData'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { Expose } from 'class-transformer'
import { IsDefined, IsString, MinLength } from 'class-validator'

export class LoginEntity extends ApiResponseData implements IEntity {
  toValidationName(): string {
    return 'LoginEntity'
  }

  @Expose()
  @IsDefined()
  @IsString()
  @MinLength(291)
  public accessToken: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public userName: string = ''
}
