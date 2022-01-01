<template>
  <section class="panel-layout">
    <div class="panel-layout__left panels">
      <slot name="leftPanel" />
    </div>
    <div class="panel-layout__right panels">
      <slot name="rightPanel" />
    </div>
  </section>
</template>

<script>
import Vue from 'vue'

export default Vue.extend({
  name: 'TwoPanelLayout',
})
</script>
