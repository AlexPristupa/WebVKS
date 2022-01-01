import { IExportState } from '@/store/modules/exportFile/types'
import { Module } from 'vuex'
import { IRootState } from '@/store/types'
import { getters } from './getters'
import { mutations } from './mutations'
import { actions } from './actions'

export const state: IExportState = {
  cancelTokenSources: {},
  hidden: {},
}
const namespaced = true

export const exportFile: Module<IExportState, IRootState> = {
  namespaced,
  state,
  getters,
  mutations,
  actions,
}
