/**
 * @description модуль установки значение по умолчанию.
 * Работает совместно с любым диалоговым окном.
 */
import api from '@/api_services'
import { methods } from '@/api_services/httpMethods.enum'
import { IDefaultValue } from '@/modules/DefaultValue/DefaultValue.interface'

export class DefaultValue {
  private readonly _field: string | undefined
  private readonly _valuesFromBackPromise: Promise<Array<IDefaultValue>>

  constructor(tableName, field) {
    this._field = field
    this._valuesFromBackPromise = DefaultValue.getDefaultValue(tableName, field)
    return this
  }

  get value() {
    return this._valuesFromBackPromise.then(res => {
      if (this._field) {
        return res.find(value => value.formField === this._field)?.value
      }
      return res
    })
  }

  private static async getDefaultValue(
    tableName,
    field,
  ): Promise<Array<IDefaultValue>> {
    const res = await api.invGetDefaultValue({
      method: methods.get,
      data: {
        tableName: tableName,
        formField: field,
      },
    })
    return res ? res.defaultValue : []
  }
}
