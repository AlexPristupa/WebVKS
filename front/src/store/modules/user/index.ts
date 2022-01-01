/**
 * @description store Аутентификации пользователя и применение прав роли
 */
import { Module } from 'vuex'
import { getters } from './getters'
import { actions } from './actions'
import { mutations } from './mutations'
import { IUser } from './types'
import { IRootState } from '@/store/types'
import { User } from '@/modules/User/User'
import { Auth } from '@/modules/Auth/Auth'

export const state: IUser = {
  accessToken: Auth.getAccessToken(),
  refreshToken: Auth.getRefreshToken(),
  roles: [],
  userName: User.getUserName(),
  userFullName: User.getUserFullName(),
}

const namespaced = true

export const user: Module<IUser, IRootState> = {
  namespaced,
  state,
  getters,
  actions,
  mutations,
}
