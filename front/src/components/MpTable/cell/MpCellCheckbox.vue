<template>
  <div class="mp-cell__checkbox">
    <el-checkbox :value="cellData" :disabled="disabled" @change="changeValue" />
  </div>
</template>

<script lang="ts">
/**
 * @description Компонент отображения чекбокса в строке таблицы. Работает
 *              совместно с:
 *              - MpCellHeaderCheckbox
 *              - class RowDataChecked
 */
import Vue from 'vue'

import { ICellRendererParams } from '@ag-grid-community/core'
import {
  IComputedMpCellCheckbox,
  IDataMpCellCheckbox,
  IMethodsMpCellCheckbox,
  IPropsMpCellCheckbox,
} from '@/components/MpTable/cell/config/MpCellCheckbox.interface'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellCheckbox,
  IMethodsMpCellCheckbox,
  IComputedMpCellCheckbox,
  IPropsMpCellCheckbox
>({
  name: 'MpCellCheckbox',
  computed: {
    cellData() {
      return this.params.colDef.field
        ? this.params.data[this.params.colDef.field]
        : ''
    },
    disabled() {
      return this.params.data.disabled || this.params.data.checkboxAllDisabled
    },
  },
  methods: {
    changeValue() {
      this.params.data.checkbox = !this.params.data.checkbox
      this.$parent.$emit('check-row', {
        column: this.params.colDef.field,
        row: this.params.data,
      })
    },
  },
})
</script>
