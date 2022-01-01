<template>
  <section class="panel-layout">
    <div class="panel-layout__one panels">
      <slot name="panel" />
    </div>
  </section>
</template>

<script>
import Vue from 'vue'

export default Vue.extend({
  name: 'OnePanelLayout',
})
</script>
