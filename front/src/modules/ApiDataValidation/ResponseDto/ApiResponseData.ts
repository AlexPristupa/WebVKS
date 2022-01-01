import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'

/**
 * @description Базовый класс данных ответа api
 */
export class ApiResponseData implements IEntity {
  toValidationName(): string {
    return 'ApiResponseData'
  }
}
