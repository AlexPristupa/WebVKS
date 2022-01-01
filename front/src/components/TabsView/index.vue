<template>
  <el-tabs :value="activeTab" type="border-card" @tab-click="selectTab">
    <template v-for="tab in tabData">
      <el-tab-pane
        :key="tab.id"
        :label="$t(tab.label)"
        :name="tab.name"
        v-if="showTabItem(tab)"
      >
        <template v-if="$route.name === tab.name">
          <div class="main__content" :key="contentKey">
            <router-view></router-view>
          </div>
        </template>
      </el-tab-pane>
    </template>
  </el-tabs>
</template>
<script>
import { mainLayoutLang } from './mainLayout.lang'
import * as CONSTANTS from '@/constant'
import { Permissions } from '@/modules/Permission/Permissions'
import { RouteName } from '@/router/RouteName.enum'

export default {
  name: 'TabsView',
  data() {
    return {
      contentKey: 1,
    }
  },
  computed: {
    tabData() {
      const data = CONSTANTS.tabsSetting[this.$route.meta.page]
      return this.$route.meta.page !== RouteName.noAccess &&
        this.$route.meta.page !== RouteName.notFound
        ? data.filter(tab => {
            return Permissions.checkPermission([tab.role])
          })
        : data
    },
    activeTab() {
      const route = this.tabData.find(x => x.routeName === this.$route.name)
      if (route) {
        return route.routeName
      }
      return ''
    },
  },
  methods: {
    showTabItem(tab) {
      return (
        this.$route.meta.page === RouteName.noAccess ||
        this.$route.meta.page === RouteName.notFound ||
        Permissions.checkPermission([tab.role])
      )
    },
    selectTab(tab) {
      if (this.activeTab !== tab.name) {
        this.contentKey = 1
        this.$router.push({ name: tab.name })
      } else {
        this.contentKey++
      }
    },
  },
  i18n: {
    messages: mainLayoutLang,
  },
}
</script>
