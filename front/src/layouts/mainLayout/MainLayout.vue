<template>
  <div class="main-layout">
    <vue-headful :title="title" />
    <aside class="aside">
      <aside-menu :layout-type="layoutType" />
    </aside>
    <main class="main">
      <TabsView></TabsView>
      <nav class="main__header">
        <user-menu></user-menu>
      </nav>
    </main>
  </div>
</template>

<script>
import Vue from 'vue'
import AsideMenu from '@/components/asideMenu/AsideMenu'
import UserMenu from '@/components/userMenu/UserMenu'
import TabsView from '@/components/TabsView'

export default Vue.extend({
  name: 'MainLayout',
  components: {
    AsideMenu,
    UserMenu,
    TabsView,
  },
  data() {
    return {
      title: 'MENTOL PRO',
    }
  },
  computed: {
    layoutType() {
      return this.$route.meta.section
    },
  },
})
</script>
