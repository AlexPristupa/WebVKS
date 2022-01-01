<template>
  <div class="mp-cell__group">
    <div class="mp-cell__group-value" :style="{ paddingLeft: padding }">
      {{ cellData }}
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import {
  groupOffset,
  IComputedMpCellGroup,
  IDataMpCellGroup,
  IMethodsMpCellGroup,
  IPropsMpCellGroup,
} from '@/components/MpTable/cell/config/MpCellGroup.interface'
import { ICellRendererParams } from '@ag-grid-community/core'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellGroup,
  IMethodsMpCellGroup,
  IComputedMpCellGroup,
  IPropsMpCellGroup
>({
  name: 'MpCellGroup',
  computed: {
    cellData() {
      return this.params.colDef.field
        ? this.params.data[this.params.colDef.field]
        : ''
    },
    padding() {
      if (this.params.data.isTitleGroup) {
        return groupOffset.zero
      }
      return groupOffset.one
    },
  },
})
</script>
