<template>
  <div class="mp-cell__status">
    <mp-icon v-if="type" :icon-type="type" :disabled="disabled" />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import MpIcon from '@/components/basic/MpIcon/MpIcon.vue'
import { ICellRendererParams } from '@ag-grid-community/core'
import {
  IComputedMpCellStatus,
  IDataMpCellStatus,
  IMethodsMpCellStatus,
  IPropsMpCellStatus,
} from '@/components/MpTable/cell/config/MpCellStatus.interface'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellStatus,
  IMethodsMpCellStatus,
  IComputedMpCellStatus,
  IPropsMpCellStatus
>({
  name: 'MpCellStatus',
  components: {
    MpIcon,
  },
  computed: {
    cellData() {
      return this.params.colDef.field
        ? this.params.data[this.params.colDef.field]
        : {}
    },
    type() {
      return this.cellData.type || ''
    },
    disabled() {
      return this.cellData.disabled || true
    },
  },
})
</script>
