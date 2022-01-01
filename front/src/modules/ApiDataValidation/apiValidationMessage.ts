import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'
import { ValidationError } from 'class-validator/types/validation/ValidationError'
import { VALIDATION_STYLE } from '@/modules/Validation/VALIDATION_STYLE.const'

/**
 * @description Функция формирует и выводит сообщения в случае провала валидации
 * @param errors Array<errors> коллекция ошибок в формате 'class-validator'
 * @param entity <instance class> инстанс класса валидируемой сущности
 */
export const apiValidationMessage = (
  errors: ValidationError[],
  entity: IEntity,
) => {
  const style = [VALIDATION_STYLE]
  console.log(
    `%c[ApiValidation] ${entity.toValidationName()}::`,
    style[0],
    errors,
  )
}
