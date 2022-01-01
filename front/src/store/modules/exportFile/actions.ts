import { ActionTree } from 'vuex'
import { IExportState } from '@/store/modules/exportFile/types'
import { IRootState } from '@/store/types'

export const actions: ActionTree<IExportState, IRootState> = {
  setCancelTokenSource({ commit }, tokenData) {
    commit('SET_CANCEL_TOKEN', tokenData)
  },
  deleteCancelTokenSource({ commit }, tableName) {
    commit('DELETE_CANCEL_TOKEN', tableName)
  },
  setHidden({ commit }, hiddenData) {
    commit('SET_HIDDEN', hiddenData)
  },
}
