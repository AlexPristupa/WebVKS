import api from '@/api_services'
import { IFiltrationPanelItem } from '@/modules/FiltrationPanel/FiltrationPanel.interface'
import { methods } from '@/api_services/httpMethods.enum'

export class FiltrationPanel {
  private _list: Array<IFiltrationPanelItem> = []

  constructor(private _tableName: string) {
    return this
  }

  public async getList() {
    this._list = await api.filtrationPanelList({
      method: methods.get,
      data: { tableName: this._tableName },
    })
    return this._list
  }
}
