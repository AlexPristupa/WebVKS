<template>
  <div v-if="text" class="custom-tooltip">
    {{ text }}
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import { ICellRendererParams } from '@ag-grid-community/core'
import { DateTime } from '@/modules/DateTime/DateTime'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend({
  name: 'CustomTooltip',
  computed: {
    field(): string {
      return this.params.colDef.field || ''
    },
    // Любая строка любой таблицы
    data(): any {
      return this.params.data
    },
    text(): string {
      // @ts-ignore
      if (this.params.colDef.isVisibleHint && this.params.colDef.tooltipField) {
        if (this.params.colDef.cellRenderer !== 'MpCellDateTime') {
          return this.data[this.params.colDef.tooltipField]
            .toString()
            .replace(/&quot;/g, '"')
        } else {
          return this.getConvertedTimeValue()
        }
      }
      return ''
    },
  },
  methods: {
    getConvertedTimeValue(): string {
      const showType = this.data.fieldsSetting[this.field]?.showType
      return new DateTime({
        dateTime: this.data[this.field],
      }).toCurrentTimeZone(showType) as string
    },
  },
})
</script>
