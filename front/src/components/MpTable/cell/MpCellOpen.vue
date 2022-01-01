<template>
  <div class="mp-cell__open" @click="itemOpen">
    <span class="mp-cell__open-title">{{ title }}</span>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import {
  IComputedMpCellOpen,
  IDataMpCellOpen,
  IMethodsMpCellOpen,
  IPropsMpCellOpen,
} from '@/components/MpTable/cell/config/MpCellOpen.interface'
import { ICellRendererParams } from '@ag-grid-community/core'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellOpen,
  IMethodsMpCellOpen,
  IComputedMpCellOpen,
  IPropsMpCellOpen
>({
  name: 'MpCellOpen',
  computed: {
    title() {
      return this.params.colDef.field
        ? this.params.data[this.params.colDef.field]
        : ''
    },
  },
  methods: {
    itemOpen() {
      this.$parent.$emit('item-open', { rowData: this.params.data })
    },
  },
})
</script>
