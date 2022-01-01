import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { Expose, Type } from 'class-transformer'
import { IsDefined, IsString } from 'class-validator'
import { ValidationErrorEntity } from '@/modules/ApiDataValidation/ResponseDto/FormValidation/FormValidationError.entity'

export class FormValidationError implements IEntity {
  toValidationName(): string {
    return 'FormValidationError'
  }

  @Expose()
  @IsDefined()
  @IsString()
  public field: string = ''

  @Expose()
  @IsDefined()
  @Type(() => ValidationErrorEntity)
  public errors: Array<ValidationErrorEntity> = []
}
