<template>
  <div class="mp-cell__boolean">
    <i v-if="value" class="el-icon-check"></i>
  </div>
</template>

<script lang="ts">
/**
 * @description Компонент отображения логического значения поля таблицы
 */
import Vue from 'vue'

import { ICellRendererParams } from '@ag-grid-community/core'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend({
  name: 'MpCellBoolean',
  computed: {
    value(): boolean {
      return this.params.colDef.field
        ? this.params.data[this.params.colDef.field]
        : false
    },
  },
})
</script>
