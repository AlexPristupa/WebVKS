import { ActionTree } from 'vuex'
import { ITableHeaderEffectsState } from './types'
import { IRootState } from '@/store/types'

export const actions: ActionTree<ITableHeaderEffectsState, IRootState> = {
  changeColumnActiveEffects({ commit }, data) {
    commit('SET_ACTIVE_EFFECTS', data)
  },
  clearTableHeaderEffects({ commit }, tableName) {
    commit('CLEAR_EFFECTS', tableName)
  },
}
