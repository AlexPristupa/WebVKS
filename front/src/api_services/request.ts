import Qs from 'qs'
import axios, { AxiosRequestConfig } from 'axios'
import { TIMEOUT } from '@/constant'
import { CONFIG } from '@/config'
import { checkApiResponse } from '@/modules/CheckApiResponse/CheckApiResponse'
import { checkApiError } from '@/modules/CheckApiResponse/CheckApiError'
import { GetHeader, requestOptions } from './request.interface'
import { methods } from '@/api_services/httpMethods.enum'
import { Auth } from '@/modules/Auth/Auth'
import { TokenChecker } from '@/modules/TokenChecker/TokenChecker'
/**
 * @description перехватчик запроса к серверу перед отправкой. Проверятет
 *              наличие токена и добавляет его.
 */
axios.interceptors.request.use(
  config => {
    TokenChecker.setUserActivityTimestamp()
    if (Auth.getAccessToken()) {
      config.headers['Authorization'] = `Bearer ${Auth.getAccessToken()}`
    }
    return config
  },
  error => {
    // Обработать возникшую ошибку запроса
    return Promise.reject(error)
  },
)

/**
 * @description перехватчик ответа сервера подключает обработчик ответа
 *              и обработчик ошибок
 */
axios.interceptors.response.use(
  response => {
    if (response.headers['content-type'] !== 'video/mp4') {
      response.data = checkApiResponse(response)
      return response.data
    }
    return response
  },
  error => {
    return checkApiError(error)
  },
)

const getHeaders: GetHeader = (headers, method) => {
  headers = Object.assign(
    {
      'Content-Type': 'application/json; charset=UTF-8',
      Pragma: 'no-cache',
    },
    headers,
  )
  if (method === methods.get) {
    headers.Accept = 'application/json'
  }
  return headers
}

const request: requestOptions = (
  url: string,
  {
    method = methods.post,
    timeout = TIMEOUT,
    cancelToken,
    params = {},
    data = {},
    headers = <any>{},
    dataType = 'json',
  },
) => {
  const baseURL =
    process.env.NODE_ENV === 'development'
      ? CONFIG.domain.dev
      : CONFIG.domain.prod

  headers = getHeaders(headers, method)

  const defaultConfig = {
    baseURL,
    url,
    method,
    params,
    data,
    cancelToken,
    timeout,
    headers,
    responseType: dataType,
  }

  if (method === methods.get) {
    defaultConfig.params = data
    delete defaultConfig.data
  } else if (method !== methods.delete) {
    delete defaultConfig.params

    const contentType = headers['Content-Type']

    if (typeof contentType !== 'undefined') {
      if (contentType.indexOf('multipart') !== -1) {
        // тип `multipart/form-data;`
        defaultConfig.data = data
      } else if (contentType.indexOf('json') !== -1) {
        // тип `application/json`
        //  Ответ сервера raw body(исходные данные) "{name:"jhon",sex:"man"}" (Нормальная строка）
        defaultConfig.data = JSON.stringify(data)
      } else {
        // тип `application/x-www-form-urlencoded`
        // Ответ сервера  raw body(исходные данные) name=homeway&key=nokey
        defaultConfig.data = Qs.stringify(data)
      }
    }
  }

  return axios(defaultConfig as AxiosRequestConfig)
}

export default request
