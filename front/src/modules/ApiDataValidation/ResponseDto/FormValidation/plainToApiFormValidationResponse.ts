import { doubleNestingApiDataValidation } from '@/modules/ApiDataValidation/doubleNesting.ApiDataValidation'
import { ApiFormValidationResponse } from '@/modules/ApiDataValidation/ResponseDto/FormValidation/ApiFormValidationResponse'
import { FormValidationError } from '@/modules/ApiDataValidation/ResponseDto/FormValidation/FormValidation.entity'

export function plainToApiFormValidationResponse(data) {
  return doubleNestingApiDataValidation(data.data, ApiFormValidationResponse, [
    {
      fieldName: 'validation',
      ClassName: FormValidationError,
    },
  ])
}
