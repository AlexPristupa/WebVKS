import { FormValidationError } from '@/modules/ApiDataValidation/ResponseDto/FormValidation/FormValidation.entity'

export class FormValidation {
  constructor() {}
  static backValidationField(
    array: Array<FormValidationError>,
    rule: any,
    value: any,
    callback: any,
  ) {
    const validated = array?.find(item => item.field === rule.field)
    const message = validated ? validated.errors[0].message : ''
    if (message) {
      console.warn(message)
      callback(new Error(message))
    }
    callback()
  }

  static backValidationWithoutField(array: Array<FormValidationError>): string {
    const validated = array?.find(item => !item.field)
    const message = validated ? validated.errors[0].message : ''
    if (message) {
      console.warn(message)
    }
    return message
  }
}
