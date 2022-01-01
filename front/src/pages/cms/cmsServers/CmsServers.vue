<template>
  <one-panel-layout>
    <template #panel>
      <server-list-panel />
    </template>
  </one-panel-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import ServerListPanel from '@/pages/cms/cmsServers/panels/ServerList.Panel.vue'
import OnePanelLayout from '@/layouts/onePanelLayout/onePanelLayout.vue'

export default Vue.extend({
  name: 'CmsServers',
  components: {
    OnePanelLayout,
    ServerListPanel,
  },
})
</script>
