import { MutationTree } from 'vuex'
import { ITableHeaderEffectsState } from './types'

export const mutations: MutationTree<ITableHeaderEffectsState> = {
  SET_ACTIVE_EFFECTS(state, data) {
    const currentTable = state.headersEffects.find(
      effect => effect.tableName === data.tableName,
    )
    if (currentTable) {
      Object.keys(data.effects).forEach(effect => {
        currentTable.effects[effect] = data.effects[effect]
      })
    } else {
      state.headersEffects.push(data)
    }
    // для поддержки реактивности
    state.headersEffects = [...state.headersEffects]
  },
  CLEAR_EFFECTS(state, tableName) {
    state.headersEffects = state.headersEffects.filter(
      effect => effect.tableName !== tableName,
    )
  },
}
