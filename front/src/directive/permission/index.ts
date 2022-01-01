/**
 * @description Модуль применения прав
 */

import store from '@/store'

export default {
  inserted(
    el: { parentNode: { removeChild: (arg0: any) => any } },
    binding: { value: any },
  ) {
    const { value } = binding
    const roles = store.getters && store.getters['user/roles']

    if (value && value instanceof Array && value.length > 0) {
      const permissionRoles = value

      const hasPermission = roles.some((role: string) => {
        return permissionRoles.includes(role)
      })

      if (!hasPermission) {
        el.parentNode && el.parentNode.removeChild(el)
      }
    } else {
      throw new Error(
        `необходимы роли! Используйте пожалуйста директиву v-permission="['admin','editor']"`,
      )
    }
  },
}
