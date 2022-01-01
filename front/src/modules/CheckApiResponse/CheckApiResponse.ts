import {
  CheckApiResponse,
  CheckRedirect,
  ResultTypeError,
  ResultTypeWarning,
} from './CheckApiResponse.interface'
import { TypeApiResult } from './CheckApiResponse.const'
import {
  errorMessage,
  warningMessage,
} from '@/modules/Messages/Messages.plugin'
import i18n from '@/i18n'
import { message } from './CheckApiResponse.lang'
import { apiDataValidation } from '@/modules/ApiDataValidation/apiDataValidation'
import { singleNestingApiDataValidation } from '@/modules/ApiDataValidation/singleNesting.ApiDataValidation'
import { ApiResponseOk } from '@/modules/ApiDataValidation/ResponseDto/ApiResponse/ApiResponseOk'
import { ApiResponseWarning } from '@/modules/ApiDataValidation/ResponseDto/ApiResponse/ApiResponseWarning'
import { ApiResponseError } from '@/modules/ApiDataValidation/ResponseDto/ApiResponse/ApiResponseError'
import { methods } from '@/api_services/httpMethods.enum'
import { plainToApiFormValidationResponse } from '@/modules/ApiDataValidation/ResponseDto/FormValidation/plainToApiFormValidationResponse.ts'

i18n.mergeLocaleMessage('en', message.en)
i18n.mergeLocaleMessage('ru', message.ru)

const resultTypeWarning: ResultTypeWarning = data => {
  warningMessage(
    data.message
      ? data.message
      : String(i18n.t('module.checkApiResponse.warning')),
    data.data,
  )
  return false
}

const resultTypeError: ResultTypeError = data => {
  errorMessage(
    data.message
      ? data.message
      : String(i18n.t('module.checkApiResponse.error')),
    data.data,
  )
  return false
}

const checkRedirect: CheckRedirect = response => {
  if (response.request.responseURL.includes('/error/notfound')) {
    errorMessage(String(i18n.t('module.checkApiResponse.warning')), [
      String(
        i18n.t('module.checkApiResponse.urlNotFound', [response.config.url]),
      ),
      String(i18n.t('module.checkApiResponse.checkUrl')),
    ])
  }

  if (
    response.request.responseURL !==
      `${response.config.baseURL}${response.config.url}` &&
    !response.request.responseURL.includes('/error/notfound')
  ) {
    warningMessage(
      String(i18n.t('module.checkApiResponse.warning')),
      String(i18n.t('module.checkApiResponse.redirect')),
    )
  }

  return (
    response.request.responseURL ===
    `${response.config.baseURL}${response.config.url}`
  )
}

const plainToApiResponse = (response, Class) => {
  const apiResponse = singleNestingApiDataValidation(response, Class)
  apiResponse.data = response.data
  return apiResponse
}

export const checkApiResponse: CheckApiResponse = response => {
  if (response.data) {
    switch (response.data.result) {
      case TypeApiResult.ok:
        return apiDataValidation(
          plainToApiResponse(response.data, ApiResponseOk),
          response.config.url,
          response.config.method as methods,
        )
      case TypeApiResult.warning:
        if (response.data?.data && response.data.data.validation) {
          return plainToApiFormValidationResponse(response.data)
        }
        return resultTypeWarning(
          plainToApiResponse(response.data, ApiResponseWarning),
        )
      case TypeApiResult.error:
        return resultTypeError(
          plainToApiResponse(response.data, ApiResponseError),
        )
    }
  }
  checkRedirect(response)
  return false
}
