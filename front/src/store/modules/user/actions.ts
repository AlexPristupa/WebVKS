import { ActionTree } from 'vuex'
import { IUser } from './types'
import { IRootState } from '@/store/types'

export const actions: ActionTree<IUser, IRootState> = {
  /**
   * @description динамическое изменение разрешений
   */
  changeRoles({ commit }, role) {
    commit('SET_ROLES', role)
  },

  /**
   * @description Сохранить имя пользователя
   */
  setUserName({ commit }, userName) {
    commit('SET_USER_NAME', userName)
  },

  /**
   * @description Сохранить имя пользователя
   */
  setUserFullName({ commit }, userFullName) {
    commit('SET_USER_FULLNAME', userFullName)
  },

  /**
   * @description Сохранить токен доступа
   */
  setAccessToken({ commit }, accessToken) {
    commit('SET_TOKEN', accessToken)
  },

  /**
   * @description Сохранить refresh-токен доступа
   */
  setRefreshToken({ commit }, refreshToken) {
    commit('SET_REFRESH_TOKEN', refreshToken)
  },
}
