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
import BookingListPanel from '@/pages/booking/Bookings/panels/BookingListPanel.vue'

export default Vue.extend({
  name: 'Bookings',
  components: {
    BookingListPanel,
    OnePanelLayout,
  },
})
</script>
