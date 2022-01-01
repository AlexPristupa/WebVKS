import { MutationTree } from 'vuex'
import {
  ILayoutItemPayload,
  IOpenedStructurePayload,
  IUserSettings,
} from './types'

export const mutations: MutationTree<IUserSettings> = {
  SET_LAYOUT_ITEM: (state, sts: ILayoutItemPayload) => {
    state.twoHorizontalPanelsLayoutList[sts.name] = sts.data
  },
  SET_OPENED_STRUCTURE: (state, data: IOpenedStructurePayload) => {
    state.openedStructure[data.tableName] = data
    state.openedStructure = { ...state.openedStructure }
  },
}
