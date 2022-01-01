import { MutationTree } from 'vuex'
import {
  IExportState,
  IHiddenData,
  ITokenData,
} from '@/store/modules/exportFile/types'
import { TableName } from '@/modules/table_grid/TableName.const'

export const mutations: MutationTree<IExportState> = {
  SET_HIDDEN(state, hiddenData: IHiddenData) {
    state.hidden[hiddenData.tableName] = hiddenData.isHidden
    state.hidden = { ...state.hidden }
  },
  SET_CANCEL_TOKEN(state, tokenData: ITokenData) {
    state.cancelTokenSources[tokenData.tableName] = tokenData.token
    state.cancelTokenSources = { ...state.cancelTokenSources }
  },
  DELETE_CANCEL_TOKEN(state, tokenData: ITokenData) {
    delete state.cancelTokenSources[tokenData.tableName]
    state.cancelTokenSources = { ...state.cancelTokenSources }
  },
}
