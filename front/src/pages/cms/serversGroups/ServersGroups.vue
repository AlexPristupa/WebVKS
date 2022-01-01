<template>
  <two-panel-layout>
    <template #leftPanel>
      <server-group-list-panel
        ref="server-group-list-panel"
        @setServersFilter="setFilterToRight"
      />
    </template>
    <template #rightPanel>
      <servers-list-panel ref="servers-list-panel" />
    </template>
  </two-panel-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import ServerGroupListPanel from '@/pages/cms/serversGroups/panels/ServerGroupList.Panel.vue'
import ServersListPanel from '@/pages/cms/cmsServers/panels/ServerList.Panel.vue'
import TwoPanelLayout from '@/layouts/twoPanelLayout/twoPanelLayout.vue'

export default Vue.extend({
  name: 'ServerGroups',
  components: {
    TwoPanelLayout,
    ServerGroupListPanel,
    ServersListPanel,
  },
  methods: {
    setFilterToRight(filter) {
      // @ts-ignore
      this.$refs['servers-list-panel'].externalFilter = filter
    },
  },
})
</script>
