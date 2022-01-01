import { TypeApiResult } from '@/modules/CheckApiResponse/CheckApiResponse.const'
import { AxiosResponse } from 'axios'
import { ApiResponseWarning } from '@/modules/ApiDataValidation/ResponseDto/ApiResponse/ApiResponseWarning'
import { ApiResponseError } from '@/modules/ApiDataValidation/ResponseDto/ApiResponse/ApiResponseError'
import { IApiResponse } from '@/modules/ApiDataValidation/ResponseDto/ApiResponse/IApiResponse'

export type MessageResponse = null | string
export type DataResponse = null | object

export interface IApiResponseOk {
  data: IResultDataApi
  message: null
  result: TypeApiResult.ok
}

export interface IApiResponseWarning {
  data: string | Array<string>
  message: null | string
  result: TypeApiResult.warning
}

export interface IApiResponseError {
  data: string | Array<string>
  message: null | string
  result: TypeApiResult.error
}

export type IDateApiResponse =
  | IApiResponseOk
  | IApiResponseWarning
  | IApiResponseError

// export type IResultApi = boolean | IApiResponse

export type IResultDataApi<T = any> = T

export type CheckApiResponse = (
  response: AxiosResponse,
) => IApiResponse | boolean

export type ResultTypeWarning = (data: ApiResponseWarning) => false
export type ResultTypeError = (data: ApiResponseError) => false

export type CheckRedirect = (response: AxiosResponse<any>) => boolean
