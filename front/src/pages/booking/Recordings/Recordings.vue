<template>
  <one-panel-layout>
    <template #panel>
      <recordings-list-panel />
    </template>
  </one-panel-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import OnePanelLayout from '@/layouts/onePanelLayout/onePanelLayout.vue'
import RecordingsListPanel from '@/pages/booking/Recordings/panels/RecordingsListPanel.vue'

export default Vue.extend({
  name: 'Records',
  components: {
    OnePanelLayout,
    RecordingsListPanel,
  },
})
</script>
