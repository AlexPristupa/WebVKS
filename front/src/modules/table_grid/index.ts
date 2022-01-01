import api from '@/api_services'
import { methods } from '@/api_services/httpMethods.enum'
import CONSTANTS from '@/constants'
import debounce from 'lodash/debounce'

/**
 * @description Установить ширину колонки
 * @param {String} tableName
 * @param {String} val
 */
export const resized = debounce(
  async (tableName: string, val: { [key: string]: number }) => {
    if (!val) return

    await api.postSettingWidthColumnsAgGridVue({
      method: methods.post,
      data: { [tableName]: val },
    })
  },
  CONSTANTS.debounce.timeOut.slow,
)

/**
 * @description Установить видимость колонки
 * @param {String} tableName
 * @param {String} val
 */
export async function visible(
  val: { cols: Array<{ [key: string]: any }> },
  tableName: string,
) {
  if (!val.cols) return

  const colsExport: any[] = []

  val.cols.forEach(col => {
    if (col.visible) colsExport.push(col.colDef.field)
  })

  await api.postSettingVisibleColumnsAgGridVue({
    method: methods.post,
    data: {
      [tableName]: colsExport,
    },
  })
}

/**
 * @description Установить перетаскивание колонки
 * @param {String} tableName
 * @param {String} val
 */
export async function draged<
  T extends { [key: string]: any },
  K extends string
>(val: T, tableName: K) {
  if (!val.cols) return
  const colsExport: string[] = []
  val.cols.forEach(col => {
    if (col.visible) colsExport.push(col.colDef.field)
  })

  await api.postSettingVisibleColumnsAgGridVue({
    method: methods.post,
    data: {
      [tableName]: colsExport,
    },
  })
}
