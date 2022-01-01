import { Module } from 'vuex'
import { getters } from './getters'
import { actions } from './actions'
import { mutations } from './mutations'
import { ITableHeaderEffectsState } from './types'
import { IRootState } from '@/store/types'

export const state: ITableHeaderEffectsState = {
  headersEffects: [],
}

const namespaced = true

export const tableHeaderEffects: Module<
  ITableHeaderEffectsState,
  IRootState
> = {
  namespaced,
  state,
  getters,
  actions,
  mutations,
}
