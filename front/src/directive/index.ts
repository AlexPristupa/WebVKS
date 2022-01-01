/**
 * @description Директива применения прав роли
 */

import Vue, { DirectiveOptions } from 'vue'

import permission from './permission'

const directives = {
  permission,
}

Object.keys(directives).forEach(key => {
  Vue.directive(key, (directives as { [key: string]: DirectiveOptions })[key])
})
