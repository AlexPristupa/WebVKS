<template>
  <div id="app" :class="themeName">
    <router-view v-if="!sectionApp"></router-view>
    <main-layout v-if="sectionApp" />
  </div>
</template>

<script>
import Vue from 'vue'
import MainLayout from '@/layouts/mainLayout/MainLayout'
import { TokenChecker } from '@/modules/TokenChecker/TokenChecker'

export default Vue.extend({
  name: 'MentolVKS',
  created() {
    TokenChecker.update()
  },
  // todo все еще нужно более талантливое решение.
  components: {
    MainLayout,
  },
  computed: {
    sectionApp() {
      return (
        this.$route.meta.section === 'app' ||
        this.$route.meta.section === 'setting'
      )
    },
    themeName() {
      return `theme-${process.env.VUE_APP_THEME}`
    },
  },
})
</script>
