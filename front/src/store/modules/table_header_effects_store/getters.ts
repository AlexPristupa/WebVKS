import { ITableHeaderEffectsState } from './types'
import { GetterTree } from 'vuex'
import { IRootState } from '@/store/types'

export const getters: GetterTree<ITableHeaderEffectsState, IRootState> = {
  getTableHeadersEffects: state => (tableName: string) => {
    if (state.headersEffects.length) {
      const tableHeadersEffects = state.headersEffects.find(
        item => item.tableName === tableName,
      )
      return tableHeadersEffects ? tableHeadersEffects.effects : undefined
    }
  },
}
