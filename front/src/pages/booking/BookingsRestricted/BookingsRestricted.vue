<template>
  <one-panel-layout>
    <template #panel>
      <booking-list-panel />
    </template>
  </one-panel-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import OnePanelLayout from '@/layouts/onePanelLayout/onePanelLayout.vue'
import BookingListPanel from '@/pages/booking/BookingsRestricted/panels/BookingListPanel.vue'

export default Vue.extend({
  name: 'BookingsRestricted',
  components: {
    BookingListPanel,
    OnePanelLayout,
  },
})
</script>
