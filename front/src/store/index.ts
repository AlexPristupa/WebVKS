import Vue from 'vue'
import Vuex, { StoreOptions } from 'vuex'
import { IRootState } from './types'
import { tableHeaderEffects } from './modules/table_header_effects_store/index'
import { userSettings } from './modules/user_settings'
import { exportFile } from '@/store/modules/exportFile'
import { user } from './modules/user'

Vue.use(Vuex)

const store: StoreOptions<IRootState> = {
  state: {
    version: '1.0.0',
  },
  modules: {
    user,
    tableHeaderEffects,
    exportFile,
    userSettings,
  },
}

export default new Vuex.Store<IRootState>(store)
