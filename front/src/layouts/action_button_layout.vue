<template>
  <div class="action-button-layout">
    <slot />
  </div>
</template>

<script>
import elDragDialog from '@/directive/el-dragDialog'

export default {
  name: 'ActionButtonLayout',
  directives: { elDragDialog },
}
</script>
