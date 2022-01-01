import Vue from 'vue'
import request from './request'
import { IRequestConfig } from '@/api_services/request.interface'
import { TIMEOUT } from '@/constant'
import { methods } from '@/api_services/httpMethods.enum'
import { URLs } from '@/api_services/Urls.const'

const FUNS: any = {}

Object.keys(URLs).forEach(key => {
  FUNS[key] = (
    options: IRequestConfig = {
      method: methods.post,
      timeout: TIMEOUT,
      params: {},
      data: {},
      headers: <any>{},
      dataType: 'json',
    },
  ) => {
    return request(URLs[key], options)
  }
})

// services прикрепляем к прототипу vue
// Вызывать, как ссылочный метод：this.$api
Object.defineProperty(Vue.prototype, '$api', { value: FUNS })

export default FUNS
