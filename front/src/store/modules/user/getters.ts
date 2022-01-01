import { IUser } from './types'
import { GetterTree } from 'vuex'
import { IRootState } from '@/store/types'

export const getters: GetterTree<IUser, IRootState> = {
  accessToken: state => state.accessToken,
  refreshToken: state => state.refreshToken,
  roles: state => state.roles,
  checkRole: (state, role) => state.roles.includes(role),
  userName: state => state.userName,
  userFullName: state => state.userFullName,
}
