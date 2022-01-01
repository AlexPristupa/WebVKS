<template>
  <div class="mp-cell__translate">
    {{ value }}
  </div>
</template>

<script lang="ts">
/**
 * @description Компонент отображения переведенного значения поля таблицы
 */
import Vue from 'vue'

import { ICellRendererParams } from '@ag-grid-community/core'
import {
  IComputedMpCellTranslate,
  IDataMpCellTranslate,
  IMethodsMpCellTranslate,
  IPropsMpCellTranslate,
} from '@/components/MpTable/cell/config/MpCellTranslate.interface'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellTranslate,
  IMethodsMpCellTranslate,
  IComputedMpCellTranslate,
  IPropsMpCellTranslate
>({
  name: 'MpCellTranslate',
  computed: {
    value() {
      if (this.params.value) {
        return this.$t(
          // @ts-ignore
          `callTable.MpCellTranslate.${this.params.tableName}.${this.params.value}`,
        ).toString()
      }
      return ''
    },
  },
})
</script>
