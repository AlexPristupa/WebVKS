import { URLs } from '@/api_services/Urls.const'
import { requestOptions } from '@/api_services/request.interface'
import { AxiosResponse } from 'axios'

export type apiService<T> = {
  [key in keyof typeof URLs]: (
    requestOptions: requestOptions,
  ) => AxiosResponse<T>
}
