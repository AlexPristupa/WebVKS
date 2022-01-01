import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { Expose } from 'class-transformer'
import { IsDefined, IsString } from 'class-validator'

export class ValidationErrorEntity implements IEntity {
  toValidationName(): string {
    return 'ValidationErrorEntity'
  }

  @Expose()
  @IsDefined()
  @IsString()
  public exception: string = ''

  @Expose()
  @IsDefined()
  @IsString()
  public message: string = ''
}
