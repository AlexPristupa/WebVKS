import { ActionTree } from 'vuex'
import {
  ILayoutItemPayload,
  IOpenedStructurePayload,
  IUserSettings,
} from './types'
import { IRootState } from '@/store/types'

export const actions: ActionTree<IUserSettings, IRootState> = {
  /**
   * @description Сохранить состояние лэйаута
   * @param param0 Мутация
   * @param {Object} sts объект состояния лэйаута
   */
  setLayoutItem({ commit }, sts: ILayoutItemPayload) {
    commit('SET_LAYOUT_ITEM', sts)
  },
  setOpenedStructure({ commit }, opened: IOpenedStructurePayload) {
    commit('SET_OPENED_STRUCTURE', opened)
  },
}
