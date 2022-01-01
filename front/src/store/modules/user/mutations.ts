import { MutationTree } from 'vuex'
import { IUser } from './types'

export const mutations: MutationTree<IUser> = {
  SET_TOKEN: (state, accessToken) => {
    state.accessToken = accessToken
  },
  SET_REFRESH_TOKEN: (state, refreshToken) => {
    state.refreshToken = refreshToken
  },
  SET_ROLES: (state, roles) => {
    state.roles = [...roles]
  },
  SET_USER_NAME: (state, sts) => {
    state.userName = sts
  },
  SET_USER_FULLNAME: (state, sts) => {
    state.userFullName = sts
  },
}
