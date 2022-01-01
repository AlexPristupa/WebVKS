<template>
  <div class="mp-cell__phone-mask">
    <span class="mp-cell__phone-mask-value" v-mask="'(999) 999-99-99'">{{
      value
    }}</span>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { ICellRendererParams } from '@ag-grid-community/core'
import {
  IComputedMpCellPhoneMask,
  IDataMpCellPhoneMask,
  IMethodsMpCellPhoneMask,
  IPropsMpCellPhoneMask,
} from '@/components/MpTable/cell/config/MpCellPhoneMask.interface'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellPhoneMask,
  IMethodsMpCellPhoneMask,
  IComputedMpCellPhoneMask,
  IPropsMpCellPhoneMask
>({
  name: 'MpCellPhoneMask',
  computed: {
    value() {
      return this.params.colDef.field
        ? this.params.data[this.params.colDef.field]
        : ''
    },
  },
})
</script>
