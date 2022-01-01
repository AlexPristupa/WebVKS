/**
 * @description store Пользовательские настройки интерфейса
 */
// import CONSTANTS from '@/constants'
import { Module } from 'vuex'
import { getters } from './getters'
import { actions } from './actions'
import { mutations } from './mutations'
import { IUserSettings } from './types'
import { IRootState } from '@/store/types'

export const state: IUserSettings = {
  twoHorizontalPanelsLayoutList: {
    /**
     * Пока что не используется, закомментировала, чтобы иметь пример
     */
    // [CONSTANTS.layoutName.inventory.managingPhoneNumbersCard]: {
    //   sizes: {
    //     top: 0,
    //     bottom: 0,
    //   },
    //   isOpen: {
    //     top: true,
    //     bottom: false,
    //   },
    // },
  },
  openedStructure: {},
}

const namespaced = true

export const userSettings: Module<IUserSettings, IRootState> = {
  namespaced,
  state,
  getters,
  actions,
  mutations,
}
