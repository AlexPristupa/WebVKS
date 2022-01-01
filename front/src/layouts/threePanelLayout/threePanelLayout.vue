<template>
  <section class="panel-layout">
    <div
      class="panels"
      :class="{
        'panel-layout__left': !report,
        'panel-layout__left-report': report,
      }"
    >
      <slot name="leftPanel" />
    </div>
    <div
      :class="{
        'panel-layout__right': !report,
        'panel-layout__right-report': report,
      }"
    >
      <div
        class="panels"
        :class="{
          'panel-layout__right-top': !report,
          'panel-layout__right-top-report': report,
        }"
      >
        <slot name="rightTopPanel" />
      </div>
      <div
        class="panels"
        :class="{
          'panel-layout__right-bottom': !report,
          'panel-layout__right-bottom-report': report,
        }"
      >
        <slot name="rightBottomPanel" />
      </div>
    </div>
  </section>
</template>

<script>
import Vue from 'vue'

export default Vue.extend({
  name: 'ThreePanelLayout',
  props: {
    report: {
      type: Boolean,
      default: false,
    },
  },
})
</script>
