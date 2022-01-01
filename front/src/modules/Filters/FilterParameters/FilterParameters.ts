import { ISeparator } from '@/modules/Separators/Separators.interface'
import api from '@/api_services'
import {
  IDtoCheckValue,
  IResponseCheckValue,
} from '@/modules/Filters/FilterParameters/FilterParameters.interface'
import { methods } from '@/api_services/httpMethods.enum'
import { separatorList } from '@/modules/Separators/Separators.const'

export class FilterParameters {
  private _dataString = ''
  private _dataArray: Array<string> = []
  private _separator: ISeparator = separatorList[0]
  private _serverResult: IResponseCheckValue = {
    notFound: [],
    found: [],
  }

  constructor(
    private _tableName: string = '',
    private _columnName: string = '',
  ) {
    return this
  }

  get dataArray(): Array<string> {
    return this._dataArray
  }

  public checkData(data: string, separator: ISeparator) {
    if (data) {
      this._dataString = data
    } else {
      new Error('data not valid')
    }
    this._separator = separator
    this._parse()
    return this
  }

  private _parse() {
    const dataSet = new Set<string>()
    this._dataString.split(this._separator.value).forEach(item => {
      item.trim()
      if (item) {
        dataSet.add(item)
      }
    })
    this._dataArray = [...dataSet]
  }

  public async checkOnServer(): Promise<IResponseCheckValue> {
    const dto: IDtoCheckValue = {
      columnName: this._columnName,
      tableName: this._tableName,
      checkList: this._dataArray,
    }
    const res: IResponseCheckValue = await api.fetchFoundColumnValue({
      method: methods.post,
      data: dto,
    })
    if (res) {
      this._serverResult = res
    }
    return this._serverResult
  }
}
