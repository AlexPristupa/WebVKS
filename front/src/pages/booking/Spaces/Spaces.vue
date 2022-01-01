<template>
  <one-panel-layout>
    <template #panel>
      <spaces-list-panel />
    </template>
  </one-panel-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import OnePanelLayout from '@/layouts/onePanelLayout/onePanelLayout.vue'
import SpacesListPanel from '@/pages/booking/Spaces/panels/SpacesListPanel.vue'

export default Vue.extend({
  name: 'Spaces',
  components: {
    SpacesListPanel,
    OnePanelLayout,
  },
})
</script>
