<template>
  <div class="mp-cell__checkboxes">
    <div
      class="mp-cell__checkboxes__checkbox"
      v-for="(checkbox, index) in checkboxes"
      :key="index"
    >
      <el-checkbox
        :value="checkbox.checked"
        :label="checkbox.label"
        @change="change(checkbox.field)"
      />
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import {
  IComputedMpCellCheckboxes,
  IDataMpCellCheckboxes,
  IMethodsMpCellCheckboxes,
  IPropsMpCellCheckboxes,
} from '@/components/MpTable/cell/config/MpCellCheckboxes.interface'
import { ICellRendererParams } from '@ag-grid-community/core'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellCheckboxes,
  IMethodsMpCellCheckboxes,
  IComputedMpCellCheckboxes,
  IPropsMpCellCheckboxes
>({
  name: 'MpCellCheckboxes',
  computed: {
    checkboxes() {
      return this.params.value
    },
  },
  methods: {
    change(field) {
      this.$parent.$emit('one-of-checkboxes-changed', {
        id: this.params.data.id,
        field: field,
      })
      return
    },
  },
})
</script>
