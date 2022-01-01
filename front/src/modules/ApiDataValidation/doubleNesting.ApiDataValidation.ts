import { plainToClass } from 'class-transformer'
import { validate } from 'class-validator'
import { apiValidationMessage } from '@/modules/ApiDataValidation/apiValidationMessage'
import { IEntity } from '@/modules/ApiDataValidation/ResponseDto/IEntity.interface.ts'

export const doubleNestingApiDataValidation = (
  responseData,
  oneLevelClass,
  twoLevelClassList: Array<{ fieldName: string; ClassName: any }>,
): typeof oneLevelClass => {
  // Приведение к классу сущности первого уровня вложенности
  const result: typeof oneLevelClass = plainToClass<IEntity, any>(
    oneLevelClass,
    responseData,
    {
      excludeExtraneousValues: true,
    },
  )

  // Итерация по коллекции полей и сущностей второго уровня вложенности
  twoLevelClassList.forEach(field => {
    result[field.fieldName] = responseData[field.fieldName].map(
      (item: object) => {
        // Приведение к классу сущностей второго уровня вложенности
        const entity = plainToClass<IEntity, object>(field.ClassName, item, {
          excludeExtraneousValues: true,
        })

        // Валидации данных второго уровня вложенности
        validate(entity).then(errors => {
          if (errors.length) {
            apiValidationMessage(errors, entity)
          }
        })
        return entity
      },
    )
  })

  // Валидации данных первого уровня вложенности
  validate(result).then(errors => {
    if (errors.length) {
      apiValidationMessage(errors, result)
    }
  })

  return result
}
