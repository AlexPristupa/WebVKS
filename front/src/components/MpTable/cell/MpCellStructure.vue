<template>
  <div class="mp-cell__structure mp-cell-structure">
    <div
      class="mp-cell-structure__arrow"
      :class="{
        'mp-cell-structure__arrow--open': structureIsOpen,
      }"
      :style="{ paddingLeft: level }"
      @click="toggleStructure"
    >
      <div v-if="childrenLength" class="mp-cell-structure__arrow-body"></div>
    </div>
    <div class="mp-cell-structure__title">
      {{ cellData }}
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { TreeStructureEventType } from '@/modules/TreeStructure/TreeStructure.types.ts'
import { ICellRendererParams } from '@ag-grid-community/core'
import {
  IComputedMpCellStructure,
  IDataMpCellStructure,
  IMethodsMpCellStructure,
  IPropsMpCellStructure,
} from '@/components/MpTable/cell/config/MpCellStructure.interface'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellStructure,
  IMethodsMpCellStructure,
  IComputedMpCellStructure,
  IPropsMpCellStructure
>({
  name: 'MpCellStructure',
  computed: {
    cellData() {
      this.addClass()
      return this.params.colDef.field
        ? this.params.data[this.params.colDef.field]
        : ''
    },
    isFound() {
      return this.params.data.isFound
    },
    level() {
      return `${this.params.data.level}rem`
    },
    structureIsOpen() {
      return this.params.data.collapsed
    },
    childrenLength() {
      return this.params.data.children.length
    },
  },
  mounted() {
    this.addClass()
  },
  methods: {
    toggleStructure() {
      this.params.node.setSelected(true, true)
      this.$parent.$emit('structure', {
        type: this.params.data.collapsed
          ? TreeStructureEventType.closeBranch
          : TreeStructureEventType.openBranch,
        data: this.params.data,
      })
    },
    addClass() {
      // @ts-ignore
      this.params.api.rowStyle = { background: 'red' }
    },
  },
})
</script>
