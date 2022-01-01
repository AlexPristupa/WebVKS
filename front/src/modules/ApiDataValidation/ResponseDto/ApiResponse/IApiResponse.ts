import { TypeApiResult } from '@/modules/CheckApiResponse/CheckApiResponse.const'
import { ApiResponseData } from '@/modules/ApiDataValidation/ResponseDto/ApiResponseData'

export interface IApiResponse {
  data: Array<ApiResponseData | string> | string | null
  message: null | string
  result: TypeApiResult
}
