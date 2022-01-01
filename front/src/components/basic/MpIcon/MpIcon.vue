<template>
  <div :class="cssClasses"></div>
</template>

<script>
import Vue from 'vue'

export default Vue.extend({
  name: 'MpIcon',
  props: {
    iconType: {
      type: String,
      default: '',
      validator: value => {
        return ['', 'status-a', 'status', 'video', 'search', 'avatar'].includes(
          value,
        )
      },
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  computed: {
    cssClasses() {
      let classes = 'mp-icon'
      if (this.iconType) classes += ` mp-icon--${this.iconType}`
      if (this.iconType && this.disabled) classes += ` disabled`
      return classes
    },
  },
})
</script>
