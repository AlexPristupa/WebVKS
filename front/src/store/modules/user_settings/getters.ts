import { IDataToOpen, IUserSettings } from './types'
import { GetterTree } from 'vuex'
import { IRootState } from '@/store/types'
import { TableName } from '@/modules/table_grid/TableName.const'

export const getters: GetterTree<IUserSettings, IRootState> = {
  layoutItemData: state => (name: string) =>
    state.twoHorizontalPanelsLayoutList[name],
  getOpenedStructure: state => (
    tableName: TableName,
    key: string = 'all',
  ): Array<number> | null => {
    const openedToTable: IDataToOpen = state.openedStructure[tableName]
    if (openedToTable && openedToTable.dataToOpen[key]) {
      return openedToTable.dataToOpen[key]
    }
    return null
  },
}
