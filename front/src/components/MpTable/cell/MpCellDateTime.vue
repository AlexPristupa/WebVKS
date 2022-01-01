<template>
  <div class="mp-cell__date-time">
    {{ modifiedValue() }}
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { ICellRendererParams } from '@ag-grid-community/core'
import {
  IComputedMpCellDateTime,
  IDataMpCellDateTime,
  IMethodsMpCellDateTime,
  IPropsmpCellDateTime,
} from '@/components/MpTable/cell/config/MpCellDateTime.interface'
import { DateTime } from '@/modules/DateTime/DateTime'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellDateTime,
  IMethodsMpCellDateTime,
  IComputedMpCellDateTime,
  IPropsmpCellDateTime
>({
  name: 'MpCellDateTime',
  computed: {
    value() {
      return this.params.colDef.field
        ? this.params.data[this.params.colDef.field]
        : ''
    },
    settings() {
      return this.params.colDef.field
        ? this.params.data.fieldsSetting[this.params.colDef.field]
        : {}
    },
  },
  methods: {
    modifiedValue() {
      if (this.value) {
        return new DateTime({ dateTime: this.value }).toCurrentTimeZone(
          this.settings.showType,
        )
      }
      return ''
    },
  },
})
</script>
