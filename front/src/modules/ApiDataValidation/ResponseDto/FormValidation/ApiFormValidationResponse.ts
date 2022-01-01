import { Expose, Type } from 'class-transformer'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface.ts'
import { FormValidationError } from '@/modules/ApiDataValidation/ResponseDto/FormValidation/FormValidation.entity'

export class ApiFormValidationResponse implements IEntity {
  toValidationName(): string {
    return 'ApiFormValidationResponse'
  }

  @Expose()
  @Type(() => FormValidationError)
  public validation: Array<FormValidationError> = []
}
