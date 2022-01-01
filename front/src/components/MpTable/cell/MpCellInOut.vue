<template>
  <div class="mp-cell__in-out">
    <span class="mp-cell__in-out-value">{{ localizeValue }}</span>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mpTableLang } from '../mpTable.lang'
import { ICellRendererParams } from '@ag-grid-community/core'
import {
  IComputedMpCellInOut,
  IDataMpCellInOut,
  IMethodsMpCellInOut,
  IPropsMpCellInOut,
} from '@/components/MpTable/cell/config/MpCellInOut.interface'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellInOut,
  IMethodsMpCellInOut,
  IComputedMpCellInOut,
  IPropsMpCellInOut
>({
  name: 'MpCellInOut',
  computed: {
    value() {
      return this.params.colDef.field
        ? this.params.data[this.params.colDef.field]
        : ''
    },
    localizeValue() {
      if (this.value) {
        return this.$t(`mpCellInOut.${this.value}`).toString()
      }
      return ''
    },
  },
  i18n: {
    messages: mpTableLang,
  },
})
</script>
