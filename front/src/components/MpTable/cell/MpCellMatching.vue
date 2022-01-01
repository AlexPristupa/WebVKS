<template>
  <div class="mp-cell--matching">
    <div v-if="numberMap === mpMatching.match" class="mp-cell--matching__item">
      <span class="mp-cell--matching__item__symbol match"> &#8226; </span>
      {{ $t('matching.match') }}
    </div>
    <div
      v-else-if="numberMap === mpMatching.noMatch"
      class="mp-cell--matching__item"
    >
      <span class="mp-cell--matching__item__symbol no-match"> &#8226; </span>
      {{ $t('matching.noMatch') }}
    </div>
    <div v-else class="mp-cell--matching__item">
      <span class="mp-cell--matching__item__symbol no-number"> &#8226; </span>
      {{ $t('matching.noNumber') }}
    </div>
  </div>
</template>

<script lang="ts">
/**
 * @description Todo: Сильно кастомный компонент требует рефакторинга в сторону
 *              универсальности
 */
import Vue from 'vue'
import { ICellRendererParams } from '@ag-grid-community/core'
import {
  MpMatching,
  IComputedMpCellMatching,
  IDataMpCellMatching,
  IMethodsMpCellMatching,
  IPropsMpCellMatching,
} from '@/components/MpTable/cell/config/MpCellMatching.interface'

declare module 'vue/types/vue' {
  interface Vue {
    params: ICellRendererParams
  }
}

export default Vue.extend<
  IDataMpCellMatching,
  IMethodsMpCellMatching,
  IComputedMpCellMatching,
  IPropsMpCellMatching
>({
  name: 'MpCellMatching',
  data() {
    return {
      mpMatching: {
        match: MpMatching.match,
        noMatch: MpMatching.noMatch,
      },
    }
  },
  computed: {
    numberMap() {
      return this.params.data.numberMap
    },
  },
})
</script>
