<template>
  <nav class="aside-menu">
    <div class="logo" />
    <ul class="aside-menu__list">
      <template v-for="menuItem in asideMenuList">
        <li
          :key="menuItem.id"
          class="aside-menu__item"
          :class="{ 'aside-menu__item--active': menuItem.page === activeTab }"
          v-if="showMenuItem(menuItem)"
        >
          <div class="aside-menu__item-link" @click="chooseMenuItem(menuItem)">
            <div
              class="aside-menu__item-icon"
              :class="menuItem.iconCssClass"
            ></div>
            <div class="aside-menu__item-title">
              {{ $t(menuItem.title) }}
            </div>
          </div>
        </li>
      </template>
    </ul>
  </nav>
</template>

<script>
import Vue from 'vue'
import { ASIDE_MENU_APP, ASIDE_MENU_SETTING } from './CONST_ASIDE_MENU'
import { asideMenuLang } from './asideMenu.lang'
import { Permissions } from '@/modules/Permission/Permissions'
import { PermissionsType } from '@/modules/Permission/permissionsType.enum'

export default Vue.extend({
  name: 'AsideMenu',
  props: {
    layoutType: {
      type: String,
      default: 'app',
      validator: value => {
        return ['app', 'setting'].indexOf(value) !== -1
      },
    },
  },
  data() {
    return {
      activeTab: '',
    }
  },
  computed: {
    asideMenuList() {
      if (this.layoutType === 'setting') {
        return ASIDE_MENU_SETTING
      }
      return ASIDE_MENU_APP
    },
  },
  mounted() {
    if (this.layoutType === 'setting') {
      this.activeTab = ASIDE_MENU_SETTING[0].page
    } else {
      this.activeTab = this.$route.meta.page
    }
  },
  methods: {
    showMenuItem(menuItem) {
      return Permissions.checkPermission(menuItem.roles, PermissionsType.single)
    },
    chooseMenuItem(menuItem) {
      this.activeTab = menuItem.page
      if (menuItem.page !== this.$route.meta.page) {
        this.$router.push({ name: menuItem.page }).catch(() => {})
      }
    },
  },
  i18n: {
    messages: asideMenuLang,
  },
})
</script>
