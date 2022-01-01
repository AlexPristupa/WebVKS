import Vue from 'vue'
import SvgIcon from '@/components/general/svg_icon'

// Регистрируем глобально
Vue.component('SvgIcon', SvgIcon)

// Загружаем все спрайты в контекст
const req = require.context('./svg', false, /\.svg$/)
const requireAll = requireContext => requireContext.keys().map(requireContext)
requireAll(req)
