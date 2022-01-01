import { plainToClass } from 'class-transformer'
import { validate } from 'class-validator'
import { apiValidationMessage } from '@/modules/ApiDataValidation/apiValidationMessage'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface'

export const singleNestingApiDataValidation = (responseData, Class) => {
  // Приведение к классу
  const result: typeof Class = plainToClass<IEntity, string>(
    Class,
    responseData,
    {
      excludeExtraneousValues: true,
    },
  )

  // Валидация данных
  validate(result).then(errors => {
    if (errors.length) {
      apiValidationMessage(errors, result)
    }
  })

  return result
}
