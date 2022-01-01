<template>
  <el-dropdown trigger="click" class="user-menu">
    <div class="el-dropdown-link user-menu__header">
      <el-avatar class="user-menu__avatar" size="small">
        <MpIcon :icon-type="'avatar'" />
      </el-avatar>
      <span class="user-menu__user-name">{{ userName }}</span>
    </div>
    <el-dropdown-menu slot="dropdown" class="user-menu__menu-list">
      <el-dropdown-item class="user-menu__item">
        <router-link :to="{ name: 'login' }" class="user-menu__link">
          <div class="user-menu__link-img"></div>
          <span>{{ $t('menuItemTitle.exit') }}</span>
        </router-link>
      </el-dropdown-item>
    </el-dropdown-menu>
  </el-dropdown>
</template>

<script>
import Vue from 'vue'
import { userMenuLang } from './userMenu.lang'
import MpIcon from '@/components/basic/MpIcon/MpIcon'

export default Vue.extend({
  name: 'UserMenu',
  components: {
    MpIcon,
  },

  computed: {
    userName() {
      return (
        this.$store.getters['user/userFullName'] ||
        this.$store.getters['user/userName']
      )
    },
  },
  i18n: {
    messages: userMenuLang,
  },
})
</script>
