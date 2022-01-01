import { plainToClass } from 'class-transformer'
import { validate } from 'class-validator'
import { apiValidationMessage } from '@/modules/ApiDataValidation/apiValidationMessage'

export const treeApiDataValidation = (responseData, Class) => {
  const result: Array<typeof Class> = []

  responseData.forEach(item => {
    const instance: typeof Class = plainToClass(Class, item, {
      excludeExtraneousValues: true,
    })

    validate(instance).then(errors => {
      if (errors.length) {
        apiValidationMessage(errors, instance)
      }
    })

    if (item.children.length) {
      instance.children = treeApiDataValidation(item.children, Class)
    }

    result.push(instance)
  })

  return result
}
