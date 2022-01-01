import { ResponseType, AxiosPromise, CancelTokenSource } from 'axios'
import { methods } from '@/api_services/httpMethods.enum'
import { URLs } from '@/api_services/Urls.const'

export interface IRequestConfig {
  method: methods
  timeout: number
  params: any
  data: any
  headers: any
  cancelToken?: CancelTokenSource['token']
  dataType: ResponseType
}

export type IHeaders = {
  Accept?: 'application/json'
  'Content-Type'?: 'application/json; charset=UTF-8'
  Pragma?: 'no-cache'
}

export type requestOptions = (url: URLs, config: IRequestConfig) => AxiosPromise

export type GetHeader = (headers: IHeaders, method: methods) => IHeaders
