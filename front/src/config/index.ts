import { API_ENTITIES } from './api'
import { TABLE } from './table'

export const CONFIG = {
  locale: 'ru',
  domain: {
    dev: 'http://192.168.80.252:200',
    //dev: 'http://localhost:5000',
    prod: '',
  },
  api: API_ENTITIES,
  table: TABLE,
}
