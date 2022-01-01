import { GetterTree } from 'vuex'
import { IExportState } from '@/store/modules/exportFile/types'
import { IRootState } from '@/store/types'
import { TableName } from '@/modules/table_grid/TableName.const'

export const getters: GetterTree<IExportState, IRootState> = {
  getHidden: state => (tableName: TableName) => {
    return state.hidden[tableName] || false
  },
  getCancelTokenSource: state => (tableName: TableName) => {
    return state.cancelTokenSources[tableName] || { cancel: null, token: null }
  },
}
