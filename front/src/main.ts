import '@babel/polyfill'
import Vue from 'vue'
import 'reflect-metadata'
import App from './App.vue'
import router from './router'
import store from './store'
// @ts-ignore
import VueVideoPlayer from 'vue-video-player'

import '@ag-grid-community/client-side-row-model'
import '@ag-grid-community/infinite-row-model'
import '@ag-grid-community/csv-export'

import 'ag-grid-community/dist/styles/ag-theme-balham.css'
import 'ag-grid-community/dist/styles/ag-grid.css'
import './directive'

import VueI18n from 'vue-i18n'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'

import '@/styles/main.scss'

import i18n from '@/i18n'
import vueHeadful from 'vue-headful'
import mes from '@/modules/Messages/Messages.plugin'
// import { setRoles } from '@/modules/system'

Vue.component('vue-headful', vueHeadful)
import 'vue-progress-path/dist/vue-progress-path.css'
import VueProgress from 'vue-progress-path' // Подключение loading-progress
Vue.use(VueProgress, {})
Vue.use(VueVideoPlayer)

const VueInputMask = require('vue-inputmask').default

Vue.use(VueInputMask)
Vue.use(mes)
Vue.use(VueI18n)
Vue.use(ElementUI, {
  size: 'small',
  zIndex: 3000,
  i18n: (key: string, value: string) => i18n.t(key, value),
})

// setRoles()
// Прочитать настройки портала
// const { getCoockiePortalOptions } = require('@/modules/system')
// getCoockiePortalOptions()

new Vue({
  router,
  store,
  i18n,
  render: h => h(App),
}).$mount('#app')
