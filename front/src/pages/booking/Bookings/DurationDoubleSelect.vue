<template>
  <div class="duration-double-select">
    <mp-select
      class="duration-double-select__item"
      :value="hour"
      option-key="label"
      option-value="value"
      option-label="label"
      :option-list="durationSelectHours"
      :popper-append-to-body="false"
      @change="entityChanged('hour', $event)"
    />
    <mp-select
      class="duration-double-select__item"
      :value="minute"
      :disabled="
        hour >= durationSelectHours[durationSelectHours.length - 1].value
      "
      option-key="label"
      option-value="value"
      option-label="label"
      :option-list="durationSelectMinutes"
      :popper-append-to-body="false"
      @change="entityChanged('minute', $event)"
    />
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import MpSelect from '@/components/MpSelect/MpSelect.vue'
import {
  durationSelectHours,
  durationSelectMinutes,
} from '@/pages/booking/Bookings/modals/config/editBookingModal.const'
export default Vue.extend({
  name: 'DurationDoubleSelect',
  components: {
    MpSelect,
  },
  props: {
    hour: {
      type: Number,
      default: 0,
    },
    minute: {
      type: Number,
      default: 0,
    },
  },
  data() {
    return {
      durationSelectHours,
      durationSelectMinutes,
    }
  },
  methods: {
    entityChanged(entity, value) {
      if (entity === 'hour') {
        this.setHour(entity, value)
      } else {
        this.$emit('changed', entity, value)
      }
    },
    setHour(entity, value) {
      const lastHour = durationSelectHours[durationSelectHours.length - 1].value
      if (value >= lastHour) {
        this.$emit('changed', entity, lastHour)
        this.$emit('changed', 'minute', 0)
      } else {
        this.$emit('changed', entity, value)
      }
    },
  },
})
</script>
